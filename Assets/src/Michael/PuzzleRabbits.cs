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
    Color Red;
    Color Green;
    Color Blue;

    protected void Awake() {
        Red = new Color(1,0,0);
        Green = new Color(0,1,0);
        Blue = new Color(0,0,1);

        base.Awake();
        instructions = "catch the green rabbit";
        TimeLimit = 30;
    }

    protected void Start()
    {
        rabbitReference = Resources.Load("Michael/Rabbits/Prefabs/Rabbit 1") as GameObject;
        Rabbits = new List<GameObject>();
        numRabbits = (int)size.magnitude/2;

        for(int i = 0; i < numRabbits; i++) {
            GameObject r = GameObject.Instantiate(rabbitReference, Zero + new Vector3(Random.Range(1,size.x-1),0,Random.Range(1,size.z-1)),Quaternion.identity,this.transform);
            if(i == 0) {
                r.transform.Find("Rabbit").gameObject.GetComponent<Renderer>().material.color = new Color(0,1,0.25f);
            }
            else {
                float rand = Random.value;
                if(rand < 0.33f)
                    r.transform.Find("Rabbit").gameObject.GetComponent<Renderer>().material.color = Red;
                else if(rand < 0.66f)
                    r.transform.Find("Rabbit").gameObject.GetComponent<Renderer>().material.color = Blue;
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

        if(solved) 
        {

        }

	}
    protected override void CheckSolveConditions() {
        if(Vector3.Distance(Rabbits[0].transform.position,Player.transform.position) < 1) {
            Rabbits[0].GetComponent<Animator>().SetBool("moving",false);
            Rabbits.Remove(Rabbits[0]);
            solved = true;
            PlaySolvedSound();
            UnlockRoom();
        }
    }

    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}

