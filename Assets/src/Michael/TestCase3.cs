using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestCase3 : MonoBehaviour {

    [SerializeField]
    int numRooms = 1;
    [SerializeField]
    int complexity; 
    LineRenderer line;
    int x,z;

    List<GameObject> endpointMarkers;
    System.String debugMessage,failMessage;
    Vector3 Zero;
    Vector3 size;
    [SerializeField]
    int minSize = 15;
    [SerializeField]
    int maxSize = 30;

    Vector3 start,end;
    Room targetRoom;

    NavMeshPath path;
    bool fail = false;

    float timeToBuild;

	// Use this for initialization
    void Awake() {
        Application.targetFrameRate = 300;
    }

	void Start () {
        this.tag = "Player";
        endpointMarkers = new List<GameObject>();
        line = GetComponent<LineRenderer>();
        line.startColor = new Color(0,1,0);
        line.endColor = new Color(0,1,0);
        timeToBuild = Time.realtimeSinceStartup;
        BuildRoom();
        timeToBuild = Time.realtimeSinceStartup - timeToBuild;
		

        line.endWidth = line.startWidth = 0.4f;
	}
	
    
	void FixedUpdate () {
        path = new NavMeshPath();
        end = new Vector3(x,0,z); 

        NavMeshHit hit;
        if(NavMesh.SamplePosition(end,out hit, 0.25f,NavMesh.AllAreas)) {
            if(NavMesh.CalculatePath(start,end,NavMesh.AllAreas,path)) {
                if(path.status == NavMeshPathStatus.PathComplete) {
                    line.positionCount = path.corners.Length;
                    for(int i = 0; i < path.corners.Length; i++) {
                        line.SetPosition(i,path.corners[i]);
                    }
                    /*/
                    GameObject s = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    s.transform.rotation = Quaternion.Euler(90,0,0);
                    s.transform.position = end + new Vector3(0,0.1f,0);
                    s.GetComponent<Renderer>().material.SetColor("_Color",new Color(0,1,0));
                    endpointMarkers.Add(s);
                    */
                    debugMessage = "end point " + end.ToString() + " \nis on navmesh, and is reachable.\n";
                }
                else {   // this is the FAIL point. 
                    if(!fail)   failMessage += "end point " + end.ToString() + " \nis on navmesh, but not reachable.\n";
                    GameObject f = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    f.transform.rotation = Quaternion.Euler(90,0,0);
                    f.transform.position = end + new Vector3(0,0.1f,0);
                    f.GetComponent<Renderer>().material.SetColor("_Color",new Color(1,0,0));
                    endpointMarkers.Add(f);
                    fail = true;
                }
            }
        }
        else
            debugMessage = "end point " + end.ToString() + " \nis not on navmesh.\n";
        if(z >= targetRoom.GetZero().z+targetRoom.GetSize().z-1) {
            z = (int)targetRoom.GetZero().z+1;
            x++;
        }
        else if(x <= targetRoom.GetZero().x+targetRoom.GetSize().x-1)
            z++;

        if(z >= targetRoom.GetZero().z+targetRoom.GetSize().z-1 && x >= targetRoom.GetZero().x+targetRoom.GetSize().x-1) {
            if(!fail) {
                foreach(Room r in RoomGenerator.RoomList)   DestroyImmediate(r.gameObject);
                foreach(GameObject o in endpointMarkers)    DestroyImmediate(o);
                endpointMarkers.Clear();
                RoomGenerator.RoomList.Clear();
                complexity++;
                timeToBuild = Time.realtimeSinceStartup;
                BuildRoom();
                timeToBuild = Time.realtimeSinceStartup - timeToBuild;
            }
            else 
                failMessage = "Room navigation failed\nat wall complexity " + (complexity+1).ToString() + "\n\n";
        }

	}

    void BuildRoom() {
        Zero = Vector3.zero;
        for(int i = 0; i < numRooms; i++) {
            size = new Vector3(Random.Range(minSize,maxSize),3,Random.Range(minSize,maxSize));
            GameObject room = RoomGenerator.Get(Zero);
            room.GetComponent<Room>().SetSize(size);
            room.GetComponent<Room>().complexity = complexity;
            room.GetComponent<Room>().Init();
            if(i == 0)
                start = room.GetComponent<Room>().GetZero() + new Vector3(1,0,1);
                //start = room.GetComponent<Room>().GetZero() + room.GetComponent<Room>().GetSize()/2;
            if(i == numRooms-1)
                targetRoom = room.GetComponent<Room>();


            //Zero = Zero + (size.x > size.z ? new Vector3(size.x,0,0) : new Vector3(0,0,size.z));
            Zero = Zero + (i%2 == 0 ? new Vector3(size.x,0,0) : new Vector3(0,0,size.z));
        }
        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
        x = (int)targetRoom.GetZero().x+1;
        z = (int)targetRoom.GetZero().z+1;
        failMessage = "";
        float MapHeight = Vector3.Distance(start, targetRoom.GetZero()+targetRoom.GetSize()) + 8;
        Debug.Log(MapHeight);
        Camera.main.orthographicSize = MapHeight/2;
        Camera.main.transform.position = new Vector3(MapHeight * 0.3f,10,MapHeight * 0.3f);
    }


    void OnGUI() {
        GUI.Label(new Rect(10,10,300,20),"Time to build rooms: " + timeToBuild.ToString() + " seconds");
        GUI.Label(new Rect(10,30,300,20),"wall complexity: " + (complexity+1).ToString());
        GUI.Label(new Rect(10,50,300,20),"room size: " + size.x.ToString() + " x " + size.z.ToString());
        GUI.Label(new Rect(10,70,300,40),debugMessage);
        GUI.Label(new Rect(10,110,300,200),failMessage);

    }
}
