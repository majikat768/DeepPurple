using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour {

    GameObject player;
    PuzzleRoom R;

	// Use this for initialization
	void Start () {
        R = this.transform.parent.GetComponent<PuzzleRoom>();
        Debug.Log(R);
        player = GameObject.FindWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            R.solved = true;
            R.PlaySolvedSound();
        }
    }
}
