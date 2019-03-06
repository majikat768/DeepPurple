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

    void Awake()
    {
        Wall = Resources.Load<GameObject>("Michael/Wall");
        Door = Resources.Load<GameObject>("Michael/Door");
        Block = Resources.Load<GameObject>("Michael/Block");
        Floor = Resources.Load<GameObject>("Michael/Plane");
        Ceiling = Resources.Load<GameObject>("Michael/Plane");

        if (standalone)
        {
            Get(this.transform.position, rt);
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
        r.SetZero(Zero);
        r.Init();
        RoomList.Add(r);
        return r;
    }

    public static void BuildDoors()
    {
        foreach(Room r in RoomList)
        {
            foreach(GameObject d in r.DoorList)
            {
                foreach(Collider o in Physics.OverlapBox(d.transform.position,d.transform.localScale))
                {
                    if(o.name == "Door" && !r.DoorList.Contains(o.gameObject))
                    {
                        Debug.Log("door found");
                        d.GetComponent<OpenDoor>().Unlock();
                        o.gameObject.GetComponent<OpenDoor>().Unlock();
                        break;
                    }


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
    public Vector3 GetZero() { return this.Zero; }

}
