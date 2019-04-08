using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour {

    public AudioClip[] songs;
    int numSongs;
    [SerializeField]
    private float volume;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        songs = Resources.LoadAll<AudioClip>("Audio/Songs");
        audioSource = gameObject.AddComponent<AudioSource>();
        numSongs = songs.Length;
		
	}
	
	// Update is called once per frame
	void Update () {
        if(!audioSource.isPlaying) {
            audioSource.PlayOneShot(songs[Random.Range(0,numSongs)]);
        }
		
	}

    public void SetVolume(float v) { 
        this.volume = v; 
        audioSource.volume = v;
    
    }
}
