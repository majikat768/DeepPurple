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
public class RoomBuilder : MonoBehaviour
{
    public GameObject Block;
    public GameObject Ground;
    public GameObject Player;
    public int size = 16;

    // Start is called before the first frame update
    void Start()
    {
        //get zero coordinates of object:
        Vector3 Zero = this.transform.position;
        //build plane for floor, set to correct size:
        GameObject g = Instantiate(Ground, new Vector3(size / 2 + Zero.x, 0, size / 2 + Zero.z), Quaternion.identity);
        g.transform.localScale = new Vector3(size / 10.0f, 1, size / 10.0f);

        // build walls with blocks.
        // might be faster/better to use single block for each wall, then scale it to size...
        GameObject w;
        for (int i = 0; i < size; i++)
        {
            w = Instantiate(Block, new Vector3(Zero.x, 0.5f, Zero.z+i+0.5f), Quaternion.identity);
            w.transform.localScale = new Vector3(0.2f, 1, 1);
            w = Instantiate(Block, new Vector3(Zero.x+size, 0.5f, Zero.z+i+0.5f), Quaternion.identity);
            w.transform.localScale = new Vector3(0.2f, 1, 1);
            w = Instantiate(Block, new Vector3(Zero.x+i+0.5f, 0.5f, Zero.z), Quaternion.identity);
            w.transform.localScale = new Vector3(1, 1, 0.2f);
            w = Instantiate(Block, new Vector3(Zero.x+i+0.5f, 0.5f, Zero.z+size), Quaternion.identity);
            w.transform.localScale = new Vector3(1, 1, 0.2f);
        }

        // here I'm just putting blocks in random places, so it looks more interesting.
        for (int i = 0; i < size; i++)
        {
            Instantiate(Block, new Vector3(Zero.x+Random.Range(1, size - 1)+0.5f, 0.5f, Zero.z+Random.Range(1, size - 1)+0.5f), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
