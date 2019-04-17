using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Programmer:  Michael the Atkinson
 * Description:
 * RoomGenerator is more like RoomManager;
 * Keeps a reference to every object I have to instantiate (floor, walls, doors, objects, lights),
 * a List of every Room object, 
 * defines Room Types,
 *
 * contains a Get function to create a Room object, 
 * and a BuildDoors function which connects bordering rooms then builds walls around each room.
 *
 * also has a get/set function for room size and room coordinates, 
 * but those also exist in Room.cs, and should be used from Room.cs instead to get/set for individual rooms, not from here.
 *
 * the Get() function here is not really necessary; the LevelGenerator could just add an empty GameObject, 
 *
 * * *
 *
 * This class is using the Singleton pattern.
 *
 */


public class RoomGenerator : MonoBehaviour
{
    // Declare all the object references I'll be using; gets passed down to Room class.  
    // I think there might be a better way to handle this, but this works fine and doesn't appear to affect memory usage or anything
    public Room playerRoom;
    public enum RoomType { Start, Boss, Treasure, Puzzle, Combat, None };
    public List<GameObject> teleporterList; 
    public int wallLayer;
    public int wallMask; 
    public List<Room> roomList; 
    public GameObject Wall; 
    public GameObject Door;
    public GameObject FloorTile; 
    public GameObject Ceiling; 
    public GameObject Console;
    public GameObject Panel;
    public GameObject Portal;
    public GameObject Column;

    // lighting colors
    public Color Amber; 
    public Color Cyan; 
    public Color Fuschia; 
    public Color Red; 
    public Color LightGreen; 

    // these aren't really used anymore
    public RoomType rt;
    public Room r;

    private Vector3 size = new Vector3(32.0f,4.0f,32.0f);
    private Vector3 Zero;

    public static RoomGenerator instance;
    public void Awake() 
    {
        if(instance == null)    instance = this;
        roomList = new List<Room>();
        teleporterList = new List<GameObject>();
        Wall = Resources.Load<GameObject>("Michael/Wall_2_X4");
        Door = Resources.Load<GameObject>("Michael/Door");
        FloorTile = Resources.Load<GameObject>("Michael/Floor_003");
        Ceiling = Resources.Load<GameObject>("Michael/Plane");
        Console = Resources.Load<GameObject>("Michael/Console_001");
        Panel = Resources.Load<GameObject>("Michael/Panel_001");
        Portal = Resources.Load<GameObject>("Michael/Portal 1");
        Column =  Resources.Load<GameObject>("Michael/Wall_2_Column");

        // lighting colors
        Amber = new Color(1.0f, 0.82f, 0.39f);
        Cyan = new Color(0.47f, 1, 1);
        Fuschia = new Color(0.87f, 0.39f, 1);
        Red = new Color(0.97f, 0.29f, 0.29f);
        LightGreen = new Color(0.3f, 1, 0.3f);
        
        wallLayer = 8;
        wallMask = 1 << wallLayer; 
    }

    // declare's an empty gameobject as a room, and attaches specific room type as component.
    public static GameObject Get(Vector3 Zero, RoomType rt = RoomType.None)
    {
        GameObject newroom = new GameObject();
        newroom.transform.position = Zero;
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
                float rand = Random.value;
                if(rand < 0.15f)
                    r = newroom.AddComponent<PuzzleRabbits>();
                else if(rand < 0.3f)
                    r = newroom.AddComponent<PuzzleBox>();
                else if(rand < 0.45f)
                    r = newroom.AddComponent<PuzzlePlatforms>();
                else if(rand < 0.6f)
                    r = newroom.AddComponent<PuzzleTurrets>();
                else if(rand < 0.85f)
                    r = newroom.AddComponent<PuzzleSuperPlatforms>();
                else
                    r = newroom.AddComponent<PuzzleLowGravity>();
                newroom.name = "Puzzle Room";
                break;
            default:
                r = newroom.AddComponent<Room>();
                newroom.name = "Room";
                break;
        }
        
        // I'll comment out the lines below, 
        // then in LevelGenerator we can add in something like: 
        //  GameObject r = RoomGenerator.Get(Zero,rt);
        //  r.SetSize(vector3 dimensions);
        //  r.complexity = x;
        //  ...other room attributes to be added later....
        //  r.Init();
        r.SetZero(Zero);
        //r.SetSize(size);
        //roomList.Add(r);
        return newroom;
    }

    /*
    *    checks each Room's box collider for intersecting box colliders attached to other rooms.
    *    finds overlapping wall sections, finds exact center of overlap, and puts a door there.
    *    it does this for every room. 
    *    finally it calls the BuildWall function for every room, which puts up walls between all the doors.
    *
    */

    public static void BuildDoors()
    {
        foreach(Room room2 in instance.roomList) 
            if(! room2.initialized) room2.Init();
        Debug.Log("building doors");

        GameObject d;
        Vector3 dBounds;
        float doorWidth = 2.5f;

        foreach(Room room in instance.roomList)
        {
            Bounds r1 = room.GetComponent<Collider>().bounds;
            foreach(Collider room2Collider in Physics.OverlapBox(r1.center,r1.size/2 )) {
                if(room2Collider.GetComponent<Room>()) {
                    Room room2 = room2Collider.GetComponent<Room>();
                    Bounds r2 = room2Collider.bounds;
                    // detect if r1 is touching r2 on r2's north, south, east, or west side.
                    // r south of r1:
                    //if(r1.ClosestPoint(r2.center).z == room.Zero.z) {
                    if(r2.center.z - r2.size.z/2 >= r1.center.z + r1.size.z/2) {
                        //find X location of doorway from south side of o to north side of r.
                        float WestOverlapEdge = Mathf.Max(r1.center.x-r1.size.x/2,r2.center.x-r2.size.x/2);
                        float EastOverlapEdge = Mathf.Min(r1.center.x+r1.size.x/2,r2.center.x+r2.size.x/2);
                        if(EastOverlapEdge - WestOverlapEdge > instance.Door.GetComponent<Renderer>().bounds.size.magnitude*1) {
                            float DoorX = (WestOverlapEdge+EastOverlapEdge)/2.0f+0.5f;
                            float DoorZ = r1.center.z+r1.size.z/2;
                            d = GameObject.Instantiate(instance.Door,new Vector3(DoorX,Mathf.Min(room.GetSize().y,room2.GetSize().y)/2,DoorZ),Quaternion.identity,room.transform);
                            dBounds = d.GetComponent<Renderer>().bounds.size;
                            d.transform.localScale = new Vector3(doorWidth/dBounds.x, Mathf.Min(room.GetSize().y,room2.GetSize().y)/dBounds.y, d.transform.localScale.z);
                            d.name = "Door";
                            d.gameObject.SetActive(false);
                            d.gameObject.SetActive(true);
                            room.DoorList.Add(d);
                            room2Collider.GetComponent<Room>().DoorList.Add(d);
                        }
                    }

                    else if(r2.center.x + r2.size.x/2 <= r1.center.x - r1.size.x/2) {
                        
                        float SouthOverlapEdge = Mathf.Max(r1.center.z-r1.size.z/2,r2.center.z-r2.size.z/2);
                        float NorthOverlapEdge = Mathf.Min(r1.center.z+r1.size.z/2,r2.center.z+r2.size.z/2);
                        if(NorthOverlapEdge - SouthOverlapEdge > instance.Door.GetComponent<Renderer>().bounds.size.magnitude*1) {
                            float DoorZ = (Mathf.Max(r1.center.z-r1.size.z/2,r2.center.z-r2.size.z/2)+Mathf.Min(r1.center.z+r1.size.z/2,r2.center.z+r2.size.z/2))/2.0f+0.5f;
                            float DoorX = r1.center.x-r1.size.x/2;

                            d = GameObject.Instantiate(instance.Door,new Vector3(DoorX,Mathf.Min(room.GetSize().y,room2.GetSize().y)/2,DoorZ),Quaternion.identity,room.transform);
                            dBounds = d.GetComponent<Renderer>().bounds.size;
                            d.transform.localScale = new Vector3(doorWidth/dBounds.x, Mathf.Min(room.GetSize().y,room2.GetSize().y)/dBounds.y, d.transform.localScale.z);
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

        //after all the doorways between rooms are found,
        // build the walls and turn off the lights.
        foreach(Room r in instance.roomList) {
            r.GetWalls();
            r.SetLighting(instance.Cyan,0);
        }
        GameObject timer = new GameObject("timer");
        timer.AddComponent<PuzzleCountdown>();
    }

    public static void BakeNavMesh()
    {
        // this actually doesn't need to be it's own function.  but it's already implemented in LevelGenerator, so ¯\_(ツ)_/¯
        NavMeshSurface surface = instance.roomList[0].transform.Find("Floor").GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
        /*
        foreach(Room r in roomList) {
            NavMeshSurface surface = r.gameObject.transform.Find("Floor").GetComponent<NavMeshSurface>();
            //UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
            surface.BuildNavMesh();
        }
        */
    }


    // the Size and Zero here are default Zeros, not for specific rooms; so these shouldn't really be used.
    public Vector3 GetSize() { return size; }
    public void SetSize(float x, float y, float z) 
    {
        size = new Vector3(x,y,z);
    }
    public Vector3 GetZero() { return Zero; }
    
}
