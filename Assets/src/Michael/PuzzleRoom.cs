using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

// base puzzle class provides boolean variable for solved, and when player enters and it's unsolved, lock the doors.
// attaches a specific puzzle script to room. puzzle script changes "solved" variable when some condition is met.
    // the PuzzleRoom will lock all doors upon entry till you solve it

public class PuzzleRoom : Room
{
    
    protected Inventory inventory;
    protected String instructions;
    public bool solved = false;
    protected bool locked = false;
    private AudioClip solvedSound;
    private AudioSource audioSource;
    private GameObject text;
    protected float TimeLimit;
    TextMeshProUGUI TMP; 
    CanvasGroup textCanvas;
    
    protected override void Awake() {
        base.Awake();
        text = new GameObject("Text");
        text.transform.parent = this.transform;
        solvedSound = (AudioClip)Resources.Load("Michael/Audio/Bubble_1");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        TMP = text.AddComponent<TextMeshProUGUI>();
        TMP.margin = new Vector4(10,0,0,10);
        TMP.fontSize = 18;
        TMP.alignment = TextAlignmentOptions.BottomLeft;
        text.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        textCanvas = text.AddComponent<CanvasGroup>();
        text.SetActive(false);

        lightColor = RoomGenerator.Red;
    }

    protected override void Start()
    {
        inventory = Inventory.instance;
    }

    public static IEnumerator FadeText(CanvasGroup canvas, float startAlpha, float endAlpha, float duration) {
        var start = Time.time;
        var end = start + duration;

        float elapsed = 0;
        canvas.alpha = startAlpha;

        while(Time.time <= end) {
            elapsed = Time.time - start;
            var perc = 1.0f/(duration/elapsed);
            
            if(startAlpha > endAlpha) {
                canvas.alpha = startAlpha - perc;
            }
            else {
                canvas.alpha = startAlpha + perc;
            }
            yield return new WaitForEndOfFrame(); 
        }
        canvas.alpha = endAlpha;
        if(endAlpha == 0)    canvas.gameObject.SetActive(false);
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
                text.SetActive(true);
                StartCoroutine(FadeText(textCanvas, 0f, 1f, 3f));
                locked = true;
                foreach(GameObject d in DoorList)
                    d.GetComponent<OpenDoor>().Lock();
            }
            SetLighting(lightColor,2);
        }
    }
    
    void LateUpdate() {
        if(PlayerInRoom && !solved) {
            ShowInstructions(instructions + '\n' + TimeLimit.ToString("#0.00"));
            if(TimeLimit > 0)   TimeLimit -= Time.deltaTime;
        }
    }

    protected override void OnTriggerExit(Collider other) {
        if(other.gameObject == Player) {
            this.SetLighting(lightColor,0);
            PlayerInRoom = false;
            StartCoroutine(FadeText(textCanvas,1f,0f,1f));
        }
    }

    protected void ShowInstructions(String txt) {
        text.GetComponent<TextMeshProUGUI>().text = txt; 
    }

    protected virtual void Update()
    {
        if(inventory == null)   inventory = Inventory.instance;
        if (solved && locked)
            UnlockRoom();
        if(!solved)
            CheckSolveConditions();

    }
    
    protected virtual void CheckSolveConditions() {}

    protected void UnlockRoom() {
        StartCoroutine(FadeText(textCanvas,1f,0f,1f));
        locked = false;
        foreach (GameObject d in DoorList)
            d.GetComponent<OpenDoor>().Unlock();
        lightColor = RoomGenerator.LightGreen;
        SetLighting(lightColor,2);
    }

    public float GetScore() { return TimeLimit; }
}
