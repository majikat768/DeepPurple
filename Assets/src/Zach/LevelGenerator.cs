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
        generateLevel(Generator.TFRACTAL);
    }

    public void generateLevel(Generator generator)
    {
        var rooms = GetRooms(generator);

        foreach (KeyValuePair<Vector2Int, RoomGenerator.RoomType> room in rooms)
        {
            RoomGenerator.Get(new Vector3(roomSize * room.Key.x, 0, roomSize * room.Key.y), room.Value);
        }

        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
        Debug.Log(Time.realtimeSinceStartup.ToString() + " seconds to build rooms and bake nav mesh");

    }

    public Dictionary<Vector2Int, RoomGenerator.RoomType> GetRooms(Generator generator)
    {
        switch (generator)
        {
            case Generator.SQUARE:
                return RoomGeneratorSquare();
            case Generator.LINEAR:
                return RoomGeneratorLinear();
            case Generator.RANDOM:
                return RoomGeneratorRandom();
            case Generator.TFRACTAL:
                return RoomGeneratorTFractal();
        }
        return null;
    }

    Dictionary<Vector2Int, RoomGenerator.RoomType> RoomGeneratorSquare()
    {
        var dictionary = new Dictionary<Vector2Int, RoomGenerator.RoomType>();
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                var vec = new Vector2Int(x, y);
                RoomGenerator.RoomType roomType;
                if (x == y && y == 0)
                {
                    roomType = RoomGenerator.RoomType.Start;
                }
                else if (x == y && y == 4)
                {
                    roomType = RoomGenerator.RoomType.Boss;
                }
                else
                {
                    roomType = RandomRoomType();
                }
                dictionary[vec] = roomType;
            }
        }
        return dictionary;
    }

    Dictionary<Vector2Int, RoomGenerator.RoomType> RoomGeneratorLinear()
    {
        var dictionary = new Dictionary<Vector2Int, RoomGenerator.RoomType>();

        for (int i = 0; i < 25; i++)
        {
            var vec = new Vector2Int(i, 0);
            RoomGenerator.RoomType roomType;
            if (i == 0)
            {
                roomType = RoomGenerator.RoomType.Start;
            }
            else if (i == 24)
            {
                roomType = RoomGenerator.RoomType.Boss;
            }
            else
            {
                roomType = RandomRoomType();
            }
            dictionary[vec] = roomType;
        }

        return dictionary;
    }

    Dictionary<Vector2Int, RoomGenerator.RoomType> RoomGeneratorRandom()
    {
        var dictionary = new Dictionary<Vector2Int, RoomGenerator.RoomType>();
        dictionary[new Vector2Int(0, 0)] = RoomGenerator.RoomType.Start;

        int numLoops = Random.Range(25, 50);
        for (int i = 0; i < numLoops; i++)
        {
            var keys = new List<Vector2Int>(dictionary.Keys);
            bool foundEmpty = false;

            while (!foundEmpty)
            {
                Vector2Int vector = keys[Random.Range(0, keys.Count)]; // Get random vector
                // Pick a random direction
                int dir = Random.Range(0, 4); // 0 North, 1 East, 2 South, 3 West
                Vector2Int newLoc = new Vector2Int(vector.x, vector.y);
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
                if (!dictionary.ContainsKey(newLoc))
                {
                    RoomGenerator.RoomType roomType;
                    if (i == numLoops - 1)
                    {
                        roomType = RoomGenerator.RoomType.Boss;
                    }
                    else
                    {
                        roomType = RandomRoomType();
                    }
                    dictionary[newLoc] = roomType;
                    foundEmpty = true;
                }
            }
        }

        return dictionary;
    }

    Dictionary<Vector2Int, RoomGenerator.RoomType> RoomGeneratorTFractal()
    {
        var dictionary = new Dictionary<Vector2Int, RoomGenerator.RoomType>();
        int startingLength = 8;
        dictionary[new Vector2Int(0, 0)] = RoomGenerator.RoomType.Start; // Add start
        TFractalRecursive(ref dictionary, 0, 1, 0, startingLength);

        var bossRoomLocs = new List<Vector2Int>();
        // Find all room with degree 1
        foreach (KeyValuePair<Vector2Int, RoomGenerator.RoomType> room in dictionary)
        {
            if (room.Value == RoomGenerator.RoomType.Start) continue;
            var possibleLocs = new List<Vector2Int>();
            var vector = room.Key;

            possibleLocs.Add(new Vector2Int(vector.x, vector.y + 1)); // North
            possibleLocs.Add(new Vector2Int(vector.x, vector.y - 1)); // South
            possibleLocs.Add(new Vector2Int(vector.x + 1, vector.y)); // East
            possibleLocs.Add(new Vector2Int(vector.x - 1, vector.y)); // West
            possibleLocs.Add(new Vector2Int(vector.x + 1, vector.y + 1)); // North East
            possibleLocs.Add(new Vector2Int(vector.x - 1, vector.y + 1)); // North West
            possibleLocs.Add(new Vector2Int(vector.x + 1, vector.y - 1)); // South East
            possibleLocs.Add(new Vector2Int(vector.x - 1, vector.y - 1)); // South West

            possibleLocs.RemoveAll(x => dictionary.ContainsKey(x)); // Remove any duplicates

            if (possibleLocs.Count > 6) // Only attach boss room to rooms with degree 1
            {
                bossRoomLocs.AddRange(possibleLocs);
            }
        }

        dictionary[bossRoomLocs[Random.Range(0, bossRoomLocs.Count)]] = RoomGenerator.RoomType.Boss;
        return dictionary;
    }

    public void TFractalRecursive(ref Dictionary<Vector2Int, RoomGenerator.RoomType> dictionary, int x, int y, int direction, int length)
    {
        if (length < 1)
        {
            return;
        }
        for (int i = 0; i < length; i++)
        {
            var vec = new Vector2Int(x, y);
            dictionary[vec] = RandomRoomType();
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
        TFractalRecursive(ref dictionary, x, y, newLeft, (int)(length * Random.Range(0.6f, 0.9f)));
        TFractalRecursive(ref dictionary, x, y, direction, (int)(length * Random.Range(0.6f, 0.9f)));
        TFractalRecursive(ref dictionary, x, y, newRight, (int)(length * Random.Range(0.6f, 0.9f)));
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
