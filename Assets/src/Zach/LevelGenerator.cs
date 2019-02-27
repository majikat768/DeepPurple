using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] availableRooms;

    public void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            RoomGenerator.Get(new Vector3(16*i,0,0), RoomGenerator.RoomType.Combat);
        }
    }

    public void generateLevel()
    {

    }
}
