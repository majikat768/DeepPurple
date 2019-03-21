using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class TestCase : MonoBehaviour {

    GameObject room;
    [SerializeField]
    GameObject target;
    GameObject player;
    NavMeshAgent agent;
    Room r;
    Vector3 Zero;
    Vector3 size;
    int complexity = 1;

	void Start () {
        gameObject.AddComponent<RoomGenerator>();

        Zero = Vector3.zero;
        size = new Vector3(Random.Range(20,40),5,Random.Range(20,40));

        room = new GameObject("room");
        r = room.AddComponent<Room>();

        r.complexity = 1;
        r.SetZero(Zero);
        r.SetSize(size);
        r.Init();

        RoomGenerator.RoomList.Add(r);

        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();

        target = GameObject.Instantiate(RoomGenerator.Block,new Vector3(r.Zero.x+r.size.x-2, 0, r.Zero.z+r.size.z-2),Quaternion.identity,this.transform);
        player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        player.AddComponent<Rigidbody>();
        player.tag = "Player";
        player.transform.position = new Vector3(3,3,3);
        agent = player.AddComponent<NavMeshAgent>();
        agent.speed = 6;
        agent.stoppingDistance = 1;
	}
	
	// Update is called once per frame
	void Update () {
        NavMeshPath path = new NavMeshPath();

        if(!agent.hasPath) {
            agent.CalculatePath(target.transform.position,path);
            if(path.status == NavMeshPathStatus.PathInvalid || path.status == NavMeshPathStatus.PathPartial) 
                Debug.Log("no valid path");
            else {
                Debug.Log("path found");
                agent.SetDestination(target.transform.position);
            }
        }

        if(Vector3.Distance(player.transform.position,target.transform.position) <= agent.stoppingDistance*2) {
            Debug.Log("complete");
            Destroy(target);
            agent.ResetPath();
            Zero = Zero + (size.x > size.y ? new Vector3(size.x,0,0) : new Vector3(0,0,size.y));
            size = new Vector3(Random.Range(15,45),5,Random.Range(15,45));
            room = new GameObject("room");
            r = room.AddComponent<Room>();
            r.SetZero(Zero);
            r.SetSize(size);
            r.complexity = complexity++;
            r.Init();
            RoomGenerator.RoomList.Add(r);
            RoomGenerator.Rebuild();
            RoomGenerator.BakeNavMesh();
            target = GameObject.Instantiate(RoomGenerator.Block,new Vector3(r.Zero.x+r.size.x-2, 0, r.Zero.z+r.size.z-2),Quaternion.identity,this.transform);
            
        }
		
	}
}
