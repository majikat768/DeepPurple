using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public enum Generator
    {
        LINEAR, RANDOM, SQUARE
    }

    public void Start()
    {
        generateLevel(Generator.RANDOM);
    }

    public void generateLevel(Generator generator)
    {
        GameObject levelGen = new GameObject();
        levelGen.AddComponent<RoomGenerator>();
        RoomGenerator roomGenerator = levelGen.GetComponent<RoomGenerator>();
        switch (generator)
        {
            case Generator.SQUARE:
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Room r;
                        if (i == j && j == 0)
                        {
                            r = roomGenerator.Get(new Vector3(16 * i, 0, 16 * j), RoomGenerator.RoomType.Start);
                        }
                        else if (i == j && j == 4)
                        {
                            r = roomGenerator.Get(new Vector3(16 * i, 0, 16 * j), RoomGenerator.RoomType.Boss);
                        }
                        else
                        {

                            r = roomGenerator.Get(new Vector3(16 * i, 0, 16 * j), RandomRoomType());
                        }
                    }
                }
                break;
            case Generator.LINEAR:
                for (int i = 0; i < 25; i++)
                {
                    Room r;
                    if (i == 0)
                    {
                        r = roomGenerator.Get(new Vector3(16 * i, 0, 0), RoomGenerator.RoomType.Start);
                    }
                    else if (i == 24)
                    {
                        r = roomGenerator.Get(new Vector3(16 * i, 0, 0), RoomGenerator.RoomType.Boss);
                    }
                    else
                    {
                        r = roomGenerator.Get(new Vector3(16 * i, 0,0), RandomRoomType());
                        r.SetSize(new Vector3(16.0f,4.0f,Random.Range(8,24)));
                    }
                }
                break;
            case Generator.RANDOM:
                List<Vector2Int> offsets = new List<Vector2Int>();
                offsets.Add(new Vector2Int(0, 0)); // Spawn room

                int numLoops = Random.Range(25, 50);
                for (int i = 0; i < numLoops; i++)
                {
                    bool foundEmpty = false;
                    while (!foundEmpty)
                    {
                        Vector2Int vector2Int = offsets[Random.Range(0, offsets.Count)]; // Pick random offset from list
                        // Pick a random direction
                        int dir = Random.Range(0, 4); // 0 North, 1 East, 2 South, 3 West
                        Vector2Int newLoc = new Vector2Int(vector2Int.x, vector2Int.y);
                        switch (dir)
                        {
                            case 0:
                                newLoc.y += 1;
                                break;
                            case 1:
                                newLoc.x += 1;
                                break;
                            case 2:
                                newLoc.y -= 1;
                                break;
                            case 3:
                                newLoc.x -= 1;
                                break;
                        }
                        if (!offsets.Contains(newLoc))
                        {
                            offsets.Add(newLoc);
                            foundEmpty = true;
                        }
                    }
                }
                for (int i = 0; i < offsets.Count; i++)
                {
                    Vector2Int vector2Int = offsets[i];
                    Room r;
                    if (vector2Int.x == 0 && vector2Int.y == 0)
                    {
                        r = roomGenerator.Get(new Vector3(0, 0, 0), RoomGenerator.RoomType.Start);
                    }
                    else if (i == offsets.Count - 1)
                    {
                        r = roomGenerator.Get(new Vector3(16 * vector2Int.x, 0, 16 * vector2Int.y), RoomGenerator.RoomType.Boss);
                    }
                    else
                    {
                        r = roomGenerator.Get(new Vector3(16 * vector2Int.x, 0, 16 * vector2Int.y), RandomRoomType());
                    }
                }
                break;
        }
        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
    }

    public RoomGenerator.RoomType RandomRoomType()
    {
        RoomGenerator.RoomType rt;
        float rand = Random.Range(0f, 1.0f);
        if (rand < 0.6)
        {
            rt = RoomGenerator.RoomType.Combat;
        }
        else if (rand < 0.9)
        {
            rt = RoomGenerator.RoomType.Puzzle;
        }
        else
        {
            rt = RoomGenerator.RoomType.Treasure;
        }
        return rt;
    }

}
