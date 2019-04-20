/*	PowerUpDecoractor.cs
 *	Name: Kyle Hild
 *	Description: This is the main Decorator Class that decorates the powerup.
 */

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
		
	//Dynamic overridable functions to decorate
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
}
