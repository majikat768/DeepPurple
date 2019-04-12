using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tstLaserBeamLauncher : MonoBehaviour 
{

	public ParticleSystem laserBeamLauncher;
    ParticleSystem.ShapeModule lbsh;

	void Start () 
	{
        laserBeamLauncher = this.GetComponent<ParticleSystem>();
        lbsh = laserBeamLauncher.shape;
        Debug.Log(laserBeamLauncher.shape);
			
	}
	
	void Update () 
	{
		if(Input.GetButtonDown("Fire1"))
		{
		    Debug.Log("fire.");
		    laserBeamLauncher.Play();

            // instead of Destroy, should be 'enemy.health--' or something.
            // setting Death trigger on enemy's Animator calls Death function
            RaycastHit hit;
            if(Physics.Raycast(this.transform.position,this.transform.forward,out hit)) {
                if(hit.transform.tag == "Enemy")
                    hit.transform.GetComponent<Animator>().SetTrigger("Death");
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
