using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLowGravity : PuzzleRoom {

    private bool gravityOff;
    BoxCollider roomCollider;
    float speed = 2;
    Vector3 defaultGravity;
    float PlayerExtraGravity;
    ParticleSystem ps;
    float FOV,defFOV;

	new void Awake () {
        base.Awake();
        complexity = -1;
        defaultGravity = Physics.gravity;
        PlayerExtraGravity = Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity;
        defFOV = Camera.main.fieldOfView;
        FOV = 100;
		
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
        roomCollider.size = new Vector3(roomCollider.size.x,Vector3.Distance(Ceiling.transform.position,Floor.transform.position),roomCollider.size.z);
        ps = this.gameObject.AddComponent<ParticleSystem>();
        ps.Stop();
        var psr = this.gameObject.GetComponent<ParticleSystemRenderer>();
        psr.material = Resources.Load<Material>("Michael/Materials/ParticleGlow");
        var main = ps.main;
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.1f,1.0f);
        main.startColor = new ParticleSystem.MinMaxGradient(new Color(1,0,1,0.5f),new Color(0,0,1,0.5f));
        main.maxParticles = 5000;
        ps.emissionRate = 100;
        var sh = ps.shape;
        sh.shapeType = ParticleSystemShapeType.Box;
        sh.position = new Vector3(size.x/2,Floor.transform.position.y+Vector3.Distance(Ceiling.transform.position,Floor.transform.position)/2,size.z/2);
        sh.scale = this.GetComponent<Collider>().bounds.size;
        sh.randomDirectionAmount= 1;


        solved = true;
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        if(gravityOff) {
            float fwd = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            Player.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward.normalized*fwd*speed,ForceMode.Impulse);
            Player.GetComponent<Rigidbody>().AddForce(Camera.main.transform.right.normalized*h*speed,ForceMode.Impulse);
        }
		
	}

    new void OnTriggerEnter(Collider other) {
        if(other.gameObject == Player) {
            ps.Play();
            Camera.main.fieldOfView = FOV;
            base.OnTriggerEnter(other);
            Physics.gravity = Vector3.zero;
            Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity = 0;
            Player.GetComponent<Rigidbody>().AddForce(new Vector3(0,100,0),ForceMode.Acceleration);
            gravityOff = true;
        }
    }
    new void OnTriggerExit(Collider other) {
        if(other.gameObject == Player) {
            ps.Stop();
            base.OnTriggerExit(other);
            Camera.main.fieldOfView = defFOV;
            Physics.gravity = defaultGravity;
            Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity = 0;
            Player.GetComponent<Rigidbody>().AddForce(new Vector3(0,100,0),ForceMode.Acceleration);
            Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity = PlayerExtraGravity;
        }
    }


}
