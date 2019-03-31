using UnityEngine;
using System.Collections.Generic;

public class PuzzleThree : MonoBehaviour {

    private bool solved;
    private GameObject player;
    private int numRabbits;
    private GameObject rabbitReference;
    private Vector3 Zero, size;
    private PuzzleRoom R;
    private List<GameObject> Rabbits;

    public void Awake() {
    }

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        rabbitReference = Resources.Load("Michael/Rabbits/Prefabs/Rabbit 1") as GameObject;
        Rabbits = new List<GameObject>();
        R = this.GetComponent<PuzzleRoom>();
        Zero = R.GetZero();
        size = R.GetSize();
        numRabbits = (int)size.magnitude/2;

        for(int i = 0; i < numRabbits; i++) {
            GameObject r = GameObject.Instantiate(rabbitReference, Zero + new Vector3(Random.Range(1,size.x-1),0,Random.Range(1,size.z-1)),Quaternion.identity,this.transform);
            if(i == 0) {
                r.transform.Find("Rabbit").gameObject.GetComponent<Renderer>().material.color = new Color(1,0,0.5f);
                r.AddComponent<Light>().color = new Color(1.0f,0,0.5f);
            }
            Rabbits.Add(r);
        }
	}

	void FixedUpdate () {
        if(R.PlayerInRoom) {
            foreach(GameObject rabbit in Rabbits) 
                rabbit.GetComponent<Animator>().SetBool("moving",true);
        }
        else
            foreach(GameObject rabbit in Rabbits) 
                rabbit.GetComponent<Animator>().SetBool("moving",false);

        if (!R.solved) 
        {
            if(Vector3.Distance(Rabbits[0].transform.position,player.transform.position) < 1) {
                //Destroy(Rabbits[0].gameObject);
                Rabbits[0].GetComponent<Animator>().SetBool("moving",false);
                Rabbits.Remove(Rabbits[0]);
                R.solved = true;
            }

        }

        if(R.solved) 
        {

        }

	}
    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}

