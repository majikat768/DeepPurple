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

    public void Update()
    {
        if (!solved)
        {
            if (this.GetComponent<Room>().InRoom(Player) && !locked)
            {
                locked = true;
                Debug.Log("locking doors");
                foreach (GameObject d in R.DoorList)
                    d.GetComponent<OpenDoor>().Lock();
                R.SetLighting(Color.red);
            }
            solved = gameObject.GetComponent<PuzzleOne>().isSolved();

        }
        if (solved && locked)
        {
            locked = false;
            foreach (GameObject d in R.DoorList)
                d.GetComponent<OpenDoor>().Unlock();
            R.SetLighting(Color.white);
            
        }
    }


}
