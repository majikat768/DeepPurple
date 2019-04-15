using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorRandom : Generator {

	/// <summary>
    /// The random room generator generates the level without any type of design in mind.
    /// We begin at the start room coordinates (0, 0) and then begin the generation loop
    /// from there. Each loop, we select a random coordinate from the dictionary and move
    /// in a random direction (North, East, South, West). We then check if there is already
    /// a room at that location. If there is no room at that location, then add that room
    /// to the dictionary. If there is a room at that location, skip over that and try again.
    /// This system has the potential to generate very odd looking room layouts, but has a
    /// tendency to cluster all of the rooms in a reasonably square like structure.
    /// </summary>
    /// <returns>Dictionary of coordinates and corresponding room types</returns>
    public override Dictionary<Vector2Int, RoomGenerator.RoomType> GetRooms()
    {
        var dictionary = new Dictionary<Vector2Int, RoomGenerator.RoomType>();
        dictionary[new Vector2Int(0, 0)] = RoomGenerator.RoomType.Start;
        // Create a random number of rooms
        int numLoops = Random.Range(25, 50);
        for (int i = 0; i < numLoops; i++)
        {
            var keys = new List<Vector2Int>(dictionary.Keys);
            bool foundEmpty = false;
            // Keep searching for a new room location until one is found to guarantee correct number of rooms
            while (!foundEmpty)
            {
                // Get random vector from dictionary
                Vector2Int vector = keys[Random.Range(0, keys.Count)];
                // Pick a random direction
                // 0 North, 1 East, 2 South, 3 West
                int dir = Random.Range(0, 4); 
                // Calculate new location based on direction
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
                    // A room hasn't been added at this location yet, so add it!
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
}
