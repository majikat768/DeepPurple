using UnityEngine;
using System.Collections.Generic;

// This is a work in progress.
// Mario style platformer, or something.

public class PuzzlePlatforms : PuzzleRoom {
    GameObject Platform; 
    GameObject Trampoline; 
    GameObject Coin;
    private float FOV;
    private List<MovingPlatform> MovingPlatforms;
    private List<GameObject> Platforms;
    GameObject trampoline;
    private BoxCollider roomCollider;
    GameObject p8;

    public new void Awake()
    {
        base.Awake();
        MovingPlatforms = new List<MovingPlatform>();
        Platforms = new List<GameObject>();
        FOV = Camera.main.fieldOfView;
        Platform = Resources.Load<GameObject>("Michael/platform");
        Trampoline = Resources.Load<GameObject>("Michael/Trampoline");
        Coin = Resources.Load<GameObject>("Gabriel/Items/GameObjects/CurrencyItem");
        complexity = -1;
    }
    public new void Start()
    {
        base.Start();
        roomCollider = this.GetComponent<BoxCollider>();

        Destroy(this.transform.Find("Ceiling").gameObject);
        R.BuildWall(Zero + new Vector3(0,size.y,0),Zero + new Vector3(size.x,size.y,0),size.y*6,false);
        R.BuildWall(Zero + new Vector3(0,size.y,0),Zero + new Vector3(0,size.y,size.z),size.y*6,false);
        R.BuildWall(Zero + new Vector3(size.x,size.y,0),Zero + new Vector3(size.x,size.y,size.z),size.y*6,false);
        R.BuildWall(Zero + new Vector3(0,size.y,size.z),Zero + new Vector3(size.x,size.y,size.z),size.y*6,false);

        roomCollider.center = new Vector3(size.x/2,size.y*7/2,size.z/2);    
        roomCollider.size = new Vector3(roomCollider.size.x,roomCollider.size.y*7,roomCollider.size.z);

        BuildPlatforms();
	}

	new void Update () {
        base.Update();
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
        p1.transform.localScale = new Vector3(3,1,size.z/2);
        Platforms.Add(p1);

        GameObject ramp = BuildRamp(Zero+new Vector3(size.x/1.5f,0,size.z/5),new Vector3(p1.transform.position.x+p1.GetComponent<Renderer>().bounds.size.x/2,p1.transform.position.y,Zero.z+size.z/5));

        GameObject elevator = GameObject.Instantiate(Platform);
        elevator.transform.position = p1.transform.position+new Vector3(-elevator.GetComponent<Renderer>().bounds.size.x,-1,p1.GetComponent<Renderer>().bounds.size.z/2);
        elevator.transform.parent = this.transform;
        elevator.AddComponent<MovingPlatform>().Init(elevator.transform.position,elevator.transform.position + new Vector3(0,elevator.transform.position.y+1,0));
        MovingPlatforms.Add(elevator.GetComponent<MovingPlatform>());
        Platforms.Add(elevator);

        GameObject p2 = GameObject.Instantiate(Platform);
        p2.transform.position = elevator.GetComponent<MovingPlatform>().end+new Vector3(0,0,3);
        p2.transform.parent = this.transform;
        p2.transform.localScale = new Vector3(3,0.5f,2);
        Platforms.Add(p2);

        GameObject p3 = GameObject.Instantiate(Platform);
        p3.transform.position = p2.transform.position + new Vector3(0,2,6);
        p3.transform.parent = this.transform;
        p3.transform.localScale = new Vector3(3,0.5f,2);
        Platforms.Add(p3);

        GameObject ramp2 = BuildRamp(p2.transform.position + new Vector3(0,0,1),p3.transform.position - new Vector3(0,0,1));


        GameObject mover2 = GameObject.Instantiate(Platform);
        mover2.transform.position = p3.transform.position + new Vector3(3,-1,0);
        mover2.transform.parent = this.transform;
        mover2.AddComponent<MovingPlatform>().Init(mover2.transform.position, mover2.transform.position + new Vector3(1,8,0),1.0f);
        MovingPlatforms.Add(mover2.GetComponent<MovingPlatform>());
        Platforms.Add(mover2);

        GameObject mover3 = GameObject.Instantiate(Platform);
        mover3.transform.position = mover2.GetComponent<MovingPlatform>().end + new Vector3(4,-1,0);
        mover3.transform.parent = this.transform;
        mover3.transform.localScale = new Vector3(4,0.5f,3);
        mover3.AddComponent<MovingPlatform>().Init(mover3.transform.position,Zero+new Vector3(size.x-mover3.GetComponent<Renderer>().bounds.size.x/2,mover3.transform.position.y+5,size.z-mover3.GetComponent<Renderer>().bounds.size.z),2, true);
        MovingPlatforms.Add(mover3.GetComponent<MovingPlatform>());
        Platforms.Add(mover3);

        GameObject p3p5 = GameObject.Instantiate(Platform);
        p3p5.transform.position = mover3.GetComponent<MovingPlatform>().end+new Vector3(0,0,-mover3.GetComponent<Renderer>().bounds.size.z/2-p3p5.GetComponent<Renderer>().bounds.size.z/2);
        p3p5.transform.localScale = new Vector3(3,0.5f,2);
        p3p5.transform.parent = this.transform;
        Platforms.Add(p3p5);

        GameObject mover4 = GameObject.Instantiate(Platform);
        mover4.transform.position = p3p5.transform.position+new Vector3(0,0,-p3p5.GetComponent<Renderer>().bounds.size.z/2-mover4.GetComponent<Renderer>().bounds.size.z/2);
        mover4.transform.parent = this.transform;
        mover4.transform.localScale = new Vector3(2,0.5f,2);
        mover4.AddComponent<MovingPlatform>().Init(mover4.transform.position,new Vector3(mover4.transform.position.x,mover4.transform.position.y,Zero.z+mover4.GetComponent<Renderer>().bounds.size.z/1),2,true);
        Platforms.Add(mover4);

        GameObject p4 = GameObject.Instantiate(Platform);
        p4.transform.localScale = new Vector3(2,0.2f,4);
        p4.transform.position = mover4.GetComponent<MovingPlatform>().end + new Vector3(-p4.GetComponent<Renderer>().bounds.size.x,0,0);
        p4.transform.parent = this.transform;
        Platforms.Add(p4);

        GameObject p5 = GameObject.Instantiate(Platform);
        p5.transform.position = Vector3.Lerp(p4.transform.position,new Vector3(p4.transform.position.x+1,p4.transform.position.y+5,Zero.z+size.z/1),0.1f);
        p5.transform.localScale = new Vector3(2,0.2f,2.2f);
        p5.transform.parent = this.transform;
        Platforms.Add(p5);

        GameObject p6 = GameObject.Instantiate(Platform);
        p6.transform.position = Vector3.Lerp(p5.transform.position,new Vector3(p5.transform.position.x+1,p5.transform.position.y+5,Zero.z+size.z/1),0.1f);
        p6.transform.localScale = new Vector3(2,0.2f,2.2f);
        p6.transform.parent = this.transform;
        Platforms.Add(p6);

        GameObject p7 = GameObject.Instantiate(Platform);
        p7.transform.position = p6.transform.position+new Vector3(0,0,p7.GetComponent<Renderer>().bounds.size.z*2);
        p7.transform.localScale = new Vector3(3,0.2f,3);
        p7.transform.parent = this.transform;
        Platforms.Add(p7);

        p8 = GameObject.Instantiate(Platform);
        p8.transform.localScale = new Vector3(3,0.2f,1);
        p8.transform.position = new Vector3(Zero.x+size.x/2,p7.transform.position.y+2,p7.transform.position.z);
        p8.transform.parent = this.transform;
        Platforms.Add(p8);

        GameObject ramp3 = BuildRamp(p7.GetComponent<Renderer>().bounds.ClosestPoint(p8.transform.position), p8.GetComponent<Renderer>().bounds.ClosestPoint(p7.transform.position));

        trampoline = GameObject.Instantiate(Trampoline);
        trampoline.transform.position = new Vector3(p8.transform.position.x,Zero.y+1,p8.transform.position.z);
        trampoline.transform.parent = this.transform;

        GameObject key = GameObject.Instantiate(Resources.Load<GameObject>("Michael/Hoop"));
        key.transform.position = trampoline.transform.position + new Vector3(0,(p8.transform.position.y+trampoline.transform.position.y)/2,0)-new Vector3(trampoline.GetComponent<Renderer>().bounds.size.x/2,0,0);
        key.transform.parent = this.transform;

        foreach(GameObject p in Platforms) {
            p.GetComponent<Renderer>().materials[0].mainTextureScale = new Vector2(p.transform.localScale.x,p.transform.localScale.z)/2;
        }
    }

    public void AddCoin(Transform platform) {
        GameObject c = GameObject.Instantiate(Coin);
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

    public new void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            base.OnTriggerEnter(other);
            foreach(GameObject p in Platforms) 
                AddCoin(p.transform);
            Camera.main.fieldOfView = 100;
        }
    }

    public new void OnTriggerExit(Collider other)
    {
        if(other.gameObject == Player)
        {
            base.OnTriggerEnter(other);
            Camera.main.fieldOfView = FOV;
        }
    }
    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}
