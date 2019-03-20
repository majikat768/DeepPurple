using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PuzzleRoom : Room
{
    public bool solved = false;
    private bool locked = false;
    private Room R;
    private AudioClip solvedSound;
    private AudioSource audioSource;
    // the PuzzleRoom will lock all doors upon entry till you solve it
    
    public new void Start()
    {
        R = this.GetComponent<Room>();
        this.gameObject.AddComponent<PuzzleTwo>();
        Player = GameObject.FindWithTag("Player");
        solvedSound = (AudioClip)Resources.Load("Michael/Audio/Bubble_1");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

    }

    public void PlaySolvedSound()
    {
        audioSource.PlayOneShot(solvedSound,1.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            locked = true;
            foreach (GameObject d in R.DoorList)
                d.GetComponent<OpenDoor>().Lock();
            R.SetLighting(RoomGenerator.Fuschia,1);
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
