using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamLauncher : MonoBehaviour 
{

	public ParticleSystem laserBeamLauncher;


	// Use this for initialization
	void Start () 
	{
        laserBeamLauncher = this.GetComponent<ParticleSystem>();
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Fire2"))
		{
            Debug.Log("fire.");
            laserBeamLauncher.Play();
            //laserBeamLauncher.Emit(1);
		}
		if(Input.GetButtonUp("Fire2"))
        {
            laserBeamLauncher.Stop();
            //laserBeamLauncher.Pause();
        }
	}
}
