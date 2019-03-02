using System.Collections.Generic;
using UnityEngine;

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
    public static List<GameObject> DoorList = new List<GameObject>();
    public static List<GameObject> WallList = new List<GameObject>();

    public Room room;
    public RoomType rt;
    private static Vector3 size = new Vector3(16.0f,4.0f,16.0f);

    // I don't think my script will be attached to any object, so I probably won't 
    // use Start(), but it's useful for testing
    void Start()
    {
        room = Get(transform.position,rt);
        // don't need to call setSize.  if you don't it's default 16x16
        room.SetSize(size.x,size.y,size.z); 
        room.Generate();
        BuildDoors();
    }

    public static Room Get(Vector3 Zero, RoomType rt)
    {
        Room r;
        switch(rt)
        {
            case RoomType.Start:
                r = new StartRoom(Zero);
                break;
            case RoomType.Combat:
                r = new CombatRoom(Zero);
                break;
            case RoomType.Treasure:
                r = new TreasureRoom(Zero);
                break;
            default:
                r = new Room(Zero);
                break;
        }
        RoomList.Add(r);
        return r;
    }

    public static void BuildDoors()
    {
        foreach(GameObject d1 in DoorList)
        {
            foreach(GameObject d2 in DoorList)
            {
                if (d1 != d2 && d1.transform.position == d2.transform.position)
                {
                    d1.AddComponent<OpenDoor>();
                    d1.GetComponent<Renderer>().material.SetColor("_Color",new Color(0.8f,0.4f,0.0f,1.0f));
                }
            }

        }
    }

    public static Vector3 GetSize() { return size; }
    public static void SetSize(float x, float y, float z) {
        size = new Vector3(x,y,z);
    }

}
