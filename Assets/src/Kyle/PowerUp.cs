/*	PowerUp.cs
 *	Name: Kyle Hild
 *	Description: This is attached to PowerUp items to create them using the decorator pattern from and apply the stat changes
 *	to the character
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	//Variable Declarations
	private float RunSpeed;
	private float JumpHeight;
	private float Sprint;
	private float OriginalHeight;
	private float OriginalRunningSpeed;
	private float OriginalSprint;
	private GameObject hud;
	private Invector.CharacterController.vThirdPersonController UI; 

	// Use this for initialization
	void Start () 
	{
		//Find the Player Object and set the UI to the script
		hud = GameObject.FindWithTag ("Player");
		if (hud != null) 
		{
			UI = hud.GetComponent<Invector.CharacterController.vThirdPersonController>();
		}
		//Get a Random Number from 0 - 4 and apply the appropriate decorator combination
		int PowerUpDecider = Random.Range (0, 4);
		PowerUpComponent Powerup = new PowerUpConcreteComponent();
		if (PowerUpDecider == 1) 
		{
			Powerup = new AddRunSpeed (Powerup);
		}
		if (PowerUpDecider == 2) 
		{
			Powerup = new AddJumpHeight (Powerup);
		}
		if (PowerUpDecider == 3) 
		{
			Powerup = new AddSprintSpeed (Powerup);
		}
		if (PowerUpDecider == 4) 
		{
			Powerup = new AddSprintSpeed (Powerup);
			Powerup = new AddJumpHeight (Powerup);
		}
		//Intialize the Variables fromt he decorator
		RunSpeed = Powerup.GetFreeRunSpeed ();
		JumpHeight = Powerup.GetJumpHeight();
		Sprint = Powerup.GetSprintSpeed();
	}
		
	//IEnumerator function to have a 10 second use
	IEnumerator Effect()
	{
		//Variable Declarations (Initialize to normal)
		float timer = 10;
		OriginalHeight = 4;
		OriginalRunningSpeed = 3;
		OriginalSprint = 4;
		//If There is no Current Effect from a powerup then apply the powerup
		if (UI.jumpHeight == OriginalHeight && UI.freeRunningSpeed == OriginalRunningSpeed && UI.freeSprintSpeed == OriginalSprint) 
		{
			UI.jumpHeight = UI.jumpHeight + JumpHeight;
			UI.freeRunningSpeed = UI.freeRunningSpeed + RunSpeed;
			UI.freeSprintSpeed = UI.freeSprintSpeed + Sprint;
			yield return new WaitForSeconds (10);
			UI.jumpHeight = OriginalHeight;
			UI.freeRunningSpeed = OriginalRunningSpeed;
			UI.freeSprintSpeed = OriginalSprint;
		}
		//Destroy the Game Object
		Destroy (gameObject);
	}
	//On Trigger Function if the is a Player then start the coroutine and move the object until its destroyed
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			StartCoroutine (Effect ());
			transform.position = new Vector3 (0, 50, 0);
		}
	}
}
