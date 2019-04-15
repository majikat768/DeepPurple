/* GeneratorLinearTest.cs
 * Programmer: Zach Sugano
 * Description: Generator subclass for creating a very simple test level.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorLinearTest : Generator {

	/// <summary>
    /// Simple fast generator used for game testing purposes. Not intended to be
    /// used in actual game play. Just generates a start room and 3 combat rooms in
    /// a row. This level does not have a boss room and is not beatable. We use
    /// this system to ensure that nav meshes get baked into our test levels.
    /// </summary>
    /// <returns>Dictionary of coordinates and corresponding room types</returns>
    public override Dictionary<Vector2Int, RoomGenerator.RoomType> GetRooms() {
	var dictionary = new Dictionary<Vector2Int, RoomGenerator.RoomType>();
	dictionary[new Vector2Int(0, 0)] = RoomGenerator.RoomType.Start;
	dictionary[new Vector2Int(0, 1)] = RoomGenerator.RoomType.Combat;
	dictionary[new Vector2Int(0, 2)] = RoomGenerator.RoomType.Combat;
	dictionary[new Vector2Int(0, 3)] = RoomGenerator.RoomType.Combat;
	return dictionary;
    }

}
