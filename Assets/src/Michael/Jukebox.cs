using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour {

    public AudioClip song;
    [SerializeField]
    private float volume;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(song,volume);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetVolume(float v) { this.volume = v; }
}
