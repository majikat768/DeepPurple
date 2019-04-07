using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// if the animator is set to "moving", set the robot (i mean rabbit) navmesh agent destination to a random location in the room.
public class Robot : MonoBehaviour {

    NavMeshAgent agent;
    public Animator animator;
    GameObject player;
    Room room;
    public bool moving = false;
    Vector3 RoomZero,RoomSize;


	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        animator.SetFloat("speed",Random.Range(1.0f,2.0f));
        player = GameObject.FindWithTag("Player");

        room = this.transform.parent.gameObject.GetComponent<Room>();
        RoomZero = room.GetZero();
        RoomSize = room.GetSize();
	}
	
	// Update is called once per frame
	void Update () {
        if(animator.GetBool("moving")) {
            if(!agent.hasPath || agent.remainingDistance < 2) {
                agent.SetDestination(RoomZero + new Vector3(Random.Range(0,RoomSize.x),0,Random.Range(0,RoomSize.z)));
            }
        }
        else
            agent.ResetPath();
		
	}
}
