using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLowGravity : PuzzleRoom {
    private bool gravityOff;
    BoxCollider roomCollider;
    public float speed = 1;
    Vector3 defaultGravity;
    float PlayerExtraGravity;
    ParticleSystem ps;
    float FOV,defFOV;
    GameObject bubble;
    GameObject Snitch;
    int numTargets = 5;
    int numShapes = 11;
    GameObject shapes;
    public List<GameObject> SnitchList;
    AudioSource ambienceSource;
    AudioClip echo;

	protected override void Awake () {
        base.Awake();
        instructions = "catch the fireballs";
        TimeLimit = 200.0f;
        shapes = new GameObject("shapes");
        shapes.transform.parent = this.transform;
        SnitchList = new List<GameObject>();
        complexity = -1;
        PlayerExtraGravity = Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity;
        defaultGravity = Physics.gravity;
        defFOV = Camera.main.fieldOfView;
        FOV = 100;
        Snitch = Resources.Load<GameObject>("Michael/GoldenSnitch");
        ambienceSource = this.gameObject.AddComponent<AudioSource>();
        echo = Resources.Load<AudioClip>("Michael/Audio/FloatingAmbience");
		
	}

    protected override void Start() {
        base.Start();
        roomCollider = this.GetComponent<BoxCollider>();

        Floor.transform.position = Zero+new Vector3(size.x/2,-size.y*6,size.z/2);
        Ceiling.transform.position = Zero+new Vector3(size.x/2,size.y*7,size.z/2);

        bubble = GameObject.Instantiate(Resources.Load<GameObject>("Michael/Bubble"));
        bubble.transform.position = Vector3.zero;
        bubble.transform.parent = transform;
        bubble.transform.localScale = new Vector3(
            Player.GetComponent<CapsuleCollider>().height,
            Player.GetComponent<CapsuleCollider>().height,
            Player.GetComponent<CapsuleCollider>().height);
        bubble.GetComponent<ParticleSystem>().Stop();
        bubble.SetActive(false);
        
        for(int i = 0; i < numTargets; i++) {
            GameObject snitch = GameObject.Instantiate(Snitch,Zero+new Vector3(Random.Range(1,size.x-2),Random.Range(Floor.transform.position.y+2,Ceiling.transform.position.y-2),Random.Range(1,size.z-2)),Quaternion.identity,transform);
            SnitchList.Add(snitch);
        }

    	BuildWall(Zero + new Vector3(0,size.y,0),Zero + new Vector3(size.x,size.y,0),size.y*6,false);
	    BuildWall(Zero + new Vector3(0,size.y,0),Zero + new Vector3(0,size.y,size.z),size.y*6,false);
	    BuildWall(Zero + new Vector3(size.x,size.y,0),Zero + new Vector3(size.x,size.y,size.z),size.y*6,false);
        BuildWall(Zero + new Vector3(0,size.y,size.z),Zero + new Vector3(size.x,size.y,size.z),size.y*6,false);

    	BuildWall(Zero + new Vector3(0,-size.y*6,0),Zero + new Vector3(size.x,-size.y*6,0),size.y*6,false);
	    BuildWall(Zero + new Vector3(0,-size.y*6,0),Zero + new Vector3(0,-size.y*6,size.z),size.y*6,false);
	    BuildWall(Zero + new Vector3(size.x,-size.y*6,0),Zero + new Vector3(size.x,-size.y*6,size.z),size.y*6,false);
        BuildWall(Zero + new Vector3(0,-size.y*6,size.z),Zero + new Vector3(size.x,-size.y*6,size.z),size.y*6,false);

        roomCollider.size = new Vector3(roomCollider.size.x,Vector3.Distance(Ceiling.transform.position,Floor.transform.position),roomCollider.size.z);
        foreach(Transform w in Walls.transform) {
            w.GetComponent<Collider>().material.dynamicFriction = 0.3f;
            w.GetComponent<Collider>().material.staticFriction = 0.3f;
            w.GetComponent<Collider>().material.bounciness = 1f;
        }
        Floor.GetComponent<Collider>().material.dynamicFriction = 0.3f;
        Floor.GetComponent<Collider>().material.staticFriction = 0.3f;
        Floor.GetComponent<Collider>().material.bounciness = 1f;

        GenerateSparkles();
        GenerateShapes();

    }
	
	// Update is called once per frame
	protected override void Update () {
        if(inventory == null)   inventory = Inventory.instance;
        if(gravityOff) {
            float fwd = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            bubble.transform.position = Player.transform.position+new Vector3(0,Player.GetComponent<CapsuleCollider>().height/2,0);
            Player.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward.normalized*fwd*speed,ForceMode.Impulse);
            Player.GetComponent<Rigidbody>().AddForce(Camera.main.transform.right.normalized*h*speed,ForceMode.Impulse);
            if(!ambienceSource.isPlaying) {
                ambienceSource.pitch = Random.Range(0.5f,2.0f);
                ambienceSource.PlayOneShot(echo,Random.Range(0.5f,1.0f));
            }
        }
	}

    protected override void CheckSolveConditions() {
        if(SnitchList.Count == 0) {
            solved = true;
            inventory.incScore((int)TimeLimit);
            UnlockRoom();
        }
    }

    protected override void OnTriggerEnter(Collider other) {
        if(other.gameObject == Player) {
            ps.Play();
            Camera.main.fieldOfView = FOV;
            base.OnTriggerEnter(other);
            Physics.gravity = Vector3.zero;
            Player.GetComponent<Rigidbody>().AddForce(new Vector3(0,100,0),ForceMode.Acceleration);
            Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity = 0;
            gravityOff = true;
            bubble.GetComponent<ParticleSystem>().Play();
            bubble.SetActive(true);

            foreach(Transform o in shapes.transform) {
                o.position = Zero + new Vector3(Random.Range(1,size.x-2),Random.Range(Floor.transform.position.y+2,Ceiling.transform.position.y-2),Random.Range(1,size.z-2));
                o.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1,1),Random.Range(-1,1),Random.Range(-1,1)).normalized*2,ForceMode.Impulse);
                o.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1,1),Random.Range(-1,1),Random.Range(-1,1)).normalized*2,ForceMode.Impulse);
            }
        }
    }

    protected override void OnTriggerExit(Collider other) {
        if(other.gameObject == Player) {
            bubble.transform.position = Vector3.zero;
            ps.Stop();
            base.OnTriggerExit(other);
            Camera.main.fieldOfView = defFOV;
            Physics.gravity = defaultGravity;
            Player.GetComponent<Invector.CharacterController.vThirdPersonController>().extraGravity = PlayerExtraGravity;
            gravityOff = false;
            bubble.GetComponent<ParticleSystem>().Stop();
            bubble.SetActive(false);
        }
    }

    void GenerateSparkles() {
        ps = this.gameObject.AddComponent<ParticleSystem>();
        ps.Stop();
        var psr = this.gameObject.GetComponent<ParticleSystemRenderer>();
        psr.material = Resources.Load<Material>("Michael/Materials/ParticleGlow");
        var main = ps.main;
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.1f,1.0f);
        main.startColor = new ParticleSystem.MinMaxGradient(new Color(0,1,0,0.5f),new Color(1,0.5f,0.5f,0.5f));
        main.maxParticles = 5000;
        ps.emissionRate = 100;
        var sh = ps.shape;
        sh.shapeType = ParticleSystemShapeType.Box;
        sh.position = new Vector3(size.x/2,Floor.transform.position.y+Vector3.Distance(Ceiling.transform.position,Floor.transform.position)/2,size.z/2);
        sh.scale = this.GetComponent<Collider>().bounds.size;
        sh.randomDirectionAmount= 1;
    }

    void GenerateShapes() {
        GameObject[] shapesList = Resources.LoadAll<GameObject>("Michael/Shapes/").Concat(Resources.LoadAll<GameObject>("Kyle/Items/Invulnerability")).ToArray();
        for(int i = 0; i < numShapes; i++) {
            GameObject shapeRef = shapesList[Random.Range(0,shapesList.Length)];
            GameObject s = GameObject.Instantiate(shapeRef);
            s.transform.position = Zero+size/2;
            s.transform.localScale *= Random.Range(1.0f,2.0f);
            s.transform.parent = shapes.transform;

        }
        GameObject Butterfly = GameObject.Instantiate(Resources.Load<GameObject>("Michael/Butterfly (Animated)/Butterfly"),Zero+new Vector3(Random.Range(4,size.x-5),Random.Range(Floor.transform.position.y,Ceiling.transform.position.y/4),Random.Range(4,size.z-5)),Quaternion.identity,this.transform);
    }

}
