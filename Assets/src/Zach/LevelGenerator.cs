using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public const int roomSize = 32;

    public enum Generator
    {
        LINEAR, RANDOM, SQUARE, TFRACTAL
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
                        GameObject r;
                        if (i == j && j == 0)
                        {
                            r = RoomGenerator.Get(new Vector3(roomSize * i, 0, roomSize * j), RoomGenerator.RoomType.Start);
                        }
                        else if (i == j && j == 4)
                        {
                            r = RoomGenerator.Get(new Vector3(roomSize * i, 0, roomSize * j), RoomGenerator.RoomType.Boss);
                        }
                        else
                        {

                            r = RoomGenerator.Get(new Vector3(roomSize * i, 0, roomSize * j), RandomRoomType());
                        }
                    }
                }
                break;
            case Generator.LINEAR:
                for (int i = 0; i < 25; i++)
                {
                    GameObject r;
                    if (i == 0)
                    {
                        r = RoomGenerator.Get(new Vector3(roomSize * i, 0, 0), RoomGenerator.RoomType.Start);
                    }
                    else if (i == 24)
                    {
                        r = RoomGenerator.Get(new Vector3(roomSize * i, 0, 0), RoomGenerator.RoomType.Boss);
                    }
                    else
                    {
                        r = RoomGenerator.Get(new Vector3(roomSize * i, 0,0), RandomRoomType());
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
                    GameObject r;
                    if (vector2Int.x == 0 && vector2Int.y == 0)
                    {
                        r = RoomGenerator.Get(new Vector3(0, 0, 0), RoomGenerator.RoomType.Start);
                    }
                    else if (i == offsets.Count - 1)
                    {
                        r = RoomGenerator.Get(new Vector3(roomSize * vector2Int.x, 0, roomSize * vector2Int.y), RoomGenerator.RoomType.Boss);
                    }
                    else
                    {
                        r = RoomGenerator.Get(new Vector3(roomSize * vector2Int.x, 0, roomSize * vector2Int.y), RandomRoomType());
                    }
                }
                break;
            case Generator.TFRACTAL:
                TFractal(roomGenerator);
                break;
        }
        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
    }

    public void TFractal(RoomGenerator roomGenerator)
    {
        int startingLength = 10;
        RoomGenerator.Get(new Vector3(0, 0, 0), RoomGenerator.RoomType.Start);
        TFractalRecursive(roomGenerator, 0, 1, 0, startingLength);
    }

    public void TFractalRecursive(RoomGenerator roomGenerator, int x, int y, int direction, int length)
    {
        if (length < 1)
        {
            return;
        }
        for (int i = 0; i < length; i++)
        {
            RoomGenerator.Get(new Vector3(x * roomSize, 0, y * roomSize), RandomRoomType());
            switch (direction)
            {
                case 0:
                    y++;
                    break;
                case 1:
                    x++;
                    break;
                case 2:
                    y--;
                    break;
                case 3:
                    x--;
                    break;
            }
        }
        int newLeft = (direction + 3) % 4;
        int newRight = (direction + 1) % 4;
        TFractalRecursive(roomGenerator, x, y, newLeft, (int) (length * Random.Range(0.6f, 0.9f)));
        TFractalRecursive(roomGenerator, x, y, newRight, (int)(length * Random.Range(0.6f, 0.9f)));
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
