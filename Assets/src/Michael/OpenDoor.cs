using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    private Vector3 OpenPosition; 
    private Vector3 ClosePosition;
    private float moveSpeed = 2;
    private int motionSensor = 4;
    private GameObject Player;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindWithTag("Player");
        OpenPosition = this.transform.position + new Vector3(0.0f,RoomGenerator.GetSize().y,0.0f);
        ClosePosition = this.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = Player.transform.position;
        float dx = Math.Abs(playerPos.x-transform.position.x);
        float dz = Math.Abs(playerPos.z-transform.position.z);
        float distPlayer = (float)Math.Sqrt(Math.Pow(dx,2.0f)+Math.Pow(dz,2.0f));
        if(distPlayer <= motionSensor) {
            if(this.transform.position.y < OpenPosition.y)
                this.transform.position += new Vector3(0.0f,moveSpeed*Time.deltaTime,0.0f);
        }
        else {
            if(this.transform.position.y > ClosePosition.y)
                this.transform.position -= new Vector3(0.0f,moveSpeed*Time.deltaTime,0.0f);
        }
		
	}
}
