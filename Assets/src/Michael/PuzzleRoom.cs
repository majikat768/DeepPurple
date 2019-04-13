using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

// base puzzle class provides boolean variable for solved, and when player enters and it's unsolved, lock the doors.
// attaches a specific puzzle script to room. puzzle script changes "solved" variable when some condition is met.

public class PuzzleRoom : Room
{
    // only puzzles two and three are currently complete.  so i'll choose randomly between those two.
    
    protected Inventory inventory;
    public enum PuzzleType { Two, Three, Four, Five };
    [SerializeField]
    public PuzzleType pt = PuzzleType.Four;
    public bool solved = false;
    protected bool locked = false;
    protected PuzzleRoom R;
    private AudioClip solvedSound;
    private AudioSource audioSource;
    GameObject text;
    TextMeshProUGUI TMP; 
    CanvasGroup textCanvas;
    // the PuzzleRoom will lock all doors upon entry till you solve it
    
    public new void Awake() {
        base.Awake();
        text = new GameObject("Text");
        text.transform.parent = this.transform;
        inventory = Inventory.instance;
        lightColor = RoomGenerator.Red;

    }

    public new void Start()
    {
        R = this.GetComponent<PuzzleRoom>();
        TMP = text.AddComponent<TextMeshProUGUI>();
        TMP.margin = new Vector4(10,0,0,10);
        TMP.fontSize = 18;
        TMP.alignment = TextAlignmentOptions.BottomLeft;
        text.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        textCanvas = text.AddComponent<CanvasGroup>();
        text.SetActive(false);
        solvedSound = (AudioClip)Resources.Load("Michael/Audio/Bubble_1");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
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

    protected new void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            PlayerInRoom = true;
            if(!solved) {
                text.SetActive(true);
                StartCoroutine(FadeText(textCanvas, 0f, 1f, 3f));
            }
            locked = true;
            foreach(GameObject d in R.DoorList)
                d.GetComponent<OpenDoor>().Lock();
            R.SetLighting(lightColor,2);
        }
    }
    
    protected void OnTriggerExit(Collider other) {
        if(other.gameObject == Player) {
            PlayerInRoom = false;
            StartCoroutine(FadeText(textCanvas,textCanvas.alpha,0f,1f));
        }
    }

    protected void ShowInstructions(System.String txt) {
        text.GetComponent<TextMeshProUGUI>().text = txt;
    }

    public void Update()
    {
        if (solved && locked)
        {
            locked = false;
            StartCoroutine(FadeText(textCanvas,1f,0f,1f));
            foreach (GameObject d in R.DoorList)
                d.GetComponent<OpenDoor>().Unlock();
            lightColor = RoomGenerator.LightGreen;
            SetLighting(lightColor,2);
        }

    }
}
