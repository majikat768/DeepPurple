using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

/*
 * This generates several different room types, which have different contents and layouts.
 * 
 * ----
 * 
 * input size variable in editor to set room size (default = 16)
 * this script, attached to an empty object, builds a room with that object's coordinates as (0,0,0).
 * 
 
 * TODO:
 * * add room connections, e.g. doors.....right now just an empty space at the middle of the wall
 * * Or find fancier way to provide room connections for Level Generator
 * * add player / items / enemy, once they're available
 * * make floors and walls look prettier.
 * * .....
 * 
 */

public class RoomGenerator : MonoBehaviour
{
    public enum RoomType { Start, Boss, Treasure, Puzzle, Combat };
    public static List<Room> RoomList = new List<Room>();
    public static List<GameObject> WallList = new List<GameObject>();
    public static List<GameObject> DoorList = new List<GameObject>();
    public List<GameObject> MyDoors = new List<GameObject>();
    public static List<GameObject> EnemyList = new List<GameObject>();

    public RoomType rt;
    public Room r;

    [SerializeField]
    private Vector3 size = new Vector3(16.0f,4.0f,16.0f);
    public bool standalone = false;

    void Start()
    {
        if (standalone)
        {
            Get(this.transform.position, rt);
            BuildDoors();
            BakeNavMesh();
        }
        /*room = Get(transform.position,rt);
        // don't need to call setSize.  if you don't it's default 16x16
        room.SetSize(size.x,size.y,size.z); 
        room.Generate();
        BuildDoors();
        */
    }

    public Room Get(Vector3 Zero, RoomType rt)
    {
        this.rt = rt;
        GameObject newroom = new GameObject();
        newroom.AddComponent<RoomGenerator>();
        switch(rt)
        {
            case RoomType.Start:
                //r = new StartRoom(Zero,this.gameObject);
                r = new StartRoom(Zero,newroom);
                //this.name = "Start Room";
                newroom.name = "Start Room";
                break;
            case RoomType.Combat:
                //r = new CombatRoom(Zero,this.gameObject);
                r = new CombatRoom(Zero,newroom);
                //this.name = "Fight Room";
                newroom.name = "Fight Room";
                break;
            case RoomType.Treasure:
                //r = new TreasureRoom(Zero,this.gameObject);
                r = new TreasureRoom(Zero,newroom);
                //this.name = "Treasure Room";
                newroom.name = "Treasure Room";
                break;
            case RoomType.Boss:
                //r = new BossRoom(Zero,this.gameObject);
                r = new BossRoom(Zero,newroom);
                //this.name = "Boss Room";
                newroom.name = "Boss Room";
                break;
            case RoomType.Puzzle:
                //r = new PuzzleRoom(Zero,this.gameObject);
                r = new PuzzleRoom(Zero,newroom);
                newroom.AddComponent<PuzzleOne>().SetBounds(Zero,size);
                //this.name = "Puzzle Room";
                newroom.name = "Puzzle Room";
                break;
            default:
                //r = new Room(Zero,this.gameObject);
                r = new Room(Zero,newroom);
                //this.name = "Room";
                newroom.name = "Room";
                break;
        }
        RoomList.Add(r);
        return r;
    }

    public static void BuildDoors()
    {
        foreach (GameObject d in DoorList)
        {
            foreach (GameObject w in WallList)
            {
                if (d != w && d.transform.position == w.transform.position)
                {
                    if(!d.GetComponent<OpenDoor>())
                        d.AddComponent<OpenDoor>();
                    d.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.8f, 0.4f, 0.0f, 1.0f));
                    GameObject.Destroy(d.GetComponent<NavMeshObstacle>());
                    d.isStatic = false;
                    if(!w.GetComponent<OpenDoor>())
                        w.AddComponent<OpenDoor>();
                    GameObject.Destroy(w.GetComponent<NavMeshObstacle>());
                    w.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.8f, 0.4f, 0.0f, 1.0f));
                    w.name = "Door";
                }
            }

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

}
