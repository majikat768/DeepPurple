/*	ItemManager.cs
 *	Name: Kyle Hild
 *	Description: The Item Manager is implemented to pass items too to essentially use the items
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour 
{
	//Variable Declarations
	public static ItemManager instance = new ItemManager();
	public GameObject hud;
	public Invector.CharacterController.vThirdPersonController UI;

	//Awake to find the Player 
	void Awake()
	{
		hud = GameObject.FindWithTag ("Player");
		if (hud != null) {
			Debug.Log ("I AM NOT NULL");
			UI = hud.GetComponent<Invector.CharacterController.vThirdPersonController>();
		} else {
			Debug.Log ("HUD ID NULL");
		}	
	}
	//Start a CoRoutine to start a timer on the Potion
	IEnumerator Effect(float OriginalHeight)
	{
		float timer = 10;
		UI.jumpHeight = 20;
		yield return new WaitForSeconds (10);
		UI.jumpHeight = OriginalHeight;
	}

	//Function to start the coroutine to use the potion
	public void UsePotion (Potion Pot)
	{
		float OriginalHeight = UI.jumpHeight;
		StartCoroutine (Effect (OriginalHeight));
	}

}
