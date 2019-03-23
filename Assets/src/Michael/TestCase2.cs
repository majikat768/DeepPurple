using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestCase2 : MonoBehaviour {

    int numRooms = 1;
    int complexity = 1;
    LineRenderer lr;

    System.String debugMessage;
    Vector3 Zero;
    Vector3 size;

    [SerializeField]
    public GameObject player;
    NavMeshAgent agent;
    GameObject target;


	// Use this for initialization
	void Start () {
        BuildRooms();

        agent = player.GetComponent<NavMeshAgent>();
        lr = player.GetComponent<LineRenderer>();
        lr.endWidth = lr.startWidth = 0.4f;

        target = GameObject.CreatePrimitive(PrimitiveType.Cube);
        target.transform.position = RoomGenerator.RoomList[RoomGenerator.RoomList.Count-1].Zero+new Vector3(3,0,3);
		
	}
	
    
	void Update () {
        if(! agent.hasPath) 
            agent.SetDestination(target.transform.position);
        if(agent.path != null) {
            lr.positionCount = agent.path.corners.Length;
            for(int i = 0; i < agent.path.corners.Length; i++) {
                lr.SetPosition(i,agent.path.corners[i]);
            }
        }


        if(Vector3.Distance(player.transform.position,target.transform.position) < 1) {
            DestroyImmediate(target);
            Debug.Log("complete.");
            agent.ResetPath();
            foreach(Room r in RoomGenerator.RoomList)   DestroyImmediate(r.gameObject);
            RoomGenerator.RoomList.Clear();
            //numRooms++;
            complexity++;
            BuildRooms();
            target = GameObject.CreatePrimitive(PrimitiveType.Cube);
            target.transform.position = RoomGenerator.RoomList[RoomGenerator.RoomList.Count-1].Zero+new Vector3(3,0,3);
        }
		
	}

    void BuildRooms() {
        Zero = Vector3.zero;
        size = new Vector3(Random.Range(20,40),5,Random.Range(20,40));

        for(int i = 0; i < numRooms; i++) {
            GameObject room = RoomGenerator.Get(Zero);
            if(RoomGenerator.RoomList.Count == 1)
                player.transform.position = room.GetComponent<Room>().Zero+new Vector3(3,3,3);
            room.GetComponent<Room>().SetSize(size);
            room.GetComponent<Room>().complexity = complexity;
            room.GetComponent<Room>().Init();
            //Zero = Zero + (size.x > size.z ? new Vector3(0,0,size.z) : new Vector3(size.x,0,0));
            Zero = Zero + new Vector3(0,0,size.z);
            size = new Vector3(Random.Range(20,40),5,Random.Range(20,40));
            
        }

        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
    }

    void OnGUI() {
        debugMessage = "complexity = " + (complexity+1).ToString() + "\nFPS:" + (1.0f/Time.deltaTime).ToString();
        GUI.Label(new Rect(10,10,100,100),debugMessage);
    }

}
