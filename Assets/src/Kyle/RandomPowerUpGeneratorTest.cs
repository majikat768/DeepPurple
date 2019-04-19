using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPowerUpGeneratorTest : MonoBehaviour 
{
	GameObject Item;
	private bool item1 = false;
	private bool item2 = false;
	private bool item3 = false;
	private bool fps = true;

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
