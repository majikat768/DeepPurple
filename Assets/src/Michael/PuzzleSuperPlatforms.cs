using UnityEngine;
using System.Collections.Generic;

public class PuzzleSuperPlatforms : PuzzleRoom 
{
    GameObject Platform;
    int step = 10;  // units between each level 
    int numLayers = 6;
    int numPlatformsPerLayer = 2;
    BoxCollider roomCollider;
    int ceilingHeight;
    Vector3 platformSize;
    List<GameObject> Platforms;
    List<MovingPlatform> MovingPlatforms;
    GameObject goal;
    GameObject Coin;
    GameObject Potion;
    GameObject bottom;

    public override void Awake()
    {
        base.Awake();
        Coin = Resources.Load<GameObject>("Gabriel/Items/GameObjects/Coin");
        Potion = Resources.Load<GameObject>("Kyle/Items/Invulnerability");
        instructions = "reach the top";
        Platforms = new List<GameObject>();
        MovingPlatforms = new List<MovingPlatform>();
        complexity = -1;
        TimeLimit = 200;
        Platform = Resources.Load<GameObject>("Michael/platform");
        ceilingHeight = numLayers*step+step;
        platformSize = Platform.GetComponent<Renderer>().bounds.size;

    }

    protected void Start()
    {
        roomCollider = this.GetComponent<BoxCollider>();
        Destroy(this.transform.Find("Ceiling").gameObject);
        BuildWall(Zero+new Vector3(0,size.y,0),Zero+new Vector3(size.x,size.y,0),ceilingHeight,false);
        BuildWall(Zero+new Vector3(0,size.y,0),Zero+new Vector3(0,size.y,size.z),ceilingHeight,false);
        BuildWall(Zero+new Vector3(size.x,size.y,0),Zero+new Vector3(size.x,size.y,size.z),ceilingHeight,false);
        BuildWall(Zero+new Vector3(0,size.y,size.z),Zero+new Vector3(size.x,size.y,size.z),ceilingHeight,false);
        roomCollider.center = new Vector3(size.x,ceilingHeight,size.z)/2;
        roomCollider.size = new Vector3(roomCollider.size.x,ceilingHeight,roomCollider.size.z);
        BuildPlatforms();
        if(goal != null)    goal.transform.Rotate(0,45,0);
    }

    protected void FixedUpdate() {
        if(PlayerInRoom) {
            foreach(MovingPlatform p in MovingPlatforms) {
                if(!p.OnlyMoveWithPlayer) {
                    p.moving = true;
                }
            }
            goal.transform.Rotate(0,2,0);
        }
        else {
            foreach(MovingPlatform p in MovingPlatforms) {
                if(!p.OnlyMoveWithPlayer)
                    p.moving = false;
            }
        }
        foreach(GameObject p in Platforms) {
            if(p.GetComponent<Renderer>().bounds.ClosestPoint(Player.transform.position).y <= Player.transform.position.y) {
                p.GetComponent<MeshCollider>().enabled = true;
            }
            else {
                p.GetComponent<MeshCollider>().enabled = false;
            } 
        }
    }

    public void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        if(other.gameObject == Player) {
            if(!solved) {
                foreach(GameObject p in Platforms) {
                    AddItem(p.transform,(Random.value < 0.9f ? Coin : Potion));
                }
            }
        }
    }
                            


    // Iterates through every level, 
    // putting numPlatformsPerLayer platforms at that height + 1/platform.  
    // creates moving platforms that move between each static platform.
    // Then creates a jumping platform that goes between levels.
    private void BuildPlatforms() 
    {
        GameObject lastPlatform = GameObject.Instantiate(Platform,Zero+new Vector3(size.x,0,size.z)/2,Quaternion.identity,this.transform);
        Platforms.Add(lastPlatform);
        lastPlatform.name = "Platform";
        bottom = lastPlatform;
        for(int i = step; i <= numLayers*step; i += step) 
        {
            numPlatformsPerLayer = Random.Range(1,4);
            List<GameObject> platformsInLayer = new List<GameObject>();
            for(int j = 0; j < numPlatformsPerLayer; j++) 
            {
                Vector3 loc = new Vector3(
                        Random.Range(platformSize.magnitude/2,size.x-platformSize.magnitude/2), 
                        i+j*(step/numPlatformsPerLayer)/2,
                        Random.Range(j*size.z/numPlatformsPerLayer+platformSize.magnitude/2,(j+1)*size.z/numPlatformsPerLayer-platformSize.magnitude/2));
                if(j > 0) {
                    while(Vector3.Distance(loc,platformsInLayer[j-1].transform.position) < platformSize.magnitude)
                        loc = new Vector3(
                                Random.Range(platformSize.magnitude/2,size.x-platformSize.magnitude/2), 
                                i+j*(step/numPlatformsPerLayer)/2, 
                                Random.Range(j*size.z/numPlatformsPerLayer+platformSize.magnitude/2,(j+1)*size.z/numPlatformsPerLayer-platformSize.magnitude/2));
                }

                GameObject p = GameObject.Instantiate(
                        Platform,Zero+loc,
                        Quaternion.identity,
                        this.transform);
                Platforms.Add(p);
                p.name = "Platform";
                platformsInLayer.Add(p);
                if(j+1 >= numPlatformsPerLayer)   goal = p;
            }

            // Connecting each platform in this level with a moving platform.
            for(int j = 0; j < platformsInLayer.Count-1; j += 1) {
                GameObject p1 = platformsInLayer[j];
                GameObject p2 = platformsInLayer[j+1];
                //AddMovingPlatform(p1,p2);
                if(j % 2 == 0) {
                    AddMovingPlatform(p1,p2);
                }
                else {
                    if(Vector3.Distance(p1.transform.position,p2.transform.position) > platformSize.magnitude) {
                        //GameObject p3 = GameObject.Instantiate(Platform,new Vector3(p1.transform.position.x,i,p2.transform.position.z),Quaternion.identity,this.transform);
                        //p1.transform.LookAt(p2.transform);
                        //p2.transform.LookAt(p1.transform);
                        BuildRamp(Vector3.MoveTowards(p1.transform.position,p2.transform.position,platformSize.x),Vector3.MoveTowards(p2.transform.position,p1.transform.position,platformSize.x));
                        //BuildRamp(p2.transform.position,p3.transform.position);
                    }
                }
            }
            float deltaZ = (platformsInLayer[0].transform.position.z > lastPlatform.transform.position.z ? -platformSize.z : platformSize.z);
            lastPlatform.AddComponent<JumpPad>().target = platformsInLayer[0].transform;
            lastPlatform.name = "Jump Platform";

            lastPlatform = platformsInLayer[platformsInLayer.Count-1];
            //GameObject n = GameObject.Instantiate(Platform,lastPlatform.transform.position-new Vector3(size.x/2,-step,0),Quaternion.identity,this.transform);
            //BuildRamp(lastPlatform.GetComponent<Collider>().ClosestPoint(n.transform.position),n.GetComponent<Collider>().ClosestPoint(lastPlatform.transform.position));
            platformsInLayer.Clear();
        }

    }

    void AddMovingPlatform(GameObject p1, GameObject p2) {
        float deltaX = (p1.transform.position.x > p2.transform.position.x ? -platformSize.x : platformSize.x)*2f;
        Vector3 start = Vector3.MoveTowards(p1.transform.position,p2.transform.position,platformSize.magnitude); 
        Vector3 end = Vector3.MoveTowards(p2.transform.position,p1.transform.position,platformSize.magnitude); 
        //GameObject m = GameObject.Instantiate(Platform,p1.transform.position+new Vector3(deltaX,-platformSize.y,0),Quaternion.identity,this.transform);
        GameObject m = GameObject.Instantiate(Platform,new Vector3(start.x,p1.transform.position.y-platformSize.y,start.z),Quaternion.identity,this.transform);
        Platforms.Add(m);
        //m.AddComponent<MovingPlatform>().Init(m.transform.position,p2.transform.position+new Vector3(-deltaX,platformSize.y,0),Random.Range(1.5f,2.5f));
        m.AddComponent<MovingPlatform>().Init(m.transform.position,new Vector3(end.x,p2.transform.position.y+platformSize.y,end.z),Random.Range(1.25f,2.25f));
        MovingPlatforms.Add(m.GetComponent<MovingPlatform>());
        m.name = "Moving Platform";
        //p1.AddComponent<MovingPlatform>().Init(p1.transform.position,p2.transform.position-new Vector3(deltaX,platformSize.y,0),Random.Range(1.0f,2.0f),true);
    }

    GameObject BuildRamp(Vector3 start, Vector3 end) {
        Vector3 midpoint = (start+end)/2;
        GameObject ramp = GameObject.Instantiate(Platform,midpoint,Quaternion.identity,this.transform);
        Platforms.Add(ramp);
        ramp.transform.LookAt(end);
        ramp.transform.Rotate(0,90,0);
        ramp.transform.localScale = new Vector3(Vector3.Distance(start,end),2.0f,2);
        ramp.name = "ramp";
        ramp.GetComponent<Renderer>().materials[0].mainTextureScale = new Vector2(ramp.transform.localScale.x,ramp.transform.localScale.z);
        return ramp;
    }

    protected override void CheckSolveConditions() {
        Bounds goalBounds = goal.GetComponent<Collider>().bounds;
            foreach(Collider o in Physics.OverlapBox(goalBounds.center,goalBounds.size/2))
            {
                if(o.gameObject == Player) {
                    solved = true;
                    PlaySolvedSound();
                }
            }
    }


    void AddItem(Transform platform, GameObject itemRef) {
        GameObject c = GameObject.Instantiate(itemRef);
        c.transform.position = platform.position + new Vector3(0,platformSize.y,0)*2;
        c.name = "Item";
        c.transform.parent = platform;
        c.transform.rotation = platform.rotation;
        c.GetComponent<Rigidbody>().isKinematic = true;
        c.GetComponent<Rigidbody>().useGravity = false;
    }
}

