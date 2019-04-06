using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSix : PuzzleRoom {

    private bool gravityOff;
    BoxCollider roomCollider;
    float speed = 10;
    Vector3 defaultGravity;
    float PlayerExtraGravity;

	new void Awake () {
        base.Awake();
        defaultGravity = Physics.gravity;
        PlayerExtraGravity = Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity;
		
	}

    new void Start() {
        base.Start();
        roomCollider = this.GetComponent<BoxCollider>();


	R.BuildWall(Zero + new Vector3(0,size.y,0),Zero + new Vector3(size.x,size.y,0),size.y*6,false);
	        R.BuildWall(Zero + new Vector3(0,size.y,0),Zero + new Vector3(0,size.y,size.z),size.y*6,false);
	        R.BuildWall(Zero + new Vector3(size.x,size.y,0),Zero + new Vector3(size.x,size.y,size.z),size.y*6,false);
        R.BuildWall(Zero + new Vector3(0,size.y,size.z),Zero + new Vector3(size.x,size.y,size.z),size.y*6,false);

	R.BuildWall(Zero + new Vector3(0,-size.y*6,0),Zero + new Vector3(size.x,-size.y*6,0),size.y*6,false);
	        R.BuildWall(Zero + new Vector3(0,-size.y*6,0),Zero + new Vector3(0,-size.y*6,size.z),size.y*6,false);
	        R.BuildWall(Zero + new Vector3(size.x,-size.y*6,0),Zero + new Vector3(size.x,-size.y*6,size.z),size.y*6,false);
        R.BuildWall(Zero + new Vector3(0,-size.y*6,size.z),Zero + new Vector3(size.x,-size.y*6,size.z),size.y*6,false);

    Floor.transform.position = Zero+new Vector3(size.x/2,-size.y*6,size.z/2);
    Ceiling.transform.position = Zero+new Vector3(size.x/2,size.y*7,size.z/2);
    ParticleSystem ps = Ceiling.AddComponent<ParticleSystem>();
    Ceiling.GetComponent<ParticleSystemRenderer>().material.shader = Shader.Find("Default-Particle");


    solved = true;
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        if(gravityOff) {
            float fwd = Input.GetAxis("Vertical");
                Player.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward.normalized*fwd*speed,ForceMode.Impulse);
        }
		
	}

    new void OnTriggerEnter(Collider other) {
        if(other.gameObject == Player) {
            base.OnTriggerEnter(other);
            Physics.gravity = Vector3.zero;
            Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity = 0;
            Player.GetComponent<Rigidbody>().AddForce(new Vector3(0,100,0),ForceMode.Acceleration);
            gravityOff = true;
        }
    }
    new void OnTriggerExit(Collider other) {
        if(other.gameObject == Player) {
            base.OnTriggerExit(other);
            Physics.gravity = defaultGravity;
            Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity = 0;
            Player.GetComponent<Rigidbody>().AddForce(new Vector3(0,100,0),ForceMode.Acceleration);
            Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity = PlayerExtraGravity;
        }
    }


}
