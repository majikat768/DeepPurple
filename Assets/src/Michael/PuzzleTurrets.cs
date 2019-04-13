using UnityEngine;
using System.Collections.Generic;


// this level spawns rotating laser turrets that will maybe damage the player at some point.  the player can kill the turrets with his or her laser.
// there are obstacles to hide behind, i might make moving barriers or something too.  lots I could do with this
//

public class PuzzleTurrets : PuzzleRoom {

    private int numTurrets;
    private GameObject turretRef;
    private int numBoxes;
    private GameObject boxRef;
    GameObject Turret;
    private float scanSpeed = 20;
    List<GameObject> turretList;

    public void Awake() {
        complexity = -1;
        numBoxes = 3;
        numTurrets = 4;
        turretList = new List<GameObject>();
        base.Awake();
    }

    public void Start()
    {
        base.Start();
        ShowInstructions("destroy the laser turrets");
        turretRef = Resources.Load<GameObject>("Michael/Turret");
        boxRef = Resources.Load<GameObject>("Michael/Crate_004");

        /*
        for(int i = 0; i < numTurrets; i++) {
            GameObject t = GameObject.Instantiate(turretRef,Zero+new Vector3(Random.Range(2,size.x-2),0,Random.Range(2,size.z-2)),Quaternion.Euler(0,Random.Range(0,180),0),this.transform);
            turretList.Add(t);

        }
        */
        GameObject t = GameObject.Instantiate(turretRef,Zero+new Vector3(size.x/4,0,size.z/4),Quaternion.Euler(0,Random.Range(0,180),0),this.transform);
        turretList.Add(t);
        t = GameObject.Instantiate(turretRef,Zero+new Vector3(3*size.x/4,0,size.z/4),Quaternion.Euler(0,Random.Range(0,180),0),this.transform);
        turretList.Add(t);
        t = GameObject.Instantiate(turretRef,Zero+new Vector3(size.x/4,0,3*size.z/4),Quaternion.Euler(0,Random.Range(0,180),0),this.transform);
        turretList.Add(t);
        t = GameObject.Instantiate(turretRef,Zero+new Vector3(3*size.x/4,0,3*size.z/4),Quaternion.Euler(0,Random.Range(0,180),0),this.transform);
        turretList.Add(t);

        GameObject b = GameObject.Instantiate(boxRef,Zero+new Vector3(size.x/2,1,size.z/4),Quaternion.identity,this.transform);
        b = GameObject.Instantiate(boxRef,Zero+new Vector3(size.x/2,1,3*size.z/4),Quaternion.identity,this.transform);
        b = GameObject.Instantiate(boxRef,Zero+new Vector3(size.x/4,1,size.z/2),Quaternion.identity,this.transform);
        b = GameObject.Instantiate(boxRef,Zero+new Vector3(3*size.x/4,1,size.z/2),Quaternion.identity,this.transform);

        
	}

	void FixedUpdate () {
        if(PlayerInRoom) {
            RaycastHit hit;
            foreach(GameObject t in turretList) {
                var ps = t.GetComponent<ParticleSystem>();
                if(t.transform.up.y < 0.5f) {
                    Debug.Log("knocked over");
                    ps.Stop();
                    turretList.Remove(t);
                }

                else {
                    if(Physics.Raycast(t.transform.position+new Vector3(0,1,0),t.transform.forward,out hit,Mathf.Infinity)) {
                        if(hit.transform.gameObject == Player) {
                            t.GetComponent<Turret>().Target();
                        }
                        else {
                            t.GetComponent<Turret>().ScanRoom(hit);
                        }
                    }
                }
            }
        }

        if(turretList.Count == 0) {
            R.solved = true;
        }
        if(R.solved) 
        {

        }

	}

    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

    void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        if(other.gameObject == Player) {
            foreach(GameObject t in turretList) {
                var ps = t.GetComponent<ParticleSystem>();
                ps.Play();
                RaycastHit hit;
                if(Physics.Raycast(t.transform.position+new Vector3(0,1,0),t.transform.forward,out hit,Mathf.Infinity)) {
                    t.GetComponent<Turret>().ScanRoom(hit);
                }
            }
        }
    }

    void OnTriggerExit(Collider other) {
        base.OnTriggerExit(other);
        if(other.gameObject == Player) {
            foreach(GameObject t in turretList) {
                var ps = t.GetComponent<ParticleSystem>();
                ps.Stop();
            }
        }
    }

}
