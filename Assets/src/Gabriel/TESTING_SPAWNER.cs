/*
*  TESTING_SPAWNER.cs
*  Programmer: Gabriel Hasenoehrl
*  Description: Used in the test case to spawn items until the game
*  can no longer handle the required items on screen. (Drops below 5fps)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTING_SPAWNER : MonoBehaviour 
{

	public Rigidbody item;
	
	// Update is called once per frame
	void Update () 
	{
		Rigidbody clone = Instantiate(item);
	}

	
}
