/*	ChestScript.cs
 *	Name: Kyle Hild
 *	Description: This Chest script was used to attach to a cube item to test spawning of items.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour 
{
	//Variable Declarations
	private GameObject Player;

	//Initialize Player to find Player
	void Start () 
	{
		Player = GameObject.FindWithTag ("Player");
	}
		//When colliding with the Cube and is player Get Items
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player) 
		{
			int i = 1;
			for (int j = 0; j < 1; j++) 
			{
				GameObject Item;
				Item = ItemDatabase.instance.RandomPowerupGrabber ();
				GameObject.Instantiate(Item, this.transform.position +new Vector3(0,1,0), Quaternion.identity);
			}
		}
	}
}
