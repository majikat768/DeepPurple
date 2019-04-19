using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoratortest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("b")) 
		{
			PowerUpComponent Powerup = new PowerUpConcreteComponent();
			Debug.Log("Basic Speed: " + Powerup.GetFreeRunSpeed());
		}

		if(Input.GetKeyDown("s"))
		{
			PowerUpComponent Powerrup = new PowerUpConcreteComponent();
			Powerrup = new AddRunSpeed(Powerrup);
			Debug.Log("Add Run Speed: " + Powerrup.GetFreeRunSpeed());
		}
		
	}
}
