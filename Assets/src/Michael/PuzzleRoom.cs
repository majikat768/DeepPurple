using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuzzleRoom : Room
{
    private bool solved = false;
    private bool locked = false;
    private Inventory inventory;
    private Room R;
    // the PuzzleRoom will lock all doors upon entry till you solve it
    
    public new void Start()
    {
        R = this.GetComponent<Room>();
        Player = GameObject.FindWithTag("Player");
        PuzzleOne p = gameObject.AddComponent<PuzzleOne>();
        p.Init(Zero,size);


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            locked = true;
            foreach (GameObject d in R.DoorList)
                d.GetComponent<OpenDoor>().Lock();
            R.SetLighting(Color.red,1);
        }
    }
    public void Update()
    {
        if (locked && !solved)
        {
            solved = gameObject.GetComponent<PuzzleOne>().isSolved();

        }

        if (solved && locked)
        {
            locked = false;
            foreach (GameObject d in R.DoorList)
                d.GetComponent<OpenDoor>().Unlock();
            R.SetLighting(Color.white,1);
            
        }
    }


}
