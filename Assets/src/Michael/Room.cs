using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    //Block and Ground are just Unity objects- cube and plane.  might change this later

    public Room(Vector3 Zero, RoomGenerator.RoomType rt)
    {
        Debug.Log(rt);
        this.Init(Zero);
    }
    public void Init(Vector3 Zero)
    {
        //get zero coordinates of object:
        // instantiate plane for floor; set to correct size (default plane size is 10 units)
        GameObject g = GameObject.Instantiate(RoomGenerator.Ground, new Vector3(RoomGenerator.size / 2 + Zero.x, 0, RoomGenerator.size / 2 + Zero.z), Quaternion.identity);
        g.transform.localScale = new Vector3(RoomGenerator.size / 10.0f, 1, RoomGenerator.size / 10.0f);

        //build walls 
        GameObject walls = new GameObject("walls");
        GameObject NorthWall = GameObject.Instantiate(RoomGenerator.Wall, new Vector3(Zero.x + RoomGenerator.size / 2, 0.5f, Zero.z + RoomGenerator.size), Quaternion.identity);
        NorthWall.transform.parent = walls.transform;
        GameObject EastWall = GameObject.Instantiate(RoomGenerator.Wall, new Vector3(Zero.x + RoomGenerator.size, 0.5f, Zero.z + RoomGenerator.size/2), Quaternion.Euler(new Vector3(0,90.0f,0)));
        EastWall.transform.parent = walls.transform;
        GameObject SouthWall = GameObject.Instantiate(RoomGenerator.Wall, new Vector3(Zero.x + RoomGenerator.size / 2, 0.5f, Zero.z), Quaternion.identity);
        SouthWall.transform.parent = walls.transform;
        GameObject WestWall = GameObject.Instantiate(RoomGenerator.Wall, new Vector3(Zero.x, 0.5f, Zero.z + RoomGenerator.size/2), Quaternion.Euler(new Vector3(0,90.0f,0)));
        WestWall.transform.parent = walls.transform;

        // here I'm just putting blocks in random places to make it look more interesting.
        // I'll probably place instances of Kyle's Items class in the future instead
        GameObject items = new GameObject("items");
        for(int i = 0; i < RoomGenerator.size; i++)
        {
            GameObject b = GameObject.Instantiate(RoomGenerator.Block, new Vector3(Zero.x + Random.Range(1, RoomGenerator.size - 1) +0.5f, 0.5f, Zero.z + Random.Range(1, RoomGenerator.size - 1) + 0.5f), Quaternion.identity);
            b.transform.parent = items.transform;
        }

    }
}
