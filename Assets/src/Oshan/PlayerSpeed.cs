/*	OK_PlayerSpeed.cs
 *	Programmer's Name: Oshan Karki
 *	Description: This script calculates the player speed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;

public class PlayerSpeed : MonoBehaviour 
{
	public vThirdPersonMotor thirdPersonMotor;
	public float playerSpeed;

	void Start()
	{
		thirdPersonMotor = GetComponent<vThirdPersonMotor>();
	}

	void Update()
	{
		print("My speed is: " + playerSpeed);
		playerSpeed = thirdPersonMotor.speed;
	}
}