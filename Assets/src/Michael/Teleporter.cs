using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    private GameObject player;
    private Room room;
    private Bounds MotionSensor;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        room = this.GetComponent<PuzzleRoom>();
        MotionSensor = this.GetComponent<Renderer>().bounds;

		
	}
	
	// Update is called once per frame
	void Update () {
        if(MotionSensor.Contains(player.transform.position)) 
        { 
            //GameObject Destination = RoomGenerator.TeleporterList[Random.Range(0,RoomGenerator.TeleporterList.Count-1)];
            //player.transform.position = Destination.transform.position;

        }

		
	}
}
