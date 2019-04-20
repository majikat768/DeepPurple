/* LevelGenerator.cs
 * Programmer: Zach Sugano
 * Description: This level generation script uses random number generation to create the level.
 * This script is called once whenever the scene is loaded.
 * 
 * This level generation scheme uses an (x, y) integer vector space, denoted "V", where each coordinate
 * is linearly transformed into the (x, y, z) float vector space, denote "W". We are doing this from
 * a top down perspective, so the actual transformation, denoted "T", T : V -> W is written in the form
 * (roomSize * x, 0, roomSize * y). You will notice that the y coordinate in vector space W is always 0,
 * because that y coordinate represents vertical height and we are only generating a flat grid for each
 * room position.
 * 
 */
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : Singleton<LevelGenerator>
{
    // Prevent not singleton initialization
    protected LevelGenerator() { }

    [SerializeField]
    public GeneratorType defaultGenerator = GeneratorType.TFRACTAL;
    public const int roomSize = 32;


    public void Start()
    {
        LevelGenerator.Instance.generateLevel(defaultGenerator);
    }

    /// <summary>
    /// Generate the level using the passed in generator pattern.
    /// </summary>
    /// <param name="generator">The generator you're using to create the level</param>
    public void generateLevel(GeneratorType generatorType)
    {
        Generator generator = generatorType.GetGenerator();
        var rooms = generator.GetRooms();
        // Iterate over all rooms and create them using Room Generator
        foreach (KeyValuePair<Vector2Int, RoomGenerator.RoomType> room in rooms)
        {
            RoomGenerator.Get(new Vector3(roomSize * room.Key.x, 0, roomSize * room.Key.y), room.Value);
        }

        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
        Debug.Log(Time.realtimeSinceStartup.ToString() + " seconds to build rooms and bake nav mesh");

    }

}
