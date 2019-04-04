using UnityEngine;

// This is a work in progress.
// I think I might make an actual maze, or something pacman-ish?
// changes camera view to top down when player is in room.
// User control is a problem, because movement is still dependent on mouse direction.
//
public class PuzzleFour : MonoBehaviour {

    private bool solved;
    private Vector3 Zero, size;
    public PuzzleRoom R;

    GameObject roomCamObj;
    Camera roomCam;
    GameObject player;
    Camera mainCam;

    public void Start()
    {
        mainCam = Camera.main;
        R = this.GetComponent<PuzzleRoom>();
        Zero = R.GetZero();
        player = GameObject.FindWithTag("Player");
        size = R.GetSize();
        roomCam = new GameObject("roomcam").AddComponent<Camera>();
        roomCam.gameObject.transform.parent = this.transform;
        roomCam.orthographic = true;
        roomCam.orthographicSize = size.magnitude/2;
        roomCam.gameObject.transform.position = Zero + size/3 + new Vector3(0,size.y*3,0);
        roomCam.gameObject.transform.rotation = Quaternion.Euler(60,45,0);

        roomCam.enabled = false;
        mainCam.enabled = true;
        R.solved = true;
	}

	void Update () {
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
