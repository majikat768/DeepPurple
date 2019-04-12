using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour {

    Camera mapCam;
    Camera mainCam;
    Rect mapLoc;
    KeyCode toggle;
    GameObject player;
    float camHeight = 12;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
        mapCam = new GameObject("map").AddComponent<Camera>();
		mapCam.orthographic = true;
        mapLoc = new Rect(0.1f,0.1f,0.3f,0.3f);
        toggle = KeyCode.M;
        player = GameObject.FindWithTag("Player");
        mapCam.orthographicSize = 24;
        mapCam.rect = mapLoc;
        mapCam.gameObject.transform.position = player.transform.position + new Vector3(0,camHeight,0);
        mapCam.gameObject.transform.rotation = Quaternion.Euler(90,0,0);

        mapCam.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(toggle)) mapCam.enabled = !mapCam.enabled;
        mapCam.gameObject.transform.position = player.transform.position + new Vector3(0,camHeight,0);
		
	}

    /*
    void OnGUI() {
        GUI.Label(new Rect(10,10,300,300),"Press M for map\n\nUse scroll wheel or press Z to adjust view");
    }
    */
}
