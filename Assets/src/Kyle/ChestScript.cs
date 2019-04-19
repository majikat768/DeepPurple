using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {
	private GameObject Player;

	// Use this for initialization
	void Start () 
	{
		Player = GameObject.FindWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player) 
		{
			int i = 1;
			for (int j = 0; j < 1; j++) 
			{
				GameObject Item;
				Item = ItemDatabase.instance.RandomPowerupGrabber ();
				GameObject.Instantiate(Item, this.transform.position +new Vector3(0,1,0), Quaternion.identity);
			}
		}
	}

}
