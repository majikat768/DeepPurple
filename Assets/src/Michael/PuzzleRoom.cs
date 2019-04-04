using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// base puzzle class provides boolean variable for solved, and when player enters and it's unsolved, lock the doors.
// attaches a specific puzzle script to room. puzzle script changes "solved" variable when some condition is met.

public class PuzzleRoom : Room
{
    // only puzzles two and three are currently complete.  so i'll choose randomly between those two.
    
    public enum PuzzleType { Two, Three, Four };
    [SerializeField]
    public PuzzleType pt = PuzzleType.Two;
    public bool solved = false;
    private bool locked = false;
    private Room R;
    private AudioClip solvedSound;
    private AudioSource audioSource;
    // the PuzzleRoom will lock all doors upon entry till you solve it
    
    public void Awake() {
        base.Awake();
        // 85% chance that it's the block puzzle.  15% chance that it's the rabbit puzzle.
        //pt = (Random.value < 0.85f ? PuzzleType.Two : PuzzleType.Three);
        switch(pt) {
            case PuzzleType.Two:
                this.gameObject.AddComponent<PuzzleTwo>();
                break;
            case PuzzleType.Three:
                this.gameObject.AddComponent<PuzzleThree>();
                break;
            case PuzzleType.Four:
                this.gameObject.AddComponent<PuzzleFour>();
                break;
            default:
                break;
        }
        //this.gameObject.AddComponent<PuzzleOne>();

    }

    public new void Start()
    {
        R = this.GetComponent<Room>();
        Player = GameObject.FindWithTag("Player");
        solvedSound = (AudioClip)Resources.Load("Michael/Audio/Bubble_1");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

    }

    public void PlaySolvedSound()
    {
        audioSource.PlayOneShot(solvedSound,1.0f);
    }

    private new void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            PlayerInRoom = true;
            locked = true;
            foreach(GameObject d in R.DoorList)
                d.GetComponent<OpenDoor>().Lock();
            R.SetLighting(RoomGenerator.Red,1);
        }
    }
    
    private void OnTriggerExit(Collider other) {
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
            R.SetLighting(RoomGenerator.LightGreen,1);
            
        }
    }


}
