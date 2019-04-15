using System;
using System.Collections;
using UnityEngine;
using TMPro;

// provides the countdown timer for puzzle rooms.
/*
 * this utilizes the singleton pattern.
 * the display for the on screen timer is a single instance of PuzzleCountdown;
 * each individual puzzle room accesses the singleton instance, 
 * and updates it according to that specific room.
 *
 */

public class PuzzleCountdown : MonoBehaviour
{
    TextMeshProUGUI TMP;
    CanvasGroup textCanvas;

    public static PuzzleCountdown instance;
    private string instructions;

    private void Awake() {
        if(instance == null)
            instance = this;
    }

    private void Start() {
        TMP = this.gameObject.AddComponent<TextMeshProUGUI>();
        TMP.margin = new Vector4(10,0,0,10);
        TMP.fontSize = 18;
        TMP.alignment = TextAlignmentOptions.BottomLeft;
        this.gameObject.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        textCanvas = this.gameObject.AddComponent<CanvasGroup>();
        this.gameObject.SetActive(true);
    }

    // stolen / adapted from http://unity.grogansoft.com/fade-your-ui-in-and-out/
    public IEnumerator FadeText(float startAlpha, float endAlpha, float duration) {
        var start = Time.time;
        var end = start + duration;

        float elapsed = 0;
        instance.textCanvas.alpha = startAlpha;

        while(Time.time <= end) {
            elapsed = Time.time - start;
            var perc = 1.0f/(duration/elapsed);

            if(startAlpha > endAlpha) {
                instance.textCanvas.alpha = startAlpha - perc;
            }
            else {
                instance.textCanvas.alpha = startAlpha + perc;
            }
            yield return new WaitForEndOfFrame();
        }
        instance.textCanvas.alpha = endAlpha;
    }

    public void SetInstructions(string txt) { instructions = txt; }
    
    public void Count(float TimeLeft) { 
        this.TMP.text = instructions + '\n' + TimeLeft.ToString("#0.00s");
    }

}

