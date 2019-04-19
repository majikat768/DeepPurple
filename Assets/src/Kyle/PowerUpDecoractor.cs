using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PowerUpDecoractor : PowerUpComponent
{

	protected PowerUpComponent n_BaseComponent;

	public PowerUpDecoractor(PowerUpComponent BaseComponent)
	{
		n_BaseComponent = BaseComponent;
	}

	#region PowerUpComponent Members

    public virtual float GetSprintSpeed()
	{
		return n_BaseComponent.GetSprintSpeed();
	}
	public virtual float GetFreeRunSpeed()
	{
		return n_BaseComponent.GetFreeRunSpeed();
	}
	public virtual float GetJumpHeight()
	{
		return n_BaseComponent.GetJumpHeight ();
	}

	#endregion

}
