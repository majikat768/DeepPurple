/* GeneratorLinear.cs
 * Programmer: Zach Sugano
 * Description: Generator subclass for creating a simple line of rooms
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorLinear : Generator
{

    /// <summary>
    /// The linear pattern creates a 25x1 array of rooms each connected in a single
    /// continuous line with the start room at (0, 0) and the end room at (24, 0)
    /// </summary>
    /// <returns>Dictionary of coordinates and corresponding room types</returns>
    public override Dictionary<Vector2Int, RoomGenerator.RoomType> GetRooms()
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
}
