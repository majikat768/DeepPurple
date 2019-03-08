using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    private Vector3 OpenPosition; 
    private Vector3 ClosePosition;
    private float moveSpeed = 3;
    private int motionSensor = 4;
    private GameObject Player;
    private bool isOpen = false;
    private bool isLocked = false;

	void Start () {
        Player = GameObject.FindWithTag("Player");
        OpenPosition = this.transform.position + new Vector3(0.0f,this.transform.localScale.y,0.0f);
        ClosePosition = this.transform.position;
		
	}
	
	void Update () {
        isOpen = false;
        float dx,dz;
        if(!isLocked) {
            Vector3 PlayerPos = Player.transform.position;
            dx = Math.Abs(PlayerPos.x-transform.position.x);
            dz = Math.Abs(PlayerPos.z-transform.position.z);
            double distPlayer = Math.Sqrt(Math.Pow(dx,2)+Math.Pow(dz,2));
            if(distPlayer <= motionSensor) {
                isOpen = true;
            }

            foreach(GameObject e in RoomGenerator.EnemyList) {
                Vector3 ePos = e.transform.position;
                dx = Math.Abs(ePos.x-transform.position.x);
                dz = Math.Abs(ePos.z-transform.position.z);
                double eDist = Math.Sqrt(Math.Pow(dx,2)+Math.Pow(dz,2));
                if(eDist <= motionSensor) {
                    isOpen = true;
                    break;
                }
            }

            if(isOpen)  Open();
            else        Close();
        }
        else {
            Close();
        }
		
	}

    private void Open() {
        if(this.transform.position.y < OpenPosition.y)
            this.transform.position += new Vector3(0,moveSpeed*Time.deltaTime,0);
    }

    private void Close() {
        if(this.transform.position.y > ClosePosition.y)
            this.transform.position -= new Vector3(0,moveSpeed*Time.deltaTime,0);
    }
    public void Lock() { this.isLocked = true; }
    public void Unlock() { this.isLocked = false; }
}
