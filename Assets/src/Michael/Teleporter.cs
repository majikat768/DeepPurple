using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Teleporter object is just a Unity Particle System.
//it rotates around the y axis every frame to make it look neat.
//it has a collider that, when the character enters it,
//it moves the character to a new teleporter location.
// when a player enters a teleporter the first time, it chooses a random one; 
// whichever random one it chooses is now connected just to that one, so they go back and forth between each other.
// this doesn't work quite perfectly yet.

public class Teleporter : MonoBehaviour {

    private GameObject player;
    private float height;
    private Room room;
    public bool justArrived;
    public GameObject Destination;

    private AudioClip bloop;
    private AudioSource audioSource;
    private RoomGenerator RG;

	// Use this for initialization
	void Start () {
        RG = RoomGenerator.instance;
        player = GameObject.FindWithTag("Player");
        room = this.transform.parent.gameObject.GetComponent<Room>();
        this.GetComponent<ParticleSystem>().Stop();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        bloop = (AudioClip)Resources.Load("Michael/Audio/Lightsaber");
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
                Destination = RG.teleporterList[Random.Range(0,RG.teleporterList.Count-1)];
                while(Destination == this.gameObject && RG.teleporterList.Count > 1)
                    Destination = RG.teleporterList[Random.Range(0,RG.teleporterList.Count)];
            }
            Debug.Log("player entered Teleporter");
            Destination.GetComponent<Teleporter>().justArrived = true;
            Destination.GetComponent<Teleporter>().Destination = this.gameObject;
            player.transform.position = Destination.transform.position; 
            player.transform.Rotate(new Vector3(0,180,0));
            Camera.main.GetComponent<vThirdPersonCamera>().mouseX = -135;
            //Camera.main.GetComponent<vThirdPersonCamera>().mouseY = 30;
            Camera.main.GetComponent<vThirdPersonCamera>().RotateCamera(0,0);

            audioSource.PlayOneShot(bloop,1.0f);
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
