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
    public static List<Room> RoomList = new List<Room>();
    public static List<GameObject> EnemyList = new List<GameObject>();
    public static GameObject Wall;
    public static GameObject Door;
    public static GameObject Block;
    public static GameObject Floor;
    public static GameObject Ceiling; 


    public RoomType rt;
    public Room r;

    [SerializeField]
    private Vector3 size = new Vector3(16.0f,4.0f,16.0f);
    [SerializeField]
    private Vector3 Zero;
    public bool standalone = false;
    public bool final = false;

    void Awake()
    {
        Wall = Resources.Load<GameObject>("Michael/Wall");
        Door = Resources.Load<GameObject>("Michael/Door");
        Block = Resources.Load<GameObject>("Michael/Block");
        Floor = Resources.Load<GameObject>("Michael/Plane");
        Ceiling = Resources.Load<GameObject>("Michael/Plane");
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
                newroom.AddComponent<PuzzleOne>();
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
        // each wall is an empty object with a collider.
        // this function checks each wall collider for another collider close by,
        // indicating it's next to another room.
        // it finds where the room edges overlap, puts a Doorway there, and removes the empty wall collider.
        // finally it calls the BuildWalls function, which puts up walls between all the doors.
        // I'm adding doors to two different rooms, so i have to do DoorList.Add on the correct room...fix this
        GameObject d;
        foreach(Room r in RoomList)
        {
            Transform n = r.transform.Find("Walls/NorthWall");
            foreach(Collider o in Physics.OverlapBox(n.position,n.GetComponent<BoxCollider>().size/3)) {
                if(o.name == "SouthWall") {
                    Room NorthRoom = o.transform.parent.parent.gameObject.GetComponent<Room>();
                    Room SouthRoom = n.transform.parent.parent.gameObject.GetComponent<Room>();

                    float Doorx = (int)(Mathf.Max(NorthRoom.WestWall.transform.position.x,SouthRoom.WestWall.transform.position.x) 
                            + Mathf.Min(NorthRoom.EastWall.transform.position.x,SouthRoom.EastWall.transform.position.x))/2.0f + 0.5f;

                    d = GameObject.Instantiate(
                            Door,
                            new Vector3(Doorx,NorthRoom.size.y/2,NorthRoom.Zero.z),
                            Quaternion.identity,
                            NorthRoom.SouthWall.transform);
                    d.transform.localScale = new Vector3(Door.transform.localScale.x,NorthRoom.size.y,Door.transform.localScale.z);
                    d.name = "Door";
                    r.DoorList.Add(d);
                    d = GameObject.Instantiate(
                            Door,
                            new Vector3(Doorx,NorthRoom.size.y/2,NorthRoom.Zero.z),
                            Quaternion.identity,
                            SouthRoom.NorthWall.transform);
                    d.transform.localScale = new Vector3(Door.transform.localScale.x,SouthRoom.size.y,Door.transform.localScale.z);
                    d.name = "Door";
                    r.DoorList.Add(d);

                }
                Destroy(o.GetComponent<BoxCollider>());
                Destroy(n.GetComponent<BoxCollider>());

            }
            Transform w = r.transform.Find("Walls/WestWall");
            foreach(Collider o in Physics.OverlapBox(w.position,w.GetComponent<BoxCollider>().size/3)) {
                if(o.name == "EastWall") {
                    Room WestRoom = o.transform.parent.parent.gameObject.GetComponent<Room>();
                    Room EastRoom = w.transform.parent.parent.gameObject.GetComponent<Room>();

                    float Doorz = (int)((Mathf.Max(WestRoom.SouthWall.transform.position.z,EastRoom.SouthWall.transform.position.z) 
                            + Mathf.Min(WestRoom.NorthWall.transform.position.z,EastRoom.NorthWall.transform.position.z))/2.0f) + 0.5f;

                    d = GameObject.Instantiate(
                            Door, 
                            new Vector3(EastRoom.Zero.x,WestRoom.size.y/2,Doorz),
                            Quaternion.Euler(0,90.0f,0),
                            WestRoom.EastWall.transform);
                    d.transform.localScale = new Vector3(Door.transform.localScale.x,WestRoom.size.y,Door.transform.localScale.z);
                    d.name = "Door";
                    r.DoorList.Add(d);
                    d = GameObject.Instantiate(
                            Door, 
                            new Vector3(EastRoom.Zero.x,WestRoom.size.y/2,Doorz),
                            Quaternion.Euler(0,90.0f,0),
                            EastRoom.WestWall.transform);
                    d.transform.localScale = new Vector3(Door.transform.localScale.x,EastRoom.size.y,Door.transform.localScale.z);
                    d.name = "Door";
                    r.DoorList.Add(d);
                }
                Destroy(o.GetComponent<BoxCollider>());
                Destroy(w.GetComponent<BoxCollider>());
            }

        }
        foreach(Room r in RoomList) {
            r.BuildWalls();
        }

    }

    public static void BakeNavMesh()
    {
        Debug.Log("baking");
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
        Debug.Log("done");
    }


    public Vector3 GetSize() { return this.size; }
    public void SetSize(float x, float y, float z) {
        this.size = new Vector3(x,y,z);
    }
    public Vector3 GetZero() { return this.Zero; }

}
