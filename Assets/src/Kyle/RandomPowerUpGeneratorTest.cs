/*	RandomPowerUpGeneratorTest.cs
 *	Name: Kyle Hild
 *	Description: Tests the FPS test fails is falls below 30 also test to make sure all the items in the random generator
 *	spawn. Passes if all items spaws atleast once
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPowerUpGeneratorTest : MonoBehaviour 
{
	//Variable Declaration
	GameObject Item;
	private bool item1 = false;
	private bool item2 = false;
	private bool item3 = false;
	private bool fps = true;
	//Fuction to test FPS
	private IEnumerator FPSTest()
	{
		yield return new WaitForSeconds (1);
		if (1.0 / Time.deltaTime < 30)
			{
				fps = false;
			}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Item = ItemDatabase.instance.RandomPowerupGrabber ();
		GameObject.Instantiate(Item, this.transform.position +new Vector3(0,1,0), Quaternion.identity);
		//If we find an Instantiated object then set the item variables to true
		if (GameObject.Find ("PowerUp2(Clone)")) 
		{
			item1 = true;
		}
		if (GameObject.Find ("PowerUp3(Clone)")) 
		{
			item2 = true;
		}
		if (GameObject.Find ("PowerUpPod(Clone)")) 
		{
			item3 = true;
		}
		//If user hits b start the FPS test and check varibles give test results
		if(Input.GetKeyDown("b"))
			StartCoroutine (FPSTest ());
		{
			if (item1 == true && item2 == true && item3 == true) 
			{
				Debug.Log ("All Items Spawn: Pass");
			} else {
				Debug.Log ("All Items Spawn: Fail");
			}
			if (fps == true) 
			{
				Debug.Log ("FPS Drop Below 30?: Pass");
			}else{
				Debug.Log ("FPS Drop Below 30?: Fail");	
			}
		}
	}
}
