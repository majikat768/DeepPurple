using UnityEngine;

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

public class PuzzleMapTest : PuzzleRoom {

    GameObject roomCamObj;
    Camera roomCam;
    Camera mainCam;

    public new void Start()
    {
        base.Start();
        mainCam = Camera.main;
        roomCam = new GameObject("roomcam").AddComponent<Camera>();
        roomCam.gameObject.transform.parent = this.transform;
        roomCam.orthographic = true;

        // here, I'm making the camera size relative to the size of the room.
        // for an on screen map, this will be changed to something else, 
        // probably.
        roomCam.orthographicSize = size.magnitude/2.5f;

        //roomCam.rect changes the location of the 
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
	}

	new void Update () {
        base.Update();
        if(this.GetComponent<Room>().PlayerInRoom) {
            roomCam.enabled = true;
        }
        else if(!R.PlayerInRoom) {
            roomCam.enabled = false;
        }


        if (!R.solved)
        {

        }

	}
    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}

