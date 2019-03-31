using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    private GameObject player;
    private float height;
    private Room room;
    public bool justArrived;
    public GameObject Destination;

	// Use this for initialization
	void Start () {
        Debug.Log(Destination);
        player = GameObject.FindWithTag("Player");
        room = this.transform.parent.gameObject.GetComponent<Room>();
        this.GetComponent<ParticleSystem>().Stop();
	}
	
	// Update is called once per frame
	void Update () {
        if(room.PlayerInRoom) {
            if(!this.GetComponent<ParticleSystem>().isPlaying)
                this.GetComponent<ParticleSystem>().Play();
            this.transform.Rotate(new Vector3(0,2,0));
        }
        else
            if(this.GetComponent<ParticleSystem>().isPlaying)
                this.GetComponent<ParticleSystem>().Stop();

    }

    void OnTriggerEnter(Collider other) {
        if(other == player.GetComponent<Collider>() && !justArrived) {
            if(Destination == null) {
                Destination = RoomGenerator.TeleporterList[Random.Range(0,RoomGenerator.TeleporterList.Count)];
                while(Destination == this.gameObject && RoomGenerator.TeleporterList.Count > 1)
                    Destination = RoomGenerator.TeleporterList[Random.Range(0,RoomGenerator.TeleporterList.Count)];
            }
            Debug.Log("player entered Teleporter");
            Destination.GetComponent<Teleporter>().justArrived = true;
            Destination.GetComponent<Teleporter>().Destination = this.gameObject;
            player.transform.position = Destination.transform.position;
            player.transform.Rotate(new Vector3(0,180,0));
        }
    }

    void OnTriggerExit(Collider other) {
        if(other == player.GetComponent<Collider>())
            this.justArrived = false;
    }

    public void SetHeight(float y) { 
        this.height = y; 
        float speed = this.GetComponent<ParticleSystem>().main.startSpeed.constantMax;
        var main = this.GetComponent<ParticleSystem>().main;
        main.startLifetime = height / speed;
    }
}
