using UnityEngine;
using System.Collections.Generic;

// This is a work in progress.
// I think I might make an actual maze, or something pacman-ish?
// this changes camera view to top down when player is in room.
// User control is a problem, because movement is still dependent on mouse direction.
// **
// Update:  the purpose of this script has totally changed .
// now, it's enabling a second camera from a bird's eye view,
// rendering in the lower left of the screen.
// we could use this as an on-screen level map.
// pretty neat, huh

public class PuzzleFour : MonoBehaviour {
    GameObject Platform; 
    PuzzleRoom R;
    Camera roomCam;
    GameObject player;
    Camera mainCam;
    private float FieldOfView;
    Vector3 Zero,size;
    public bool solved;
    private List<MovingPlatform> MovingPlatforms;
    private BoxCollider roomCollider;

    public void Awake()
    {
        R = this.GetComponent<PuzzleRoom>();
        MovingPlatforms = new List<MovingPlatform>();
        FieldOfView = Camera.main.fieldOfView;
        Platform = Resources.Load<GameObject>("Michael/platform");
        mainCam = Camera.main;
        R.complexity = -1;
    }
    public void Start()
    {
        Zero = R.GetZero();
        size = R.GetSize();
        roomCollider = this.GetComponent<BoxCollider>();
        player = GameObject.FindWithTag("Player");
        roomCam = new GameObject("roomcam").AddComponent<Camera>();
        roomCam.gameObject.transform.parent = this.transform;
        roomCam.orthographic = true;

        Destroy(this.transform.Find("Ceiling").gameObject);
        for(int i = 1; i < 2; i++) {
            R.BuildWall(Zero + new Vector3(0,size.y*i,0),Zero + new Vector3(size.x,size.y*i,0),size.y*6,false);
            R.BuildWall(Zero + new Vector3(0,size.y*i,0),Zero + new Vector3(0,size.y*i,size.z),size.y*6,false);
            R.BuildWall(Zero + new Vector3(size.x,size.y*i,0),Zero + new Vector3(size.x,size.y*i,size.z),size.y*6,false);
            R.BuildWall(Zero + new Vector3(0,size.y*i,size.z),Zero + new Vector3(size.x,size.y*i,size.z),size.y*6,false);
        }
        roomCollider.size = new Vector3(roomCollider.size.x,roomCollider.size.y*11,roomCollider.size.z);

        // here, I'm making the camera size relative to the size of the room.
        // for an on screen map, this will be changed to something else, 
        // probably.
        roomCam.orthographicSize = size.magnitude/2.5f;

        // roomCam.rect changes the location of the 
        // rendered camera (map) on screen.
        // parameters are (x location,y location,width,height).
        // each one is a percent of the total game screen,
        // so this is 2% offset from x and y 
        // and the size is 30% of the width and height.
        roomCam.rect = new Rect(0.02f,0.02f,0.3f,0.3f);
        roomCam.gameObject.transform.position = Zero + size/2 + new Vector3(0,size.y*2,0);
        roomCam.gameObject.transform.rotation = Quaternion.Euler(90,0,0);

        roomCam.enabled = false;
        mainCam.enabled = true;
        R.solved = true;
        BuildPlatforms();
	}

	void Update () {
        if(R.PlayerInRoom) {
            roomCam.enabled = true;
            foreach(MovingPlatform p in MovingPlatforms) {
                if(!p.OnlyMoveWithPlayer)
                    p.moving = true;
            }
        }
        else if(!R.PlayerInRoom) {
            roomCam.enabled = false;
            foreach(MovingPlatform p in MovingPlatforms) {
                p.moving = false;
            }
        }


        if (!R.solved)
        {

        }

	}

    private void BuildPlatforms() {
        float stepHeight = size.y;
        GameObject p1 = GameObject.Instantiate(Platform,Zero+new Vector3(Platform.GetComponent<Renderer>().bounds.size.x*2,stepHeight,size.z/2),Quaternion.identity,this.transform);
        p1.transform.localScale = new Vector3(2,1,size.z/2);

        GameObject ramp = GameObject.Instantiate(Platform);
        ramp.transform.localScale = new Vector3(2,1.0f,12);
        ramp.transform.rotation = Quaternion.Euler(20,90,0);
        ramp.transform.position = new Vector3(p1.transform.position.x+5*Mathf.Cos(20*Mathf.Deg2Rad)+p1.GetComponent<Renderer>().bounds.size.x,5*Mathf.Sin(20*Mathf.Deg2Rad),Zero.z+size.z/2);
        GameObject elevator = GameObject.Instantiate(Platform);
        elevator.transform.position = p1.transform.position+new Vector3(0,0,p1.GetComponent<Renderer>().bounds.size.z/2)+new Vector3(0,0,elevator.GetComponent<Renderer>().bounds.size.z/2);
        elevator.transform.parent = this.transform;
        elevator.AddComponent<MovingPlatform>().Init(elevator.transform.position,elevator.transform.position + new Vector3(0,elevator.transform.position.y,0));
        MovingPlatforms.Add(elevator.GetComponent<MovingPlatform>());

        GameObject p2 = GameObject.Instantiate(Platform);
        p2.transform.position = elevator.GetComponent<MovingPlatform>().end+new Vector3(6,0,0);
        p2.transform.localScale = new Vector3(8,0.5f,3);

        GameObject mover2 = GameObject.Instantiate(Platform);
        mover2.transform.position = p2.transform.position + new Vector3(6,-1,-2);
        mover2.AddComponent<MovingPlatform>().Init(mover2.transform.position, mover2.transform.position + new Vector3(0,7,-4));
        MovingPlatforms.Add(mover2.GetComponent<MovingPlatform>());

        GameObject mover3 = GameObject.Instantiate(Platform);
        mover3.transform.position = mover2.GetComponent<MovingPlatform>().end - mover3.GetComponent<Renderer>().bounds.size*2;
        mover3.transform.localScale *= 3;
        mover3.AddComponent<MovingPlatform>().Init(mover3.transform.position,mover3.transform.position + new Vector3(0,7,0),1, true);
        MovingPlatforms.Add(mover3.GetComponent<MovingPlatform>());
    }

    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}
