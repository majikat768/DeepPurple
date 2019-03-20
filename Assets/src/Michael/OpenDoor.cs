using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    private Vector3 OpenPosition, ClosePosition;
    private readonly float moveSpeed = 5;
    //private int motionSensor = 4;
    private Vector3 motionSensor = new Vector3(4,4,4)/2;
    private bool isOpen = false;
    private bool isLocked = false;
    private AudioClip openSound,closeSound; 
    private AudioSource audioSource;
    public float volume = 0.5f;

	void Start () {
        OpenPosition = this.transform.position + new Vector3(0.0f,this.GetComponent<Collider>().bounds.size.y,0.0f);
        ClosePosition = this.transform.position;


        audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.clip = openSound;
        audioSource.playOnAwake = false;

        if (openSound == null)
            openSound = (AudioClip)Resources.Load("Michael/Audio/electric_door_opening_1");
        if (closeSound == null)
            closeSound = (AudioClip)Resources.Load("Michael/Audio/electric_door_closing_2");
            //closeSound = (AudioClip)Resources.Load("Michael/Audio/sfx-door-open");
	}
	
	void Update () {
        isOpen = false;
        if(!isLocked) {
            foreach(Collider o in Physics.OverlapBox(new Vector3(this.transform.position.x,0,this.transform.position.z),motionSensor))
            {
                if(o.tag == "Player" || o.name == "Enemy")
                {
                    isOpen = true;
                    if (!audioSource.isPlaying)
                        audioSource.PlayOneShot(openSound, 1.0f);
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
        if(this.transform.position.y < OpenPosition.y) {
            Debug.Log("opening");
            this.transform.position += new Vector3(0,moveSpeed*Time.deltaTime,0);
        }
        else
            audioSource.Stop();

    }

    private void Close() {
        if (this.transform.position.y > ClosePosition.y)
        {
            Debug.Log("closing");
            this.transform.position -= new Vector3(0, moveSpeed/2.0f * Time.deltaTime, 0);
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(closeSound, 1.0f);
        }
        else
            audioSource.Stop();
    }
    public void Lock() {
        this.isLocked = true;
        this.GetComponent<Renderer>().materials[1].color = new Color(0.984f, 0.313f, 0.156f, 0.309f);
    }
    public void Unlock() {
        this.isLocked = false;
        this.GetComponent<Renderer>().materials[1].color = new Color(0.156f, 0.313f, 0.984f, 0.309f);
    }
}
