using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all this does is moves the camera's following distance to 
// in front of the player character, making it appear as first person.
//
// some other changes that could be made (in Update() when toggling)
// could be changing the camera's field of view, or height, to make it look better

public class PerspectiveChange : MonoBehaviour {

    Camera cam;
    KeyCode toggle = KeyCode.Z;
    float maxZoomOut;
    float maxZoomIn;
    bool firstPerson = false;

	void Start () {
        cam = Camera.main;
        maxZoomOut = cam.GetComponent<vThirdPersonCamera>().defaultDistance;
        maxZoomIn = -1;
		
	}
	
    // When player hits toggle button (Z), 
    // change the vThirdPersonCamera component's follow distance to -1.
    // this puts the camera slightly in front of the player character. 
	void Update () {
        if(Input.GetKeyDown(toggle)) {
            if(!firstPerson) {
                cam.GetComponent<vThirdPersonCamera>().defaultDistance = maxZoomIn;
                firstPerson = true;
            }
            else {
                cam.GetComponent<vThirdPersonCamera>().defaultDistance = maxZoomOut;
                firstPerson = false;
            }
        }

        cam.GetComponent<vThirdPersonCamera>().defaultDistance -= Input.mouseScrollDelta.y;
        if(cam.GetComponent<vThirdPersonCamera>().defaultDistance > maxZoomOut*2) {
            cam.GetComponent<vThirdPersonCamera>().defaultDistance = maxZoomOut*2;
            firstPerson = false;
        }
        if(cam.GetComponent<vThirdPersonCamera>().defaultDistance < maxZoomIn) {
            cam.GetComponent<vThirdPersonCamera>().defaultDistance = maxZoomIn;
            firstPerson = true;
        }
		
	}
}
