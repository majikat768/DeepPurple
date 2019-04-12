using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// base puzzle class provides boolean variable for solved, and when player enters and it's unsolved, lock the doors.
// attaches a specific puzzle script to room. puzzle script changes "solved" variable when some condition is met.

public class PuzzleRoom : Room
{
    // only puzzles two and three are currently complete.  so i'll choose randomly between those two.
    
    Inventory inventory;
    public enum PuzzleType { Two, Three, Four, Five };
    [SerializeField]
    public PuzzleType pt = PuzzleType.Four;
    public bool solved = false;
    protected bool locked = false;
    protected PuzzleRoom R;
    private AudioClip solvedSound;
    private AudioSource audioSource;
    // the PuzzleRoom will lock all doors upon entry till you solve it
    
    public new void Awake() {
        base.Awake();
        inventory = Inventory.instance;
        lightColor = RoomGenerator.Red;
        // add puzzle component in RoomGenerator.Get.
        // 85% chance that it's the block puzzle.  15% chance that it's the rabbit puzzle.
        //pt = (Random.value < 0.85f ? PuzzleType.Two : PuzzleType.Three);
        //this.gameObject.AddComponent<PuzzleOne>();

    }

    public new void Start()
    {
        R = this.GetComponent<PuzzleRoom>();
        solvedSound = (AudioClip)Resources.Load("Michael/Audio/Bubble_1");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

    }

    public void PlaySolvedSound()
    {
        audioSource.PlayOneShot(solvedSound,1.0f);
    }

    protected new void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            PlayerInRoom = true;
            locked = true;
            foreach(GameObject d in R.DoorList)
                d.GetComponent<OpenDoor>().Lock();
            R.SetLighting(lightColor,2);
        }
    }
    
    protected void OnTriggerExit(Collider other) {
        if(other.gameObject == Player) {
            PlayerInRoom = false;
        }
    }

    public void Update()
    {
        if (solved && locked)
        {
            locked = false;
            foreach (GameObject d in R.DoorList)
                d.GetComponent<OpenDoor>().Unlock();
            lightColor = RoomGenerator.LightGreen;
            SetLighting(lightColor,2);
            inventory.incScore(5);
        }

    }


}
