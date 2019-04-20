/*
 * Programmer:  Michael Atkinson
 * Description:
 * base puzzle class provides boolean variable for solved, and when player enters and it's unsolved, lock the doors.
 * Decorator Pattern
 * subclasses to this have to fill in the CheckSolveConditions() function, which this class will check in LateUpdate() to see if the room is solved.
 * subclasses also have to fill in the instructions string, which will be sent from this class to the PuzzleCountdown instance, to be displayed on the screen.
 * subclasses also have to fill in the instructions string, which will be sent from this class to the PuzzleCountdown instance, to be displayed on the screen.
 * attaches a specific puzzle script to room. puzzle script changes "solved" variable when some condition is met.
 * the PuzzleRoom will lock all doors upon entry till you solve it
 *
 */

using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PuzzleRoom : Room
{
    protected Inventory inventory = Inventory.instance;
    protected ItemDatabase itemDatabase; 
    protected PuzzleCountdown countdown = PuzzleCountdown.instance;
    protected String instructions;
    public bool solved = false;
    protected bool locked = false;
    private AudioClip solvedSound;
    private AudioSource audioSource;
    protected float TimeLimit;
    GameObject Powerup;
    
    public override void Awake() {
        addPortal = false;
        base.Awake();
        itemDatabase = ItemDatabase.instance;
        solvedSound = (AudioClip)Resources.Load("Michael/Audio/Bubble_1");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        Powerup = itemDatabase.RandomPowerupGrabber();

        lightColor = RG.Red;
    }

    public void PlaySolvedSound()
    {
        audioSource.PlayOneShot(solvedSound,1.0f);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            PlayerInRoom = true;
            if(!solved) {
                countdown.SetInstructions(instructions);
                StartCoroutine(countdown.FadeText(0f, 1f, 3f));
                locked = true;
                foreach(GameObject d in DoorList)
                    d.GetComponent<OpenDoor>().Lock();
            }
            SetLighting(lightColor,4);
        }
    }
    
    protected override void OnTriggerExit(Collider other) {
        if(other.gameObject == Player) {
            this.SetLighting(lightColor,0);
            PlayerInRoom = false;
        }
    }

    void LateUpdate() {
        if(countdown == null)   countdown = PuzzleCountdown.instance;
        if(inventory == null)   inventory = Inventory.instance;
        if(PlayerInRoom && !solved) {
            countdown.Count(TimeLimit);
            if(TimeLimit > 0)   TimeLimit -= Time.deltaTime;
            else {
                TimeLimit = 0;
                solved = true;
            }
            CheckSolveConditions();
            if(solved) {
                GameObject p = GameObject.Instantiate(Powerup,Zero+new Vector3(size.x,2,size.z)/2,Quaternion.identity,this.transform);
                StartCoroutine(countdown.FadeText(1f, 0f, 3f));
                Debug.Log("solved");
                UnlockRoom();
                inventory.incScore((int)TimeLimit);
            }
        }
        if(locked && !PlayerInRoom) UnlockRoom();
    }


    protected virtual void Update()
    {
        if(inventory == null)   inventory = Inventory.instance;
        if (solved && locked)
            UnlockRoom();
    }
    
    protected virtual void CheckSolveConditions() {}

    protected void UnlockRoom() {
        locked = false;
        foreach (GameObject d in DoorList)
            d.GetComponent<OpenDoor>().Unlock();
        lightColor = RG.LightGreen;
        SetLighting(lightColor,3);
    }

    public float GetScore() { return TimeLimit; }
}
