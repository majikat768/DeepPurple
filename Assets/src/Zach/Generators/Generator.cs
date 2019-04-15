using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator {

	private GeneratorType GeneratorType;

	/// <summary>
    /// The square pattern currently just creates a 5x5 array of rooms with the start room
    /// located at (0, 0) and the boss room at (4, 4).
    /// </summary>
    /// <returns>Dictionary of coordinates and corresponding room types</returns>
	public virtual Dictionary<Vector2Int, RoomGenerator.RoomType> GetRooms() {
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

    /// <summary>
    /// Use random number generator to return a random room type.
    /// The Random.Range function utilizes a uniformly distributed number generator
    /// so we can set the probabilities for each room type just by generating
    /// a random float between 0 and 1 and checking which range it falls into
    /// </summary>
    /// <returns>A random room type</returns>
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
