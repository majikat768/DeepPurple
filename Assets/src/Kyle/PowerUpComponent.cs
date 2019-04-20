/*	PowerUpComponent.cs
 *	Name: Kyle Hild
 *	Description: This is the interface for the Decorator Pattern
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerUpComponent
{

	float GetSprintSpeed();
	float GetFreeRunSpeed();
	float GetJumpHeight();
}
