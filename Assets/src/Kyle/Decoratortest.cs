/*	Decoratortest.cs
 *	Name: Kyle Hild
 *	Description: This Script is used to test to make sure the Decorator Pattern is working as intended
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoratortest : MonoBehaviour {

	
	// When you hit b create a new powerup component and Debug The Speed Variable
	void Update () 
	{
		if (Input.GetKeyDown ("b")) 
		{
			PowerUpComponent Powerup = new PowerUpConcreteComponent();
			Debug.Log("Basic Speed: " + Powerup.GetFreeRunSpeed());
		}
		//When you hit "s" get a Powerup from the Decorator and print its speed
		if(Input.GetKeyDown("s"))
		{
			PowerUpComponent Powerrup = new PowerUpConcreteComponent();
			Powerrup = new AddRunSpeed(Powerrup);
			Debug.Log("Add Run Speed: " + Powerrup.GetFreeRunSpeed());
		}
		
	}
}
