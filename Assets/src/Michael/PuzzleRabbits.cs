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
    Color goalColor;

    protected void Awake() {
        base.Awake();
        TimeLimit = 30;
        goalColor = RandomColor();
        instructions = "catch the " + ColorName(goalColor) + " rabbit";
    }

    Color RandomColor() {
        return new Color(Random.Range(0,2),Random.Range(0,2),Random.Range(0,2));
    }

    protected void Start() {
            rabbitReference = Resources.Load("Michael/Rabbits/Prefabs/Rabbit 1") as GameObject;
            Rabbits = new List<GameObject>();
            numRabbits = (int)size.magnitude;
    }

    public void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.gameObject == Player && Rabbits.Count == 0) {

            for(int i = 0; i < numRabbits; i++) {
                GameObject r = GameObject.Instantiate(rabbitReference, Zero + new Vector3(Random.Range(1,size.x-1),0,Random.Range(1,size.z-1)),Quaternion.identity,this.transform);
                if(i == 0) {
                    r.transform.Find("Rabbit").gameObject.GetComponent<Renderer>().material.color = goalColor;
                }
                else {
                    Color RabbitColor = RandomColor();
                    while(RabbitColor == goalColor) RabbitColor = RandomColor();
                    r.transform.Find("Rabbit").gameObject.GetComponent<Renderer>().material.color = RabbitColor;
                }
                Rabbits.Add(r);
            }
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
                Rabbits[0].GetComponent<Animator>().SetBool("moving",true);
            Rabbits[0].GetComponent<Robot>().pet = true;
            Rabbits.Remove(Rabbits[0]);
            solved = true;
            PlaySolvedSound();
            UnlockRoom();
        }
    }

    string ColorName(Color c) { 
        if(c.r == 1)
            if(c.g == 1)
                if(c.b == 1)
                    return "white";
                else
                    return "yellow";
            else if(c.b == 1)
                return "pink";
            else
                return "red";
        else
            if(c.g == 1)
                if(c.b == 1)
                    return "cyan";
                else
                    return "green";
            else
                if(c.b == 1)
                    return "blue";
                else
                    return "black";
    }

    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}

