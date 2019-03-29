using UnityEngine;
using System.Collections.Generic;

public class PuzzleZero : MonoBehaviour {

    private bool solved;
    private List<GameObject> Teleporters;
    private int numTeleporters;
    private Vector3 Zero, size;
    private PuzzleRoom R;

    public void Awake() {
        this.GetComponent<Room>().complexity = -1;
    }

    public void Start()
    {
        R = this.GetComponent<PuzzleRoom>();
        Zero = R.GetZero();
        size = R.GetSize();

        foreach(GameObject door in R.DoorList) {
            Debug.DrawRay(door.transform.position,door.transform.TransformDirection(Vector3.forward),Color.green,10);
        }

	}

	void FixedUpdate () {
        if (!R.solved) 
        {
        }

        if(R.solved) 
        {

        }

	}
    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}
