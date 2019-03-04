using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuzzleRoom : Room
{
    // the PuzzleRoom will lock all doors upon entry till you solve it
    
    public List<GameObject> Doors = new List<GameObject>();
    private GameObject GM;
    public PuzzleRoom(Vector3 Zero,GameObject r) : base(Zero,r)
    {
        foreach(Transform wall in r.transform.Find("Walls").transform) {
            foreach(Transform door in wall) {
                if(door.name == "Door") {
                    r.GetComponent<RoomGenerator>().MyDoors.Add(door.gameObject);

                }
            }
        }


    }


}
