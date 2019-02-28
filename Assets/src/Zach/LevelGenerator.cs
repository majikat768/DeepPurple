using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] availableRooms;

    public void Start()
    {
        Room r;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                r = RoomGenerator.Get(new Vector3(16 * i, 0, 16 * j), RoomGenerator.RoomType.Combat);
                r.Generate();
            }
        }
        RoomGenerator.BuildDoors();
    }

    public void generateLevel()
    {

    }
}
