/*
*  TESTING_PASSFAIL.cs
*  Programmer: Gabriel Hasenoehrl
*  Description: Used for test scene to determine if the fps drops below
*  a certain point. (Drops below 5fps)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTING_PASSFAIL : MonoBehaviour 
{
	
	// Update is called once per frame
	void Update () 
	{
		StartCoroutine(TestingCo());
	}

	public IEnumerator TestingCo()
	{
	 	yield return new WaitForSeconds(1);
		 if((1.0f / Time.deltaTime) < 5)
		 {
			 Debug.Log("Fail");
		 }
		 else
		 {
			 Debug.Log("Pass");
		 }
	}
}
