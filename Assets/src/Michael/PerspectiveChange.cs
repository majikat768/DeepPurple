using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all this does is moves the camera's following distance to 
// in front of the player character, making it appear as first person.
//
// some other changes that could be made (in Update() when toggling)
// could be changing the camera's field of view, or height, to make it look better

public class PerspectiveChange : MonoBehaviour {

    vThirdPersonCamera cam;
    KeyCode toggle = KeyCode.Z;
    float maxZoomOut;
    float maxZoomIn;
    float maxFOV;
    float minFOV;
    bool firstPerson = false;

	void Start () {
        cam = Camera.main.GetComponent<vThirdPersonCamera>();
        cam.height = 1.8f;
        maxZoomOut = cam.defaultDistance*2;
        maxZoomIn = -1;
		
	}
	
    // When player hits toggle button (Z), 
    // change the vThirdPersonCamera component's follow distance to -1.
    // this puts the camera slightly in front of the player character. 
	void FixedUpdate () {
        if(Input.GetKeyDown(toggle)) {
            if(!firstPerson) {
                cam.defaultDistance = maxZoomIn;
                firstPerson = true;
            }
            else {
                cam.defaultDistance = maxZoomOut/2;
                firstPerson = false;
            }
        }

        cam.defaultDistance -= Input.mouseScrollDelta.y;
        if(cam.defaultDistance > maxZoomOut) {
            cam.defaultDistance = maxZoomOut;
            firstPerson = false;
        }
        else if(cam.defaultDistance < maxZoomIn) {
            cam.defaultDistance = maxZoomIn;
            firstPerson = true;
        }
        else
            Camera.main.fieldOfView += Input.mouseScrollDelta.y*3;

		
	}
}
