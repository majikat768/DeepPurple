using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {

    GameObject player;
    float BounceForce; 

	// Use this for initialization
	void Start () {
	    player = GameObject.FindWithTag("Player");	
        BounceForce = player.GetComponent<Rigidbody>().mass;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            Vector3 velo = playerRB.velocity;
            playerRB.velocity = Vector3.zero;
            playerRB.AddForce(new Vector3(BounceForce,-velo.y*BounceForce,0),ForceMode.Impulse);
        }
    }
}
