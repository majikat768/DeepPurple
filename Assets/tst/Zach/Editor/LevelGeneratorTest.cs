/* LevelGeneratorTest.cs
 * Programmer: Zach Sugano
 * Description: This file is used for unit testing on the level generator
 * to ensure that it is behaving correctly. This utilizes the Unity unit
 * test framework so tests can be done without actually starting the game.
 * 
 * This is important because it takes quite a fair bit of time to actually
 * instantiate all of the rooms in a level once it has been generated.
 * It wouldn't be feasible to instantiate every single room. Instead we just
 * generate the dictionary for a random level and perform tests on the dictionary.
 * 
 * You can read more about how the level generator works in the file "LevelGenerator.cs"
 * 
 */
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class LevelGeneratorTest {

    /// <summary>
    /// Generates a random level using the TFractal generator and checks
    /// to ensure that a start room is located somewhere within the level.
    /// Test fails if it is unable to locate a start room.
    /// </summary>
	[Test]
	public void LevelGeneratorTFractalHasStart() {
        var rooms = GeneratorType.TFRACTAL.GetGenerator().GetRooms();
        bool foundStart = false;
        foreach (KeyValuePair<Vector2Int, RoomGenerator.RoomType> room in rooms)
        {
            RoomGenerator.RoomType roomType = room.Value;
            if (roomType == RoomGenerator.RoomType.Start)
            {
                foundStart = true;
            }
        }

        Assert.IsTrue(foundStart);
	}

    /// <summary>
    /// Generates a random level using the TFractal generator and checks
    /// to ensure that a boss room is located somewhere within the level.
    /// Test fails if it is unable to lcoate a boss room.
    /// </summary>
    [Test]
    public void LevelGeneratorTFractalHasBoss()
    {
        var rooms = GeneratorType.TFRACTAL.GetGenerator().GetRooms();
        bool foundBoss = false;
        foreach (KeyValuePair<Vector2Int, RoomGenerator.RoomType> room in rooms)
        {
            RoomGenerator.RoomType roomType = room.Value;
            if (roomType == RoomGenerator.RoomType.Boss)
            {
                foundBoss = true;
            }
        }

        Assert.IsTrue(foundBoss);
    }

    /// <summary>
    /// Generates a random level using the TFractal generator and checks
    /// to ensure that there is a winnable path betweeen the start room
    /// and the boss room. This is done using an iterative depth first search
    /// which starts at (0, 0). This location is assumed to be the start room
    /// for the level.
    /// This test fails if there is no found path between the start room and
    /// the boss room.
    /// </summary>
    [Test]
    public void LevelGeneratorTFractalHasWinnablePath()
    {
        var rooms = GeneratorType.TFRACTAL.GetGenerator().GetRooms();

        var visited = new List<Vector2Int>();
        var stack = new Stack<Vector2Int>();
        stack.Push(new Vector2Int(0, 0));

        while (stack.Count != 0)
        {
            var vertex = stack.Pop();
            
            if (!visited.Contains(vertex))
            {
                visited.Add(vertex);
            }

            var possibleLocs = new List<Vector2Int>();

            possibleLocs.Add(new Vector2Int(vertex.x, vertex.y + 1)); // North
            possibleLocs.Add(new Vector2Int(vertex.x, vertex.y - 1)); // South
            possibleLocs.Add(new Vector2Int(vertex.x + 1, vertex.y)); // East
            possibleLocs.Add(new Vector2Int(vertex.x - 1, vertex.y)); // West

            possibleLocs.RemoveAll(x => !rooms.ContainsKey(x));

            foreach (Vector2Int vec in possibleLocs)
            {
                if (rooms[vec] == RoomGenerator.RoomType.Boss)
                {
                    return; // Path has been found exit function
                }
                if (!visited.Contains(vec))
                {
                    stack.Push(vec);
                }
            }
        }

        Assert.Fail();
    }

    /// <summary>
    /// Generates a random level using the TFractal generator and checks to ensure
    /// that the boss room is located at the end of a corridor such that there is
    /// never a situation where you need to pass through the boss room in order
    /// to reach some other room.
    /// This test fails if the boss room is not found in the generated level or
    /// the boss room has two adjacent rooms to it. You are able to tell which
    /// issue generated the failure case based off of whether or not it printed
    /// the vertex location of the boss room.
    /// </summary>
    [Test]
    public void LevelGeneratorTFractalBossRoomAtEndOfCorridor()
    {
        var rooms = GeneratorType.TFRACTAL.GetGenerator().GetRooms();
        var vertex = new Vector2Int(0, 0);
        foreach (KeyValuePair<Vector2Int, RoomGenerator.RoomType> room in rooms)
        {
            if (room.Value == RoomGenerator.RoomType.Boss)
            {
                vertex = room.Key;
            }
        }
        // No boss room found
        if (vertex.x == 0 && vertex.y == 0) Assert.Fail();
        var possibleLocs = new List<Vector2Int>();
        possibleLocs.Add(new Vector2Int(vertex.x, vertex.y + 1));
        possibleLocs.Add(new Vector2Int(vertex.x, vertex.y - 1));
        possibleLocs.Add(new Vector2Int(vertex.x + 1, vertex.y));
        possibleLocs.Add(new Vector2Int(vertex.x - 1, vertex.y));
        // Remove all rooms that are not in the level dictionary
        possibleLocs.RemoveAll(x => !rooms.ContainsKey(x));
        // Fail test if there is more than one adjacent room
        if (possibleLocs.Count > 1)
        {
            Debug.Log(vertex);
            Debug.Log(possibleLocs[0]);
            Debug.Log(possibleLocs[1]);
            Assert.Fail();
        }
    }

    /// <summary>
    /// Performs 1000 iterations of the previous tests to ensure that all of
    /// the possible generated levels will pass all of the tests. While this
    /// test in and of itself does not definitely prove that an invalid level will
    /// never be generated, I have ran tens of thousands of iterations of the
    /// most recent version of the level generation algorithm and have not experienced
    /// a fail scenario. This means that greater than 99.997% of all levels generated
    /// will pass the required tests for a level to be playable.
    /// </summary>
    [Test]
    public void LevelGeneratorTFractal1000IterationTest() {
	    for (int i = 0; i < 1000; i++) {
		    LevelGeneratorTFractalHasStart();
		    LevelGeneratorTFractalHasBoss();
		    LevelGeneratorTFractalBossRoomAtEndOfCorridor();
		    LevelGeneratorTFractalHasWinnablePath();
	    }
    }

}
