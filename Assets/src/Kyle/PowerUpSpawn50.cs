using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn50 : MonoBehaviour 
{
	GameObject Item;
	private bool TestEffects;
	private float RunSpeed;
	private float JumpHeight;
	private float Sprint;
	private float OriginalHeight;
	private float OriginalRunningSpeed;
	private float OriginalSprint;
	private GameObject hud;
	private Invector.CharacterController.vThirdPersonController UI;

	void Start ()
	{
		TestEffects = true;
		OriginalHeight = 4;
		OriginalRunningSpeed = 3;
		OriginalSprint = 4;
		hud = GameObject.FindWithTag ("Player");

		if (hud != null) {
			UI = hud.GetComponent<Invector.CharacterController.vThirdPersonController> ();
		} else {
			Debug.Log ("I cannot find the player");
		}
		int i = 0;
		for (i = 0; i < 50; i++) 
		{
			Item = ItemDatabase.instance.RandomPowerupGrabber ();
			GameObject.Instantiate(Item, this.transform.position +new Vector3(0,1,0), Quaternion.identity);	
		}
	}
	void Update()
	{
		if (Input.GetKeyDown ("b")) 
		{
			//If All Effects are active the Fail
			if (UI.jumpHeight > OriginalHeight && UI.freeRunningSpeed > OriginalRunningSpeed && UI.freeSprintSpeed > OriginalSprint) 
			{
				TestEffects = false;
			}
			if (UI.jumpHeight > OriginalHeight && UI.freeRunningSpeed > OriginalRunningSpeed) 
			{
				TestEffects = false;
			}
			if (UI.jumpHeight > OriginalHeight && UI.freeSprintSpeed > OriginalSprint) 
			{
				TestEffects = false;
			}
			if (UI.freeRunningSpeed > OriginalRunningSpeed && UI.freeSprintSpeed > OriginalSprint) 
			{
				TestEffects = false;
			}

			if (TestEffects == false) 
			{
				Debug.Log ("Effects Test: FAIL");
			} else {
				Debug.Log ("Effects Test: PASS");
			}
		}
	}

}