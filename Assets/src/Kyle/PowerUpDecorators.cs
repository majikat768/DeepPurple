/*	PowerUpDecorators.cs
 *	Name: Kyle Hild
 *	Description: This is the Decorator Classes to decorate the items
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRunSpeed : PowerUpDecoractor 
{

	private float m_FreeRunningSpeed = 3f;

	public AddRunSpeed(PowerUpComponent BaseComponent) : base(BaseComponent) {}
	//These Override function override the function in the Decoractor class and adds their variables to the PowerUp
	public override float GetFreeRunSpeed()
	{
		return base.GetFreeRunSpeed () + m_FreeRunningSpeed;
	}
}

public class AddJumpHeight : PowerUpDecoractor 
{
	private float m_JumpHeight = 2.0f;

	public AddJumpHeight(PowerUpComponent BaseComponent) : base(BaseComponent) {}

	public override float GetJumpHeight()
	{
		return base.GetJumpHeight () + m_JumpHeight;
	}
}
	
public class AddSprintSpeed : PowerUpDecoractor 
{
	private float m_SprintSpeed = 3f;

	public AddSprintSpeed(PowerUpComponent BaseComponent) : base(BaseComponent) {}

	public override float GetSprintSpeed()
	{
		return base.GetSprintSpeed () + m_SprintSpeed;
	}
}
	