using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * using blocks (cube object) to build walls.
 * also placing blocks randomly, for now to make level appearance more interesting...
 * in the future they'll be replaced with items, aliens, and maybe blocks still too I don't know.
 * ----
 * input size variable in editor to set room size (default = 16)
 * this script, attached to an empty object, builds a room with that object's coordinates as (0,0,0).
 * 
 
 * TODO:
 * * add room connections, e.g. doors.....
 * * Or find fancier way to provide room connections for Level Generator
 * * add player / items / enemy, once they're available
 * * make floors and walls look prettier.
 * * .....
 * 
 */ 

public class RoomGenerator : MonoBehaviour
{
    public Room room;
    public GameObject BlockPrefab;
    public GameObject GroundPrefab;
    public GameObject WallPrefab;
    public static GameObject Block;
    public static GameObject Ground;
    public static GameObject Wall;
    public static GameObject Player;
    public static int size = 16;
    public enum RoomType { Start, Boss, Treasure, Puzzle, Combat };
    public RoomType rt;

    // Start is called before the first frame update
    void Start()
    {
        rt = RoomType.Puzzle;
        Block = BlockPrefab;
        Ground = GroundPrefab;
        Wall = WallPrefab;
        GetRoom(this.transform.position);
    }

    void GetRoom(Vector3 Zero)
    {
        room = new Room(Zero,rt);
    }
}
