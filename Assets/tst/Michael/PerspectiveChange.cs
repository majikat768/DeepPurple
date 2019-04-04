using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all this does is moves the camera's following distance to 
// in front of the player character, making it appear as first person.
//
// some other changes that could be made (in Update() when toggling)
// could be changing the camera's field of view, or height, to make it look better

public class PerspectiveChange : MonoBehaviour {

    GameObject player;
    Camera cam;
    KeyCode toggle = KeyCode.Z;
    float thirdPersonDistance;
    bool firstPerson = false;

	void Start () {
        player = GameObject.FindWithTag("Player");
        cam = Camera.main;
        thirdPersonDistance = cam.GetComponent<vThirdPersonCamera>().defaultDistance;
		
	}
	
    // When player hits toggle button (Z), 
    // change the vThirdPersonCamera component's follow distance to -1.
    // this puts the camera slightly in front of the player character. 
	void Update () {
        if(Input.GetKeyDown(toggle)) {
            if(!firstPerson) {
                cam.GetComponent<vThirdPersonCamera>().defaultDistance = -0.5f;
                firstPerson = true;
            }
            else {
                cam.GetComponent<vThirdPersonCamera>().defaultDistance = thirdPersonDistance;
                firstPerson = false;
            }
        }

		
	}
}
