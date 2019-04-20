/*	TestItemSpawn.cs
 *	Name: Kyle Hild
 *	Description: This is to test the Invuln Potion in the singleton instance. If the potion instatiates it works.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemSpawn : MonoBehaviour {
	
	// Update is called once per frame
	void Update () 
	{
		GameObject.Instantiate (ItemDatabase.instance.InvulnPotRef);
	}
}
