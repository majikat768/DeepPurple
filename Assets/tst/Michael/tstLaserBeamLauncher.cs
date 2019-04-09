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

            // instead of Destroy, try 'enemy.health--' or something
            RaycastHit hit;
            if(Physics.Raycast(this.transform.position,this.transform.forward,out hit)) {
                if(hit.transform.tag == "Enemy")
                    Destroy(hit.collider.gameObject);
            }
		}

		if(Input.GetButtonUp("Fire1"))
		{
		    laserBeamLauncher.Stop();
		    //laserBeamLauncher.Pause();
		}


        //this.transform.rotation = Camera.main.transform.rotation;
	}
}
