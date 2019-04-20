/*	OK_LaserBeamLauncher.cs
 *	Name: Oshan Karki
 *	Description: Handles the mechanism and behavior of laser particle.
 				 Takes input and calculates hit score.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamLauncher : MonoBehaviour 
{

	public ParticleSystem laserBeamLauncher;
	public int hitScore = 0;

	void Start () 
	{
        laserBeamLauncher = this.GetComponent<ParticleSystem>();
			
	}
	
	void Update () 
	{
		if(Input.GetButtonDown("Fire1"))
		{
		    Debug.Log("fire.");
		    laserBeamLauncher.Play();

            // instead of Destroy, should be 'enemy.health--' or something.
            RaycastHit hit;
            if(Physics.Raycast(this.transform.position,this.transform.forward,out hit)) 
            {
                if(hit.transform.tag == "Enemy")
                {
                    Destroy(hit.transform.gameObject);
                    hitScore += 10;
                }
            }
		}

		if(Input.GetButtonUp("Fire2"))
		{
		    laserBeamLauncher.Stop();
		    //laserBeamLauncher.Pause();
		}


        //this.transform.rotation = Camera.main.transform.rotation;
	}
}
