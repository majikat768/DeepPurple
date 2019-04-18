using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour 
{
	public static ItemManager instance = new ItemManager();
	public GameObject hud;
	public Invector.CharacterController.vThirdPersonController UI;


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

	// Use this for initialization
	void Start () 
	{
		
	}

	IEnumerator Effect(float OriginalHeight)
	{
		float timer = 10;
		UI.jumpHeight = 20;
		yield return new WaitForSeconds (10);
		UI.jumpHeight = OriginalHeight;
	}


	public void UsePotion (Potion Pot)
	{
		float OriginalHeight = UI.jumpHeight;
		StartCoroutine (Effect (OriginalHeight));
/*		Debug.Log (" I MADE IT HERE ");
		UI.jumpHeight = 20;
		yield return new WaitForSeconds (10);
		//Debug.Log ("Seconds: " + time);
		UI.jumpHeight = OriginalHeight;
		*/
	}
	
	// Update is called once per fr	ame
	void Update () {
		
	}
}
