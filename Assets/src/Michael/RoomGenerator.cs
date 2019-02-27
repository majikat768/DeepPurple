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

    public Room room;
    public RoomType rt;
    public static int size = 16;

    // I don't think my script will be attached to any object, so I probably won't 
    // use Start(), but it's useful for testing
    void Start()
    {
        room = Get(transform.position,rt);
    }

    public static Room Get(Vector3 Zero, RoomType rt)
    {
        switch(rt)
        {
            case RoomType.Start:
                return new StartRoom(Zero);
            case RoomType.Combat:
                return new CombatRoom(Zero);
            case RoomType.Treasure:
                return new TreasureRoom(Zero);
            default:
                return new Room(Zero);
        }
    }
}
