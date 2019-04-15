using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointer : MonoBehaviour {
    GameObject bossRoom;
    KeyCode toggle;
    GameObject player;
    GameObject arrow,arrowCanvas;
    RectTransform arrowRT;
    TextMeshProUGUI distance;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        player = GameObject.FindWithTag("Player");
        toggle = KeyCode.P;
        arrowCanvas = GameObject.Instantiate(Resources.Load<GameObject>("Michael/PointerCanvas"));
        arrow = arrowCanvas.transform.Find("Pointer").gameObject;

        arrowRT = arrow.GetComponent<RectTransform>();
        arrowRT.sizeDelta = new Vector2(Screen.width*0.05f,Screen.width*0.05f);
        arrowRT.anchoredPosition = new Vector2(Screen.width/2,Screen.height/2)-arrowRT.sizeDelta;

        distance = arrowCanvas.AddComponent<TextMeshProUGUI>();
        distance.alignment = TextAlignmentOptions.TopRight;
        distance.fontSize = 12;
        distance.margin = new Vector4(0,arrowRT.sizeDelta.y*2,arrowRT.sizeDelta.x/2,0);
        bossRoom = GameObject.Find("Boss Room");
    }

    void Update() 
    {
        if(arrowCanvas.activeInHierarchy)
        {
            if(bossRoom == null)    bossRoom = GameObject.Find("Boss Room");
            distance.text = Vector3.Distance(player.transform.position,bossRoom.transform.position+bossRoom.GetComponent<Room>().GetSize()/2).ToString("#0.00m");
            Vector3 dir = (bossRoom.transform.position-player.transform.position).normalized;
            Vector3 camXZ = new Vector3(cam.transform.forward.x,0,cam.transform.forward.z).normalized;
            float angle = Vector3.Angle(dir,camXZ);
            float heading = Vector3.Cross(dir,camXZ).y;
            arrowRT.rotation = Quaternion.Euler(0,0,angle*(heading > 0 ? 1 : -1));
        }

        if(Input.GetKeyDown(toggle)) {
            arrowCanvas.SetActive(!arrowCanvas.activeInHierarchy);
        }
    }
}

