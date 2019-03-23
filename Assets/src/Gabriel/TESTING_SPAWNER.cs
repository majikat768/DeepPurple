using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTING_SPAWNER : MonoBehaviour {

	public Rigidbody item;
	
	// Update is called once per frame
	void Update () {
		Rigidbody clone = Instantiate(item);
	}

	
}
