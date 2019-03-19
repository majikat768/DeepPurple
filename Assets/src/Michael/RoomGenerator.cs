using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

/*
 */

public class RoomGenerator : MonoBehaviour
{

    // Declare all the object references I'll be using; gets passed down to Room class
    public enum RoomType { Start, Boss, Treasure, Puzzle, Combat };
    public static int WallLayer;
    public static int WallMask;
    public static List<Room> RoomList = new List<Room>();
    public static List<GameObject> EnemyList = new List<GameObject>();
    public static GameObject Wall;
    public static float WallHeight;
    public static GameObject Door;
    public static GameObject Block;
    public static GameObject FloorTile;
    public static GameObject Ceiling; 
    public static GameObject WallLight;

    public static Color Amber = new Color(1.0f, 0.82f, 0.39f);
    public static Color Cyan = new Color(0.47f, 1, 1);
    public static Color Fuschia = new Color(0.87f, 0.39f, 1);
    public static Color LightGreen = new Color(0.4f, 1, 0.4f);

    public RoomType rt;
    public Room r;

    [SerializeField]
    private Vector3 size = new Vector3(32.0f,5.0f,32.0f);
    [SerializeField]
    private Vector3 Zero;
    public bool standalone = false;
    public bool final = false;

    void Awake()
    {
        WallLayer = 8;
        WallMask = 1 << WallLayer;
        //Wall = Resources.Load<GameObject>("Michael/Wall");
        Wall = Resources.Load<GameObject>("Michael/Wall_2_X4");
        //Door = Resources.Load<GameObject>("Michael/Door");
        Door = Resources.Load<GameObject>("Michael/WindowGlass_001");
        Block = Resources.Load<GameObject>("Michael/Block");
        FloorTile = Resources.Load<GameObject>("Michael/Floor_003");
        //FloorTile = Resources.Load<GameObject>("Michael/FloorTile");
        Ceiling = Resources.Load<GameObject>("Michael/Plane");
        WallLight = Resources.Load<GameObject>("Michael/Roof_Light_003");
    }
    void Start() {

        if(standalone)  Get(this.transform.position, rt);
        if (final)
        {
            BuildDoors();
            BakeNavMesh();
        }
        /*room = Get(transform.position,rt);
        // don't need to call setSize.  if you don't it's default 16x16
        room.SetSize(size.x,size.y,size.z); 
        BuildDoors();
        BakeNavMesh();
        */
    }

    public Room Get(Vector3 Zero, RoomType rt)
    {
        GameObject newroom = new GameObject();
        Room r;
        switch(rt)
        {
            case RoomType.Start:
                r = newroom.AddComponent<StartRoom>();
                newroom.name = "Start Room";
                break;
            case RoomType.Combat:
                r = newroom.AddComponent<CombatRoom>();
                newroom.name = "Fight Room";
                break;
            case RoomType.Treasure:
                r = newroom.AddComponent<TreasureRoom>();
                newroom.name = "Treasure Room";
                break;
            case RoomType.Boss:
                r = newroom.AddComponent<BossRoom>();
                newroom.name = "Boss Room";
                break;
            case RoomType.Puzzle:
                r = newroom.AddComponent<PuzzleRoom>();
                //newroom.AddComponent<PuzzleOne>();
                newroom.AddComponent<PuzzleTwo>();
                newroom.name = "Puzzle Room";
                break;
            default:
                r = newroom.AddComponent<Room>();
                newroom.name = "Room";
                break;
        }
        
        // I'll comment out the lines below, 
        // then in LevelGenerator you can add in something like: 
        //  r = GetComponent<RoomGenerator>().Get(Zero,rt);
        //  r.SetSize(vector3 dimensions);
        //  ...other room attributes to be added later....
        //  r.Init();
        //  the output should be something like the scene in my tst/ folder.
        r.SetZero(Zero);
        r.SetSize(size);
        r.Init();
        RoomList.Add(r);
        return r;
    }

    public static void BuildDoors()
    {
        //checks each Room's box collider for nearby box colliders attached to other rooms,
        //finds overlapping wall sections and puts Door objects there
        // this function checks each wall collider for another collider close by,
        // indicating it's next to another room.
        // it finds where the room edges overlap, puts a Doorway there, and removes the empty wall collider.
        // finally it calls the BuildWall function, which puts up walls between all the doors.
        // I'm adding doors to two different rooms, so i have to do DoorList.Add on the correct room...fix this
        GameObject d;
        Vector3 dBounds;
        float doorWidth = 2.5f;
        foreach(Room room in RoomList)
        {
            BoxCollider roomCollider = room.GetComponent<BoxCollider>();
            foreach(Collider room2Collider in Physics.OverlapBox(roomCollider.center,roomCollider.size/2)) {
                if(room2Collider.GetComponent<Room>() && room2Collider != roomCollider) {
                    // detect if roomCollider is touching o on o's north, south, east, or west side.
                    // r south of o
                    if(room2Collider.bounds.center.z - room2Collider.bounds.size.z/2 >= roomCollider.center.z + roomCollider.size.z/2) {
                        //find X location of doorway from south side of o to north side of r.
                        float WestOverlapEdge = Mathf.Max(roomCollider.center.x-roomCollider.size.x/2,room2Collider.bounds.center.x-room2Collider.bounds.size.x/2);
                        float EastOverlapEdge = Mathf.Min(roomCollider.center.x+roomCollider.size.x/2,room2Collider.bounds.center.x+room2Collider.bounds.size.x/2);
                        if(EastOverlapEdge - WestOverlapEdge > Door.transform.localScale.x*2) {
                            float DoorX = (WestOverlapEdge+EastOverlapEdge)/2.0f+0.5f;
                            float DoorZ = roomCollider.center.z+roomCollider.size.z/2;
                            d = GameObject.Instantiate(Door,new Vector3(DoorX,room.size.y/2,DoorZ),Quaternion.identity,room.transform);
                            dBounds = d.GetComponent<Renderer>().bounds.size;
                            d.transform.localScale = new Vector3(doorWidth/dBounds.x, room.size.y/dBounds.y, d.transform.localScale.z);
                            d.name = "Door";
                            d.gameObject.SetActive(false);
                            d.gameObject.SetActive(true);
                            room.DoorList.Add(d);
                            room2Collider.GetComponent<Room>().DoorList.Add(d);
                        }
                    }
                    /* else if(o.bounds.center.z + o.bounds.size.z/2 <= roomCollider.center.z-roomCollider.size.z/2)
                        Debug.Log(roomCollider.name + " north of " + o.name); */
                    else if(room2Collider.bounds.center.x + room2Collider.bounds.size.x/2 <= roomCollider.center.x - roomCollider.size.x/2) {
                        
                        float SouthOverlapEdge = Mathf.Max(roomCollider.center.z-roomCollider.size.z/2,room2Collider.bounds.center.z-room2Collider.bounds.size.z/2);
                        float NorthOverlapEdge = Mathf.Min(roomCollider.center.z+roomCollider.size.z/2,room2Collider.bounds.center.z+room2Collider.bounds.size.z/2);
                        if(NorthOverlapEdge - SouthOverlapEdge > Door.transform.localScale.x*2) {
                            float DoorZ = (Mathf.Max(roomCollider.center.z-roomCollider.size.z/2,room2Collider.bounds.center.z-room2Collider.bounds.size.z/2)+Mathf.Min(roomCollider.center.z+roomCollider.size.z/2,room2Collider.bounds.center.z+room2Collider.bounds.size.z/2))/2.0f+0.5f;
                            float DoorX = roomCollider.center.x-roomCollider.size.x/2;

                            d = GameObject.Instantiate(Door,new Vector3(DoorX,room.size.y/2,DoorZ),Quaternion.identity,room.transform);
                            dBounds = d.GetComponent<Renderer>().bounds.size;
                            d.transform.localScale = new Vector3(doorWidth/dBounds.x, room.size.y/dBounds.y, d.transform.localScale.z);
                            d.transform.Rotate(0, 90, 0);
                            d.name = "Door";
                            d.gameObject.SetActive(false);
                            d.gameObject.SetActive(true);
                            room.DoorList.Add(d);
                            room2Collider.GetComponent<Room>().DoorList.Add(d);
                        }
                    }

                }
            }

        }

        foreach(Room r in RoomList) {
            r.GetWalls();
            r.SetLighting(Amber);
        }

    }

    public static void BakeNavMesh()
    {
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
    }


    public Vector3 GetSize() { return this.size; }
    public void SetSize(float x, float y, float z) {
        this.size = new Vector3(x,y,z);
    }
    public Vector3 GetZero() { return this.Zero; }
    
    public void Update() {
    }

}
