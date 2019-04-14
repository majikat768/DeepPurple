using UnityEngine;
using System.Collections.Generic;

// This puzzle is kind of ridiculous and dumb.
// Spawn a bunch of rabbits, and make one of them pink.
// When the player enters the room, the rabbits just start running around like crazy.
// After you catch the pink one, the room is solved.
// I'm not sure what I was thinking
//

public class PuzzleRabbits : PuzzleRoom {

    private int numRabbits;
    private GameObject rabbitReference;
    private List<GameObject> Rabbits;

    protected void Awake() {
        base.Awake();
        TimeLimit = 30;
        instructions = "catch the pink rabbit";
    }

    protected override void Start()
    {
        base.Start();
        rabbitReference = Resources.Load("Michael/Rabbits/Prefabs/Rabbit 1") as GameObject;
        Rabbits = new List<GameObject>();
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

	protected override void Update () {
        if(PlayerInRoom) {

            foreach(GameObject rabbit in Rabbits) 
                rabbit.GetComponent<Animator>().SetBool("moving",true);
        }
        else
            foreach(GameObject rabbit in Rabbits) 
                rabbit.GetComponent<Animator>().SetBool("moving",false);

        if (!solved) 
            CheckSolveConditions();

        if(solved) 
        {

        }

	}
    protected override void CheckSolveConditions() {
        if(Vector3.Distance(Rabbits[0].transform.position,Player.transform.position) < 1) {
            Rabbits[0].GetComponent<Animator>().SetBool("moving",false);
            Rabbits.Remove(Rabbits[0]);
            solved = true;
            inventory.incScore((int)TimeLimit);
            PlaySolvedSound();
            UnlockRoom();
        }
    }

    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}

