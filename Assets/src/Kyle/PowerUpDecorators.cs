﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRunSpeed : PowerUpDecoractor {

	private float m_FreeRunningSpeed = 10.0f;

	public AddRunSpeed(PowerUpComponent BaseComponent) : base(BaseComponent) {}

	public override float GetFreeRunSpeed()
	{
		return base.GetFreeRunSpeed () + m_FreeRunningSpeed;
	}
}

public class AddJumpHeight : PowerUpDecoractor {

	private float m_JumpHeight = 40.0f;

	public AddJumpHeight(PowerUpComponent BaseComponent) : base(BaseComponent) {}

	public override float GetJumpHeight()
	{
		return base.GetJumpHeight () + m_JumpHeight;
		}
}
	
public class AddSprintSpeed : PowerUpDecoractor {

	private float m_SprintSpeed = 10.0f;

	public AddSprintSpeed(PowerUpComponent BaseComponent) : base(BaseComponent) {}

	public override float GetSprintSpeed()
	{
		return base.GetSprintSpeed () + m_SprintSpeed;
		}
}
	