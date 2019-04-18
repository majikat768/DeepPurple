using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase {
	public static ItemDatabase instance = new ItemDatabase();
	//Using Singleton pattern to create one instance of this database
	void Awake()
	{
		if (instance != null) 
		{
			Debug.Log ("Already and instance");
			return;
		}
			instance = this;
	}
	//Variable Declarations
	public GameObject[] AllItems = Resources.LoadAll<GameObject>("Kyle/Items");
	public GameObject InvulnPotRef = Resources.Load<GameObject>("Kyle/Items/Invulnerability");

	//Methods
	public GameObject RandomItemGrabber()
	{
		GameObject i = AllItems[Random.Range(0,AllItems.Length)];
			return i;
	}
}

