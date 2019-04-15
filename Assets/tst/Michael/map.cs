using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map : MonoBehaviour {

    Camera mapCam;
    Camera mainCam;
    GameObject bossRoom;
    Rect mapLoc;
    KeyCode toggle;
    GameObject player;
    float camHeight = 48;
    GameObject pointer, pointerCanvas;
    RectTransform pointerRT;
    GameObject arrow;
    TextMeshProUGUI distance;

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

        pointerCanvas = GameObject.Instantiate(Resources.Load<GameObject>("Michael/PointerCanvas"));
        pointer = pointerCanvas.transform.Find("Pointer").gameObject;
        pointerRT = pointer.GetComponent<RectTransform>();
        pointerRT.sizeDelta = new Vector2(Screen.width*0.05f,Screen.width*0.05f);
        pointerRT.anchoredPosition = new Vector2(Screen.width/2,Screen.height/2)-pointerRT.sizeDelta;
        distance = pointerCanvas.AddComponent<TextMeshProUGUI>();
        distance.alignment = TextAlignmentOptions.TopRight;
        distance.fontSize = 12;
        distance.margin = new Vector4(0,pointerRT.sizeDelta.y*2,pointerRT.sizeDelta.x/2,0);
        pointerCanvas.SetActive(false);
        
        bossRoom = GameObject.Find("Boss Room");

	}
	
	// Update is called once per frame
	void Update () {
        if(pointerCanvas.activeInHierarchy) {
            if(bossRoom == null)    bossRoom = GameObject.Find("Boss Room");
            distance.text = Vector3.Distance(player.transform.position,bossRoom.transform.position).ToString("#0.00m");
            Vector3 bossScreenPos = mainCam.WorldToScreenPoint(bossRoom.transform.position);
            Vector3 dir = bossRoom.transform.position-player.transform.position;
            float angle = Vector3.Angle(dir,new Vector3(mainCam.transform.forward.x,0,mainCam.transform.forward.z));
            float heading = Vector3.Cross(dir.normalized,new Vector3(mainCam.transform.forward.x,0,mainCam.transform.forward.z).normalized).y;
            //Vector3 dir = bossRoom.transform.position - mainCam.transform.forward;
            pointerRT.rotation = Quaternion.Euler(0,0,angle*(heading > 0 ? 1 : -1));
            Debug.Log(heading);

            Vector3 playerPos = player.transform.position;

            mapCam.gameObject.transform.position = new Vector3(playerPos.x,camHeight,playerPos.z);
        }

        if(Input.GetKeyDown(toggle)) {
            mapCam.enabled = !mapCam.enabled;
            pointerCanvas.SetActive(!pointerCanvas.activeInHierarchy);
        }
		
	}

    /*
    void OnGUI() {
        GUI.Label(new Rect(10,10,300,300),"Press M for map\n\nUse scroll wheel or press Z to adjust view");
    }
    */
}
