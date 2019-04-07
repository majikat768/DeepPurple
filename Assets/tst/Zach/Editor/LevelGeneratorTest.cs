using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class LevelGeneratorTest {

	[Test]
	public void LevelGeneratorTFractalHasStart() {
        LevelGenerator lg = LevelGenerator.Instance;
        var rooms = lg.GetRooms(LevelGenerator.Generator.TFRACTAL);
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

    [Test]
    public void LevelGeneratorTFractalHasBoss()
    {
        LevelGenerator lg = LevelGenerator.Instance;
        var rooms = lg.GetRooms(LevelGenerator.Generator.TFRACTAL);
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

    [Test]
    public void LevelGeneratorTFractalHasWinnablePath()
    {
        LevelGenerator lg = LevelGenerator.Instance;
        var rooms = lg.GetRooms(LevelGenerator.Generator.TFRACTAL);

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

    //[Test]
    //public void 

    [Test]
    public void LevelGeneratorTFractal100IterationTest() {
	    for (int i = 0; i < 100; i++) {
		    LevelGeneratorTFractalHasStart();
		    LevelGeneratorTFractalHasBoss();
		    LevelGeneratorTFractalHasWinnablePath();
	    }
    }

}
