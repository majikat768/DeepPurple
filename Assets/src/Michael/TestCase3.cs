using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestCase3 : MonoBehaviour {

    int numRooms = 1;
    [SerializeField]
    int complexity; 
    LineRenderer lr;

    System.String debugMessage,failMessage;
    Vector3 Zero;
    Vector3 size;
    int minSize = 15;
    int maxSize = 30;

    Vector3 start,end;

    NavMeshPath path;
    bool fail = false;


	// Use this for initialization
	void Start () {
        complexity = 0;
        lr = GetComponent<LineRenderer>();
        Zero = Vector3.zero;
        size = new Vector3(Random.Range(minSize,maxSize),3,Random.Range(minSize,maxSize));
        GameObject room = RoomGenerator.Get(Zero);
        room.tag = "Player";
        room.GetComponent<Room>().SetSize(size);
        room.GetComponent<Room>().complexity = complexity;
        room.GetComponent<Room>().Init();
        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
		
        start = Zero + new Vector3(1,0,1);

        lr = GetComponent<LineRenderer>();
        lr.endWidth = lr.startWidth = 0.4f;
	}
	
    
    int i = 1;
    int j = 1;
	void Update () {
        path = new NavMeshPath();
        end = new Vector3(i,0,j);

        NavMeshHit hit;
        if(NavMesh.SamplePosition(end,out hit, 0.25f,NavMesh.AllAreas)) {
            if(NavMesh.CalculatePath(start,end,NavMesh.AllAreas,path)) {
                if(path.status == NavMeshPathStatus.PathComplete) {
                    lr.positionCount = path.corners.Length;
                    for(int k = 0; k < path.corners.Length; k++) {
                        lr.SetPosition(k,path.corners[k]);
                    }
                    debugMessage = "end point " + end.ToString() + " \nis on navmesh, and is reachable.\n";
                }
                else {   // this is the FAIL point. 
                    if(!fail)   failMessage += "end point " + end.ToString() + " \nis on navmesh, but not reachable.\n";
                    GameObject f = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    f.transform.rotation = Quaternion.Euler(90,0,0);
                    f.transform.position = end + new Vector3(0,0.1f,0);
                    f.GetComponent<Renderer>().material.SetColor("_Color",new Color(1,0,0));
                    fail = true;
                }
            }
        }
        else
            debugMessage = "end point " + end.ToString() + " \nis not on navmesh.\n";
        if(j >= size.z-1) {
            j = 1;
            i++;
        }
        else if(i <= size.x-1)
            j++;

        if(j >= size.z-1 && i >= size.x-1) {
            if(!fail) {
                foreach(Room r in RoomGenerator.RoomList)   DestroyImmediate(r.gameObject);
                RoomGenerator.RoomList.Clear();
                complexity++;
                BuildRoom();
            }
            else 
                failMessage = "Room navigation failed\nat size " + size.x.ToString() + " x " + size.z.ToString() + " \nand wall complexity " + (complexity+1).ToString() + "\n\n";
        }

	}

    void BuildRoom() {
        size = new Vector3(Random.Range(minSize,maxSize),3,Random.Range(minSize,maxSize));
        GameObject room = RoomGenerator.Get(Zero);
        room.tag = "Player";
        room.GetComponent<Room>().SetSize(size);
        room.GetComponent<Room>().complexity = complexity;
        room.GetComponent<Room>().Init();
        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
        i = 1;
        j = 1;
        failMessage = "";
    }


    void OnGUI() {
        GUI.Label(new Rect(10,10,200,20),"wall complexity: " + (complexity+1).ToString());
        GUI.Label(new Rect(10,30,200,20),"room size: " + size.x.ToString() + " x " + size.z.ToString());
        GUI.Label(new Rect(10,50,200,40),debugMessage);
        GUI.Label(new Rect(10,90,200,200),failMessage);

    }
}
