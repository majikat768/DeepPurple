using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map : MonoBehaviour {

    Camera mapCam;
    Camera mainCam;
    Rect mapLoc;
    KeyCode toggle;
    GameObject player;
    float camHeight = 48;

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
        player = GameObject.FindWithTag("Player");
        mapCam = new GameObject("map").AddComponent<Camera>();
		mapCam.orthographic = true;
        mapCam.orthographicSize = 24;
        mapLoc = new Rect(0.1f,0.1f,0.3f,0.3f);
        mapCam.rect = mapLoc;
        mapCam.gameObject.transform.position = player.transform.position + new Vector3(0,camHeight,0);
        mapCam.gameObject.transform.rotation = Quaternion.Euler(90,0,0);

        mapCam.enabled = false;

        toggle = KeyCode.M;
        GameObject PlayerPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        PlayerPoint.transform.position = mapCam.transform.position + new Vector3(0,-2,0);
        PlayerPoint.GetComponent<Renderer>().material.SetColor("_Color",new Color(0,1,0));
        PlayerPoint.transform.localScale = new Vector3(2,2,2);
        PlayerPoint.transform.parent = mapCam.gameObject.transform;

	}
	
	// Update is called once per frame
	void Update () {
        if(mapCam.enabled) {
            Vector3 playerPos = player.transform.position;

            mapCam.gameObject.transform.position = new Vector3(playerPos.x,camHeight,playerPos.z);
        }

        if(Input.GetKeyDown(toggle)) {
            mapCam.enabled = !mapCam.enabled;
        }
		
	}

    /*
    void OnGUI() {
        GUI.Label(new Rect(10,10,300,300),"Press M for map\n\nUse scroll wheel or press Z to adjust view");
    }
    */
}
