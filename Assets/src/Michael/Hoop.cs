using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour {

    GameObject player;
    Inventory inventory;
    PuzzleRoom R;

	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        R = this.transform.parent.GetComponent<PuzzleRoom>();
        player = GameObject.FindWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            if(!R.solved) {
                inventory.incScore(8);
                R.PlaySolvedSound();
                R.solved = true;
            }
        }
    }
}
