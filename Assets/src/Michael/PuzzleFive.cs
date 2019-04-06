using UnityEngine;
using System.Collections.Generic;

// This is a work in progress.
// Mario style platformer, or something.

public class PuzzleFive : MonoBehaviour {
    GameObject Platform; 
    GameObject Trampoline; 
    GameObject Coin;
    PuzzleRoom R;
    GameObject player;
    private float FieldOfView;
    Vector3 Zero,size;
    public bool solved;
    private List<MovingPlatform> MovingPlatforms;
    private BoxCollider roomCollider;
    GameObject p8;

    public void Awake()
    {
        R = this.GetComponent<PuzzleRoom>();
        MovingPlatforms = new List<MovingPlatform>();
        FieldOfView = Camera.main.fieldOfView;
        Platform = Resources.Load<GameObject>("Michael/platform");
        Trampoline = Resources.Load<GameObject>("Michael/Trampoline");
        Coin = Resources.Load<GameObject>("Gabriel/Items/GameObjects/CurrencyItem");
        R.complexity = -1;
    }
    public void Start()
    {
        Zero = R.GetZero();
        size = R.GetSize();
        roomCollider = this.GetComponent<BoxCollider>();
        player = GameObject.FindWithTag("Player");

        Destroy(this.transform.Find("Ceiling").gameObject);
        R.BuildWall(Zero + new Vector3(0,size.y,0),Zero + new Vector3(size.x,size.y,0),size.y*6,false);
        R.BuildWall(Zero + new Vector3(0,size.y,0),Zero + new Vector3(0,size.y,size.z),size.y*6,false);
        R.BuildWall(Zero + new Vector3(size.x,size.y,0),Zero + new Vector3(size.x,size.y,size.z),size.y*6,false);
        R.BuildWall(Zero + new Vector3(0,size.y,size.z),Zero + new Vector3(size.x,size.y,size.z),size.y*6,false);

        roomCollider.center = new Vector3(size.x/2,size.y*7/2,size.z/2);    
        roomCollider.size = new Vector3(roomCollider.size.x,roomCollider.size.y*7,roomCollider.size.z);

        BuildPlatforms();
	}

	void Update () {
        if(R.PlayerInRoom) {
            foreach(MovingPlatform p in MovingPlatforms) {
                if(!p.OnlyMoveWithPlayer)
                    p.moving = true;
            }
        }
        else if(!R.PlayerInRoom) {
            foreach(MovingPlatform p in MovingPlatforms) {
                p.moving = false;
            }
        }


        if (!R.solved)
        {
            if(player.transform.position.y >= p8.transform.position.y)  R.solved = true;

        }

	}

    private void BuildPlatforms() {
        GameObject c;
        float stepHeight = size.y;
        GameObject p1 = GameObject.Instantiate(Platform);
        p1.transform.position = Zero+new Vector3(Platform.GetComponent<Renderer>().bounds.size.x*2,size.y,size.z/2-5);
        p1.transform.parent = this.transform;
        p1.transform.localScale = new Vector3(2,1,size.z/2);
        AddCoin(p1.transform);

        GameObject ramp = BuildRamp(Zero+new Vector3(size.x/1.5f,0,size.z/5),new Vector3(p1.transform.position.x+1,p1.transform.position.y,Zero.z+size.z/5));
        AddCoin(ramp.transform);

        GameObject elevator = GameObject.Instantiate(Platform);
        elevator.transform.position = p1.transform.position+new Vector3(-elevator.GetComponent<Renderer>().bounds.size.x,-1,p1.GetComponent<Renderer>().bounds.size.z/2);
        elevator.transform.parent = this.transform;
        elevator.AddComponent<MovingPlatform>().Init(elevator.transform.position,elevator.transform.position + new Vector3(0,elevator.transform.position.y+1,0));
        MovingPlatforms.Add(elevator.GetComponent<MovingPlatform>());
        AddCoin(elevator.transform);

        GameObject p2 = GameObject.Instantiate(Platform);
        p2.transform.position = elevator.GetComponent<MovingPlatform>().end+new Vector3(0,0,3);
        p2.transform.parent = this.transform;
        p2.transform.localScale = new Vector3(3,0.5f,2);
        AddCoin(p2.transform);

        GameObject p3 = GameObject.Instantiate(Platform);
        p3.transform.position = p2.transform.position + new Vector3(0,2,6);
        p3.transform.parent = this.transform;
        p3.transform.localScale = new Vector3(3,0.5f,2);
        AddCoin(p3.transform);

        GameObject ramp2 = BuildRamp(p2.transform.position + new Vector3(0,0,1),p3.transform.position - new Vector3(0,0,1));
        AddCoin(ramp2.transform);


        GameObject mover2 = GameObject.Instantiate(Platform);
        mover2.transform.position = p3.transform.position + new Vector3(3,-1,0);
        mover2.transform.parent = this.transform;
        mover2.AddComponent<MovingPlatform>().Init(mover2.transform.position, mover2.transform.position + new Vector3(1,8,0),2);
        MovingPlatforms.Add(mover2.GetComponent<MovingPlatform>());
        AddCoin(mover2.transform);

        GameObject mover3 = GameObject.Instantiate(Platform);
        mover3.transform.position = mover2.GetComponent<MovingPlatform>().end + new Vector3(4,-1,0);
        mover3.transform.parent = this.transform;
        mover3.transform.localScale = new Vector3(4,0.5f,3);
        mover3.AddComponent<MovingPlatform>().Init(mover3.transform.position,mover3.transform.position + new Vector3(0,5,0),2, true);
        MovingPlatforms.Add(mover3.GetComponent<MovingPlatform>());
        AddCoin(mover3.transform);

        GameObject mover4 = GameObject.Instantiate(Platform);
        mover4.transform.position = mover3.GetComponent<MovingPlatform>().end+new Vector3(0,0,-mover3.GetComponent<Renderer>().bounds.size.z/2-mover4.GetComponent<Renderer>().bounds.size.z/2);
        mover4.transform.parent = this.transform;
        mover4.transform.localScale = new Vector3(2,0.5f,2);
        mover4.AddComponent<MovingPlatform>().Init(mover4.transform.position,new Vector3(mover4.transform.position.x,mover4.transform.position.y,Zero.z+mover4.GetComponent<Renderer>().bounds.size.z*2),3,true);
        AddCoin(mover4.transform);

        GameObject p4 = GameObject.Instantiate(Platform);
        p4.transform.position = mover4.GetComponent<MovingPlatform>().end + new Vector3(p4.GetComponent<Renderer>().bounds.size.x,0,0);
        p4.transform.localScale = new Vector3(2,0.2f,2);
        AddCoin(p4.transform);

        GameObject p5 = GameObject.Instantiate(Platform);
        p5.transform.position = p4.transform.position + new Vector3(3,1.0f,0);
        p5.transform.localScale = new Vector3(2,0.2f,2);
        AddCoin(p5.transform);

        GameObject p6 = GameObject.Instantiate(Platform);
        p6.transform.position = p5.transform.position + new Vector3(3,1.0f,0);
        p6.transform.localScale = new Vector3(2,0.2f,2);
        AddCoin(p6.transform);

        GameObject mover5 = GameObject.Instantiate(Platform);
        mover5.transform.position = p6.transform.position + new Vector3(3,1.0f,0);
        mover5.transform.localScale = new Vector3(2,0.2f,2);
        mover5.AddComponent<MovingPlatform>().Init(mover5.transform.position,new Vector3(mover5.transform.position.x,mover5.transform.position.y+1,Zero.z+size.z/2));
        AddCoin(mover5.transform);

        GameObject p7 = GameObject.Instantiate(Platform);
        p7.transform.localScale = new Vector3(3,0.2f,3);
        p7.transform.position = mover5.GetComponent<MovingPlatform>().end+new Vector3(0,0,p7.GetComponent<Renderer>().bounds.size.z);
        AddCoin(p7.transform);

        p8 = GameObject.Instantiate(Platform);
        p8.transform.localScale = new Vector3(3,0.2f,3);
        p8.transform.position = new Vector3(Zero.x+size.x/2,p7.transform.position.y+2,p7.transform.position.z);
        AddCoin(p8.transform);

        GameObject ramp3 = BuildRamp(p7.transform.position-new Vector3(p7.GetComponent<Renderer>().bounds.size.x/2,0,0),p8.transform.position+new Vector3(p8.GetComponent<Renderer>().bounds.size.x/2,0,0));
        AddCoin(ramp3.transform);

        GameObject trampoline = GameObject.Instantiate(Trampoline);
        trampoline.transform.position = Zero + size/2 - new Vector3(-trampoline.GetComponent<Renderer>().bounds.size.x/2,size.y/2-1,0);
    }

    public void AddCoin(Transform platform) {
        GameObject c = GameObject.Instantiate(Coin);
        c.transform.position = platform.position + new Vector3(0,platform.gameObject.GetComponent<Renderer>().bounds.size.y+c.GetComponent<Renderer>().bounds.size.y,0);
        c.name = "Coin";
    }

    public GameObject BuildRamp(Vector3 start, Vector3 end) {
        Vector3 midpoint = (start + end)/2;
        GameObject ramp = GameObject.Instantiate(Platform,midpoint,Quaternion.identity,this.transform);
        ramp.transform.LookAt(end);
        ramp.transform.Rotate(0,90,0);
        ramp.transform.localScale = new Vector3(Vector3.Distance(start,end),0.25f,2);
        return ramp;
    }

    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}
