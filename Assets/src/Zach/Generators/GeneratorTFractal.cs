/* GeneratorTFractal.cs
 * Programmer: Zach Sugano
 * Description: Generator subclass for the main generator that is being used
 * for the game. Based off of a T Fractal pattern.
 * 
 */

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTFractal : Generator
{

    /// <summary>
    /// The TFractal room generator creates the level based on a recursive fractal
    /// algorithm. The fractal pattern is loosely based off of the T-branching fractal
    /// which is sometimes referred to as an H tree. The most basic form of the
    /// algorithm starts with a line segment of some length. Then draw two shorter
    /// line segments at right angles to the first line segment through the endpoint.
    /// Recursively apply this algorithm to create higher and higher levels.
    /// This algorithm has been modified and uses random number generation to create
    /// a variety of room patterns.
    /// </summary>
    /// <returns>Dictionary of coordinates and corresponding room types</returns>
    public override Dictionary<Vector2Int, RoomGenerator.RoomType> GetRooms()
    {
        var dictionary = new Dictionary<Vector2Int, RoomGenerator.RoomType>();
        int startingLength = 8;
        // Add starting room
        dictionary[new Vector2Int(0, 0)] = RoomGenerator.RoomType.Start;
        // Recursively create rooms
        TFractalRecursive(ref dictionary, 0, 1, 0, startingLength);

        // Identify where boss room should be located
        // This is done by attaching the boss room to a a random room of degree one
        // For the purposes of this particular function, degree refers to the number
        // of rooms surrounding the randomly selected room. We include rooms that
        // are one diagonal move away as well. The reason we do all of this is to
        // ensure that the boss room always ends up at the end of a corridor.
        // We want the player to be able to reach every possible room in the level
        // without fighting the boss until they're ready.
        // bossRoomLocs stores every coordinate where a boss room could be located.
        var bossRoomLocs = new List<Vector2Int>();
        // Iterate over every room that was generated
        foreach (KeyValuePair<Vector2Int, RoomGenerator.RoomType> room in dictionary)
        {
            // Never attach boss room to start room
            if (room.Value == RoomGenerator.RoomType.Start) continue;
            // possibleLocs stores every coordinate around the current room iteration
            var possibleLocs = new List<Vector2Int>();
            var vector = room.Key;

            // Add all coordinates North, South, East, West, North East, North West,
            // South East, and South West, 2 North, 2 South, 2 East, 2 West of the current room iteration
            possibleLocs.Add(new Vector2Int(vector.x, vector.y + 1));
            possibleLocs.Add(new Vector2Int(vector.x, vector.y - 1));
            possibleLocs.Add(new Vector2Int(vector.x + 1, vector.y));
            possibleLocs.Add(new Vector2Int(vector.x - 1, vector.y));
            possibleLocs.Add(new Vector2Int(vector.x + 1, vector.y + 1));
            possibleLocs.Add(new Vector2Int(vector.x - 1, vector.y + 1));
            possibleLocs.Add(new Vector2Int(vector.x + 1, vector.y - 1));
            possibleLocs.Add(new Vector2Int(vector.x - 1, vector.y - 1));
            possibleLocs.Add(new Vector2Int(vector.x, vector.y + 2));
            possibleLocs.Add(new Vector2Int(vector.x, vector.y - 2));
            possibleLocs.Add(new Vector2Int(vector.x + 2, vector.y));
            possibleLocs.Add(new Vector2Int(vector.x - 2, vector.y));

            // Remove any rooms from possibleLocs that are already in the dictionary
            // A room will already be put there, so the boss room can't go there.
            possibleLocs.RemoveAll(x => dictionary.ContainsKey(x));
            // Only attach boss room to rooms with degree 1
            // Degree zero would imply that the room is an "island" with no
            // connection to any of the other rooms. This shouldn't be possible.
            // This system checks 12 points around the target room and removes
            // any rooms that already exist in the dictionary. This first pass
            // filtering will get rid of any situation where the boss room
            // isn't at the end of a corridor.
            if (possibleLocs.Count > 9)
            {
                // This second pass filtering removes any possible locations that
                // are farther than 1 unit away from the room that the boss room
                // will be attached to. This is done to prevent the boss room
                // from being an island.
                possibleLocs.RemoveAll(x => Vector2Int.Distance(x, vector) > 1);
                bossRoomLocs.AddRange(possibleLocs);
            }
        }
        // Sort potential boss rooms by distance from start room
        // We do this to try to make sure the boss room is as far from the start as possible.
        bossRoomLocs = bossRoomLocs.OrderBy(x => -x.magnitude).ToList();
        // Possible boss room locations are now ordered from greatest distance to smallest
        // Take only the first 1/10th of the list to cut out possible boss room locations
        // that are close to the start room.
        int newSize = bossRoomLocs.Count / 10;
        // Set possible boss room locations to a sub range of the first newSize elements
        // or if that integer divison results in a new size of zero, just take the first
        // element in the list which represents that farthest possible distance a boss room
        // can be from the starting location.
        bossRoomLocs = bossRoomLocs.GetRange(0, newSize < 1 ? 1 : newSize);
        // Select a random room from all the possible boss room locations that we just calculated
        dictionary[bossRoomLocs[Random.Range(0, bossRoomLocs.Count)]] = RoomGenerator.RoomType.Boss;
        return dictionary;
    }

    /// <summary>
    /// This function implements the recursive fractal algorithm. The pattern is
    /// loosely based off of the T-branching fractal which is sometimes referred to as
    /// an H tree. Our implementation is far more random. The most basic form of the
    /// algorithm starts with a line segment of some length. Then draw two shorter
    /// line segments at right angles to the first line segment through the endpoint.
    /// Recursively apply this algorithm to create higher and higher levels.
    /// This algorithm has been modified and uses random number generation to create
    /// a variety of room patterns. What follows is an explanation of our algorithm:
    /// 
    /// Creating a line segment with the length parameter in the direction of 
    /// direction parameter. Calculate the direction of west and east relative
    /// to the direction that the line segment was originally moving. For example,
    /// if the original line segment was moving west, turning right would leave you
    /// facing north and turning left would leave you facing south. A simple modulus
    /// calculation is done to determine new direction. Once the new directions have
    /// been calulated, recursively repeat these steps by moving left, forward, and 
    /// right. The magnitude of each new line segment is calculated using direction 
    /// multiplied by a random number between 0.6 and 0.9. This helps keep the branches
    /// long. This differs from the original T-branching fractal in that:
    /// 1) We also move forward in addition to left and right
    /// 2) We calculate each new length using a random number generator
    /// Ultimately, this creates very interesting room layouts that tend to contain
    /// tunnels loops, corridors, and blocks all at the same time.
    /// </summary>
    /// <param name="dictionary">Reference to the dictionary that will be returned</param>
    /// <param name="x">The starting x position</param>
    /// <param name="y">The startiny y position</param>
    /// <param name="direction">The direction to create the line segment in</param>
    /// <param name="length">The length of the line segment</param>
    public void TFractalRecursive(ref Dictionary<Vector2Int, RoomGenerator.RoomType> dictionary, int x, int y, int direction, int length)
    {
        if (length < 1)
        {
            // Don't recurse if length is too small
            return;
        }
        for (int i = 0; i < length; i++)
        {
            var vec = new Vector2Int(x, y);
            // Dont overwrite a room that already exists
            if (!dictionary.ContainsKey(vec))
            {
                dictionary[vec] = RandomRoomType();
            }
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

}
