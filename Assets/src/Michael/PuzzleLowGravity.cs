using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLowGravity : PuzzleRoom {

    private bool gravityOff;
    private float FOV;
    BoxCollider roomCollider;
    float speed = 2.5f;
    Vector3 defaultGravity;
    float PlayerExtraGravity;
    ParticleSystem ps;

	new void Awake () {
        base.Awake();
        FOV = Camera.main.fieldOfView;
        defaultGravity = Physics.gravity;
        PlayerExtraGravity = Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity;
        complexity = -1;
		
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
        roomCollider.size = new Vector3(roomCollider.size.x, roomCollider.size.y * 14, roomCollider.size.z);
        ps = R.gameObject.AddComponent<ParticleSystem>();
        var main = ps.main;
        ParticleSystemRenderer psr = R.gameObject.GetComponent<ParticleSystemRenderer>();
        psr.material = Resources.Load<Material>("Michael/Materials/ParticleGlow");
        var sh = ps.shape;
        sh.shapeType = ParticleSystemShapeType.Box;
        sh.position = new Vector3(size.x/2, Floor.transform.position.y+Vector3.Distance(Ceiling.transform.position, Floor.transform.position)/2, size.z/2);
        sh.scale = R.GetComponent<Collider>().bounds.size;
        sh.randomDirectionAmount = 360;
        
        sh.angle = 0;
        sh.radius = Mathf.Min(size.x, size.z) / 3;
        main.scalingMode = ParticleSystemScalingMode.Shape;
        main.startSize = 0.5f;
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.5f, 1.0f);
        main.startLifetime = Vector3.Distance(Ceiling.transform.position, Floor.transform.position) * main.startSpeed.constantMax;
        main.maxParticles = 5000;
        var pse = ps.emission;
        pse.rateOverTime = 100;
        main.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 0, 1,0.5f), new Color(0, 1, 1,0.5f));
        ps.Stop();
        


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
            base.OnTriggerEnter(other);
            Physics.gravity = Vector3.zero;
            Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity = 0;
            Player.GetComponent<Rigidbody>().AddForce(new Vector3(0,100,0),ForceMode.Acceleration);
            gravityOff = true;
            Camera.main.fieldOfView = 100;
        }
    }
    new void OnTriggerExit(Collider other) {
        if(other.gameObject == Player) {
            ps.Stop();
            base.OnTriggerExit(other);
            Physics.gravity = defaultGravity;
            Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity = 0;
            Player.GetComponent<Rigidbody>().AddForce(new Vector3(0,100,0),ForceMode.Acceleration);
            Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity = PlayerExtraGravity;
            Camera.main.fieldOfView = FOV;
        }
    }


}
