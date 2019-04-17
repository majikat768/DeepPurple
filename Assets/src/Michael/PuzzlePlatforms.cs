using UnityEngine;
using System.Collections.Generic;

// This is a work in progress.
// Mario style platformer, or something.

public class PuzzlePlatforms : PuzzleRoom {
    GameObject Platform; 
    GameObject Trampoline; 
    GameObject Coin;
    GameObject Potion;
    private List<MovingPlatform> MovingPlatforms;
    private List<GameObject> Platforms;
    GameObject trampoline;
    private BoxCollider roomCollider;
    GameObject p8;
    GameObject hoop;
    ParticleSystem hoopFire;
    Vector3 platformSize;
    float height;

    public override void Awake()
    {
        base.Awake();
        TimeLimit = 100;
        MovingPlatforms = new List<MovingPlatform>();
        Platforms = new List<GameObject>();
        Platform = Resources.Load<GameObject>("Michael/platform");
        Trampoline = Resources.Load<GameObject>("Michael/Trampoline");
        Coin = Resources.Load<GameObject>("Gabriel/Items/GameObjects/CurrencyItem");
        Potion = Resources.Load<GameObject>("Kyle/Items/Invulnerability");
        platformSize = Platform.GetComponent<Renderer>().bounds.size;
        complexity = -1;
    }
    protected void Start()
    {
        height = size.y*7;
        instructions = "leap through the ring of fire";
        roomCollider = this.GetComponent<BoxCollider>();

        Destroy(this.transform.Find("Ceiling").gameObject);
        BuildWall(Zero + new Vector3(0,size.y,0),Zero + new Vector3(size.x,size.y,0),height,false);
        BuildWall(Zero + new Vector3(0,size.y,0),Zero + new Vector3(0,size.y,size.z),height,false);
        BuildWall(Zero + new Vector3(size.x,size.y,0),Zero + new Vector3(size.x,size.y,size.z),height,false);
        BuildWall(Zero + new Vector3(0,size.y,size.z),Zero + new Vector3(size.x,size.y,size.z),height,false);

        roomCollider.center = new Vector3(size.x/2,(size.y+height)/2,size.z/2);
        roomCollider.size = new Vector3(roomCollider.size.x,roomCollider.size.y+height,roomCollider.size.z);

        BuildPlatforms();
	}

	protected override void Update () {
        if(PlayerInRoom) {
            foreach(MovingPlatform p in MovingPlatforms) {
                if(!p.OnlyMoveWithPlayer)
                    p.moving = true;
            }
        }
        else if(!PlayerInRoom) {
            foreach(MovingPlatform p in MovingPlatforms) {
                p.moving = false;
            }
        }

	}

    private void BuildPlatforms() {
        GameObject c;
        float stepHeight = size.y;
        GameObject p1 = GameObject.Instantiate(Platform);
        p1.transform.position = Zero+new Vector3(Platform.GetComponent<Renderer>().bounds.size.x*2,size.y,size.z/2-5);
        p1.transform.parent = this.transform;
        p1.transform.localScale = new Vector3(4,1,size.z/4);
        Platforms.Add(p1);

        GameObject p0 = GameObject.Instantiate(Platform);
        p0.transform.position = Zero+new Vector3(3*size.x,0,size.z)/4;
        p0.transform.parent = this.transform;
        p0.transform.localScale = new Vector3(2,2,2);
        p0.AddComponent<JumpPad>().target = p1.transform;
        //GameObject ramp = BuildRamp(Zero+new Vector3(size.x/1.5f,0,size.z/5),new Vector3(p1.transform.position.x+p1.GetComponent<Renderer>().bounds.size.x/2,p1.transform.position.y,Zero.z+size.z/5));

        GameObject elevator = GameObject.Instantiate(Platform);
        elevator.transform.position = p1.transform.position+new Vector3(elevator.GetComponent<Renderer>().bounds.size.x,-1,p1.GetComponent<Renderer>().bounds.size.z/1.5f);
        elevator.transform.parent = this.transform;
        elevator.AddComponent<MovingPlatform>().Init(elevator.transform.position,elevator.transform.position + new Vector3(0,elevator.transform.position.y+1,0));
        MovingPlatforms.Add(elevator.GetComponent<MovingPlatform>());
        Platforms.Add(elevator);

        GameObject p2 = GameObject.Instantiate(Platform);
        p2.transform.position = elevator.GetComponent<MovingPlatform>().end+new Vector3(0,0,3);
        p2.transform.parent = this.transform;
        p2.transform.localScale = new Vector3(2,2,2);
        Platforms.Add(p2);

        GameObject p3 = GameObject.Instantiate(Platform);
        p3.transform.position = p2.transform.position + new Vector3(0,2,6);
        p3.transform.parent = this.transform;
        p3.transform.localScale = new Vector3(3,0.5f,3);
        Platforms.Add(p3);

        p2.AddComponent<JumpPad>().target = p3.transform;

        //GameObject ramp2 = BuildRamp(p2.transform.position + new Vector3(0,0,1),p3.transform.position - new Vector3(0,0,1));


        GameObject mover2 = GameObject.Instantiate(Platform);
        mover2.transform.position = p3.transform.position + new Vector3(3,-1,0);
        mover2.transform.parent = this.transform;
        //mover2.AddComponent<MovingPlatform>().Init(mover2.transform.position, mover2.transform.position + new Vector3(1,8,0),1.0f);
        Platforms.Add(mover2);

        GameObject mover3 = GameObject.Instantiate(Platform);
        mover3.transform.position = mover2.transform.position + new Vector3(platformSize.x*3,8,0);
        mover3.transform.parent = this.transform;
        mover3.transform.localScale = new Vector3(3,0.5f,3);
        mover3.AddComponent<MovingPlatform>().Init(mover3.transform.position,Zero+new Vector3(size.x-mover3.GetComponent<Renderer>().bounds.size.x/2,mover3.transform.position.y+5,size.z-mover3.GetComponent<Renderer>().bounds.size.z),2, true);
        MovingPlatforms.Add(mover3.GetComponent<MovingPlatform>());
        Platforms.Add(mover3);
        mover2.AddComponent<JumpPad>().target = mover3.transform;

        GameObject p3p5 = GameObject.Instantiate(Platform);
        p3p5.transform.position = mover3.GetComponent<MovingPlatform>().end+new Vector3(0,0,-mover3.GetComponent<Renderer>().bounds.size.z/2-p3p5.GetComponent<Renderer>().bounds.size.z/2);
        p3p5.transform.localScale = new Vector3(3,0.5f,2);
        p3p5.transform.parent = this.transform;
        Platforms.Add(p3p5);

        GameObject mover4 = GameObject.Instantiate(Platform);
        mover4.transform.position = p3p5.transform.position+new Vector3(0,0,-platformSize.z*2);
        mover4.transform.parent = this.transform;
        mover4.transform.localScale = new Vector3(2,2,2);
        //mover4.AddComponent<MovingPlatform>().Init(mover4.transform.position,new Vector3(mover4.transform.position.x,mover4.transform.position.y,Zero.z+mover4.GetComponent<Renderer>().bounds.size.z/1),2,true);
        Platforms.Add(mover4);

        GameObject p4 = GameObject.Instantiate(Platform);
        p4.transform.localScale = new Vector3(2,0.2f,4);
        p4.transform.position = new Vector3(mover4.transform.position.x,mover4.transform.position.y,Zero.z+platformSize.z*2);
        p4.transform.parent = this.transform;
        Platforms.Add(p4);

        mover4.AddComponent<JumpPad>().target = p4.transform;

        GameObject p5 = GameObject.Instantiate(Platform);
        p5.transform.position = p4.transform.position+new Vector3(-platformSize.x*2,1,0);
        p5.transform.localScale = new Vector3(2,0.2f,2.2f);
        p5.transform.parent = this.transform;
        Platforms.Add(p5);

        GameObject p6 = GameObject.Instantiate(Platform);
        p6.transform.position = p5.transform.position+new Vector3(-platformSize.x*2,1,0);
        p6.transform.localScale = new Vector3(2,0.2f,2.2f);
        p6.transform.parent = this.transform;
        Platforms.Add(p6);

        GameObject p7 = GameObject.Instantiate(Platform);
        p7.transform.position = p6.transform.position+new Vector3(-platformSize.x*2,1,0);
        p7.transform.localScale = new Vector3(2,2,2);
        p7.transform.parent = this.transform;
        Platforms.Add(p7);

        p8 = GameObject.Instantiate(Platform);
        p8.transform.localScale = new Vector3(4,0.5f,4);
        p8.transform.position = Zero+new Vector3(size.x/2,p7.transform.position.y+2,size.z/2);
        p8.transform.parent = this.transform;
        //Platforms.Add(p8);

        AddItem(p8.transform,Potion);
        p7.AddComponent<JumpPad>().target = p8.transform;

        trampoline = GameObject.Instantiate(Trampoline);
        trampoline.transform.position = Zero + new Vector3(size.x,trampoline.GetComponent<Renderer>().bounds.size.y*2,size.z)/2;
        trampoline.transform.parent = this.transform;

        hoop = GameObject.Instantiate(Resources.Load<GameObject>("Michael/Hoop"));
        hoop.transform.position = trampoline.transform.position + new Vector3(0,(p8.transform.position.y+trampoline.transform.position.y)/2,0)-new Vector3(trampoline.GetComponent<Renderer>().bounds.size.x/2,0,0);
        AddItem(hoop.transform,Potion);
        hoopFire = hoop.GetComponent<ParticleSystem>();
        hoopFire.Stop();
        hoop.transform.parent = this.transform;

        foreach(GameObject p in Platforms) {
            p.GetComponent<Renderer>().materials[0].mainTextureScale = new Vector2(p.transform.localScale.x,p.transform.localScale.z)/2;
        }
    }

    public void AddItem(Transform platform,GameObject itemRef) {
        GameObject c = GameObject.Instantiate(itemRef);
        c.transform.position = platform.position + new Vector3(0,platform.gameObject.GetComponent<Renderer>().bounds.size.y+c.GetComponent<Renderer>().bounds.size.y,0);
        c.name = "Coin";
        c.transform.parent = this.transform;
    }

    public GameObject BuildRamp(Vector3 start, Vector3 end) {
        Vector3 midpoint = (start + end)/2;
        GameObject ramp = GameObject.Instantiate(Platform,midpoint,Quaternion.identity,this.transform);
        ramp.transform.LookAt(end);
        ramp.transform.Rotate(0,90,0);
        ramp.transform.localScale = new Vector3(Vector3.Distance(start,end),0.25f,2);
        ramp.transform.parent = this.transform;
        Platforms.Add(ramp);
        return ramp;
    }

    public void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.gameObject == Player)
        {
            if(!solved) {
            if(!hoopFire.isPlaying) hoopFire.Play();
            foreach(GameObject p in Platforms) 
                AddItem(p.transform,Coin);
            }
        }
    }

    public new void OnTriggerExit(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.gameObject == Player)
        {
            if(hoopFire.isPlaying) hoopFire.Stop();
        }
    }
    protected override void CheckSolveConditions() {
        if(hoop.GetComponent<Hoop>().solved) {
            solved = true;
            PlaySolvedSound();
        }
    }

    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}
