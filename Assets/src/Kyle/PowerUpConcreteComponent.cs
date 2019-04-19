using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpConcreteComponent : PowerUpComponent {

	private float FreeSprintSpeed = 0;
	private float FreeRunningSpeed = 0;
	private float JumpHeight = 0;

	public float GetSprintSpeed()
	{
		return FreeSprintSpeed;
	}

	public float GetFreeRunSpeed()
	{
		return FreeRunningSpeed;
	}
	public float GetJumpHeight()
	{
		return JumpHeight;
	}

}