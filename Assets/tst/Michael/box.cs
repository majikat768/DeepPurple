using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour {

    Animator animator;
	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        animator.SetBool("Attack",true);
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Fire1")) {
            animator.SetBool("Attack",true);
            Debug.Log("box");

        }
		
	}
}
