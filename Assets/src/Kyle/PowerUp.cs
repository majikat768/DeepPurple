﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public float RunSpeed;
	public float JumpHeight;
	public float Sprint;
	public GameObject hud;
	public Invector.CharacterController.vThirdPersonController UI; 



	// Use this for initialization
	void Start () 
	{
		Debug.Log ("Making Item");
		hud = GameObject.FindWithTag ("Player");
		if (hud != null) {
			Debug.Log ("I AM NOT NULL");
			UI = hud.GetComponent<Invector.CharacterController.vThirdPersonController>();
		} else {
			Debug.Log ("HUD ID NULL");
		}	
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

		RunSpeed = Powerup.GetFreeRunSpeed ();
		JumpHeight = Powerup.GetJumpHeight();
		Sprint = Powerup.GetSprintSpeed();


		Debug.Log("Item Made");
	}
		

	IEnumerator Effect()
	{
		Debug.Log ("Is this being Called?");
		float timer = 10;
		float OriginalHeight = 4;
		float OriginalRunningSpeed = 3;
		float OriginalSprint = 4;

		UI.jumpHeight = UI.jumpHeight + JumpHeight;
		UI.freeRunningSpeed = UI.freeRunningSpeed + RunSpeed;
		UI.freeSprintSpeed = UI.freeSprintSpeed + Sprint;
		yield return new WaitForSeconds (10);

		UI.jumpHeight = OriginalHeight;
		UI.freeRunningSpeed = OriginalRunningSpeed;
		UI.freeSprintSpeed = OriginalSprint;
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			StartCoroutine (Effect ());
			transform.position = new Vector3 (0, 50, 0);
		}
		Debug.Log ("After OnTriggerEffect This being Called?");
	}
}
