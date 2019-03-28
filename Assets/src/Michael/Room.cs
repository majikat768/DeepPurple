using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Room : MonoBehaviour
{
    public bool initialized = false;
    public bool testbuild = false;
    private Bounds RoomBounds;
    public int complexity = 3;
    public List<GameObject> DoorList;
    public List<GameObject> FloorTiles;
    protected GameObject Wall;
    protected GameObject Door;
    protected GameObject Block; 
    protected GameObject Console;
    protected GameObject Panel;
    protected static GameObject FloorTile;
    protected GameObject WallLight; 
    protected GameObject Player;
    [SerializeField]
    protected Vector3 Zero;
    [SerializeField]
    protected Vector3 size;

    public GameObject Walls;
    public GameObject Floor;
    public GameObject Ceiling;

    public void Awake()
    {
        this.gameObject.transform.position = Zero;
        FloorTile = RoomGenerator.FloorTile;
        Wall = RoomGenerator.Wall;
        Door = RoomGenerator.Door;
        Block = RoomGenerator.Block;
        WallLight = RoomGenerator.WallLight;
        Console = RoomGenerator.Console;
        Panel = RoomGenerator.Panel;
        DoorList = new List<GameObject>();
        FloorTiles = new List<GameObject>();
        Player = GameObject.FindWithTag("Player");
        Walls = new GameObject("Walls");
        Walls.transform.parent = this.gameObject.transform;
        RoomGenerator.RoomList.Add(this);

        if(testbuild) {
            RoomGenerator.BuildDoors();
            RoomGenerator.BakeNavMesh();
        }
    }

    public void Start()
    {
    }


    public void Init() {
        initialized = true;
        this.RoomBounds = new Bounds(new Vector3(size.x / 2, size.y / 2, size.z / 2), size);

        /*
         * * * * if tiling instances of a prefab, use this:
        Floor = new GameObject("Floor");
        Floor.transform.parent = this.transform;
        for(float i = 0.5f; i < size.x; i++)
        {
            for(float j = 0.5f; j < size.z; j++)
            {
                GameObject f = GameObject.Instantiate(FloorTile, Zero + new Vector3(i, 0, j), FloorTile.transform.rotation, Floor.transform);
                Vector3 FloorSize = f.GetComponent<Renderer>().bounds.size;
                f.transform.localScale = new Vector3(1.0f / FloorSize.x, 1.0f, 1.0f / FloorSize.z);
                f.name = "Floor";
                FloorTiles.Add(f);
            }
        }
        /*
        */

        /*
         * * * * if using a tileable texture, use this:
         */
        Floor = GameObject.Instantiate(
            FloorTile,
            Zero+new Vector3(size.x/2,0,size.z/2),
            this.gameObject.transform.rotation,
            this.transform);
        Vector3 FloorSize = Floor.GetComponent<Renderer>().bounds.size;
        Floor.name = "Floor";
        Floor.transform.localScale = new Vector3(size.x / FloorSize.x, 1, size.z / FloorSize.x);
        //Floor.GetComponent<Renderer>().material.mainTextureScale = new Vector2(size.x/2, size.z/2);
        /*
        */

        Ceiling = GameObject.Instantiate(
            Resources.Load<GameObject>("Michael/Plane"),
            Zero+new Vector3(size.x / 2, size.y, size.z / 2), 
            Quaternion.Euler(180.0f, 0, 0), 
            this.transform);
        Ceiling.name = "Ceiling";
        /*
        Light light = Ceiling.AddComponent<Light>();
        light.range = Mathf.Max(size.x, size.z);
        light.intensity = 0.0f;
        light.shadows = LightShadows.Hard;
        */
        Ceiling.transform.localScale = new Vector3(size.x / 10.0f, 1, size.z / 10.0f);

        // Box Collider tells me when player enters or exits a room;
        // also gets bordering rooms
        gameObject.AddComponent<BoxCollider>().size = size;
        this.GetComponent<BoxCollider>().center = size/2;
        this.GetComponent<BoxCollider>().isTrigger = true;
        this.gameObject.layer = 2;

    }


    public void SetLighting(Color c, float intensity = 0)
    {
        foreach(Transform w in this.Walls.transform) {
            Transform l = w.transform.Find("Light");
            l.gameObject.GetComponent<Light>().color = c;
            l.gameObject.GetComponent<Light>().intensity = intensity;
            l.GetComponent<Renderer>().material.SetColor("_EmissionColor",c*intensity);
        }
        //Ceiling.GetComponent<Light>().color = c; 
        //Ceiling.GetComponent<Light>().intensity = intensity;
    }

    public bool InRoom(GameObject Player)
    {
        if (!Player) return false;

        if (RoomBounds.Contains(Player.transform.position))
            return true;
        return false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == Player)
        {
            this.SetLighting(RoomGenerator.Cyan, 1);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == Player)
        {
            this.SetLighting(RoomGenerator.Cyan, 0.0f);
        }
    }

    public void GetWalls()
    {
        BuildWall(Zero, Zero+new Vector3(size.x, 0, 0), size.y);
        BuildWall(Zero, Zero+new Vector3(0, 0, size.z), size.y);
        BuildWall(Zero+new Vector3(size.x, 0, 0), Zero+new Vector3(size.x, 0, size.z), size.y);
        BuildWall(Zero+new Vector3(0, 0, size.z), Zero+new Vector3(size.x, 0, size.z), size.y);

        Transform start = Walls.transform.GetChild(Random.Range(0, Walls.transform.childCount - 1));
        Vector3 dir = start.TransformDirection(Vector3.forward)*((start.position.x == Zero.x || start.position.z == Zero.z+size.z ? 1 : -1));
        Vector3 startpoint = start.position + 
            (start.position.x > start.position.z ? 
             new Vector3(Random.Range(-start.GetComponent<Renderer>().bounds.size.x/3,start.GetComponent<Renderer>().bounds.size.x/3),0,0) : 
             new Vector3(0,0,Random.Range(-start.GetComponent<Renderer>().bounds.size.z/3,start.GetComponent<Renderer>().bounds.size.z/3)));

        RaycastHit hit;
        //Debug.DrawRay(start.position+new Vector3(0,size.y/2,0), dir*64,Color.red,10);
        if (Physics.Raycast(startpoint + new Vector3(0, size.y / 2, 0), dir, out hit, Mathf.Infinity, RoomGenerator.WallMask))
        {
            GetInnerWalls(startpoint, hit, 0);
        }
            
    }

    public void BuildWall(Vector3 start, Vector3 end,float height,bool doors = true)
    {
        Vector3 wBounds;
        //start at wall's starting location; 
        //increase length of wall segment(segmentEnd -> segmentStart) until hits a door, or hits wallEnd
        //build wall there, start again.

        Vector3 SegmentStart = start;
        Vector3 SegmentEnd = start;
        Vector3 dir;
        GameObject w,l,c,p;
        Vector3 lastDoor = new Vector3();
        float t = 0.0f;
        if (doors)
        {
            while (Vector3.Distance(SegmentEnd, end) > 0.1f)
            {
                SegmentEnd = Vector3.Lerp(SegmentEnd, end, t);
                foreach (Collider o in Physics.OverlapBox(new Vector3((SegmentEnd.x + SegmentStart.x) / 2, Zero.y+size.y / 2, (SegmentEnd.z + SegmentStart.z) / 2), new Vector3(Mathf.Abs(SegmentEnd.x - SegmentStart.x), size.y*0.75f, Mathf.Abs(SegmentEnd.z - SegmentStart.z))/2))
                {
                    if (o.name == "Door" && lastDoor != o.transform.position)
                    {
                        SegmentEnd = o.bounds.ClosestPoint(start);
                        SegmentEnd = new Vector3(SegmentEnd.x, 0, SegmentEnd.z);
                        w = GameObject.Instantiate(Wall, (SegmentStart + SegmentEnd) / 2, this.gameObject.transform.rotation, Walls.transform);
                        wBounds = w.GetComponent<Renderer>().bounds.size;
                        w.transform.LookAt(end);
                        w.transform.Rotate(0, 90, 0);
                        w.transform.localScale = new Vector3(Vector3.Distance(SegmentStart, SegmentEnd) / wBounds.x, height / wBounds.y, 1);
                        w.name = "Wall";
                        w.GetComponent<NavMeshObstacle>().size = wBounds;
                        w.gameObject.SetActive(false);
                        w.gameObject.SetActive(true);

                        c = GameObject.Instantiate(Console,Vector3.MoveTowards(SegmentEnd,SegmentStart,Console.GetComponent<Renderer>().bounds.size.magnitude/2),Quaternion.identity,this.transform);
                        c.transform.LookAt(end);
                        c.transform.Rotate(0,90*(c.transform.position.x == Zero.x || c.transform.position.z == Zero.z+size.z ? 1 : -1),0);
                        dir = (w.transform.TransformDirection(Vector3.forward)*((w.transform.position.x == Zero.x || w.transform.position.z == Zero.z+size.z ? 1 : -1))).normalized;
                        c.transform.position += dir*Console.GetComponent<Renderer>().bounds.size.z/2;
                        l = GameObject.Instantiate(WallLight,w.transform.position + new Vector3(0,height*0.75f,0),w.transform.rotation,w.transform);
                        l.transform.position += dir*Mathf.Min(w.GetComponent<Renderer>().bounds.size.z,w.GetComponent<Renderer>().bounds.size.x)/2;
                        //l.GetComponent<Light>().lightmapBakeType = LightmapBakeType.Baked;
                        l.name = "Light";
                        

                        SegmentStart = o.GetComponent<Renderer>().bounds.ClosestPoint(end);
                        SegmentStart = new Vector3(SegmentStart.x, 0, SegmentStart.z);
                        SegmentEnd = SegmentStart;
                        p = GameObject.Instantiate(Panel,Vector3.MoveTowards(SegmentEnd,end,Panel.GetComponent<Renderer>().bounds.size.magnitude/2),Quaternion.identity,this.transform);
                        p.transform.LookAt(end);
                        p.transform.Rotate(0,90*(p.transform.position.x == Zero.x || p.transform.position.z == Zero.z+size.z ? 1 : -1),0);
                        p.transform.position += dir*Panel.GetComponent<Renderer>().bounds.size.z;
                        p.transform.position += new Vector3(0,size.y/2,0);
                        //o.name = "nDoor";
                        t = 0.0f;
                        lastDoor = o.transform.position;
                        break;

                    }
                }
                t += 0.1f;
            }
        }
        SegmentEnd = end;
        w = GameObject.Instantiate(Wall, (SegmentStart + SegmentEnd) / 2, Quaternion.identity,Walls.transform);
        wBounds = w.GetComponent<Renderer>().bounds.size;
        w.name = "Wall";
        w.transform.LookAt(end);
        w.transform.Rotate(0, 90, 0);
        w.transform.localScale = new Vector3(Vector3.Distance(SegmentStart,SegmentEnd)/wBounds.x, height/wBounds.y, 1);
        w.GetComponent<NavMeshObstacle>().size = wBounds;

        l = GameObject.Instantiate(WallLight,w.transform.position + new Vector3(0,height*0.75f,0),w.transform.rotation,w.transform);
        dir = (w.transform.TransformDirection(Vector3.forward)*((w.transform.position.x == Zero.x || w.transform.position.z == Zero.z+size.z ? 1 : -1))).normalized;
        l.transform.position += dir*Mathf.Min(w.GetComponent<Renderer>().bounds.size.z,w.GetComponent<Renderer>().bounds.size.x)/2;
        //l.GetComponent<Light>().lightmapBakeType = LightmapBakeType.Baked;
        l.name = "Light";

        w.gameObject.SetActive(false);
        w.gameObject.SetActive(true);

    }

    public void GetInnerWalls(Vector3 start, RaycastHit endHit, int depth)
    {
        bool built = false;
        if (depth > complexity) return;
        Vector3 end = endHit.point - new Vector3(0, size.y / 2, 0);
        if (Vector3.Distance(start, end) < 4)   return;
        RaycastHit hit,hit2;
        Vector3 dir;
        foreach(Collider o in Physics.OverlapBox(endHit.point, new Vector3(2,2,2))) {
            if(o.transform.name == "Door") {
                BuildWall(start,Vector3.Lerp(start,end,0.7f),size.y/2,false);
                built = true;
            }
        }

        if(!built) {
            BuildWall(start, Vector3.Lerp(start, end, 0.3f), size.y / 2,false);
            BuildWall(end, Vector3.Lerp(end, start, 0.3f), size.y / 2,false);
        }

        /*
        if (endHit.transform.gameObject.name == "Door")
            BuildWall(start, Vector3.Lerp(start, end, 0.7f), size.y / 2, false);
        else
        {
            BuildWall(start, Vector3.Lerp(start, end, 0.3f), size.y / 2,false);
            BuildWall(end, Vector3.Lerp(end, start, 0.3f), size.y / 2,false);
        }
        */

        Vector3 newStart = Vector3.Lerp(start, end, Random.Range(0.2f, 0.8f));
        dir = Vector3.Cross(start+new Vector3(0,1,0), end+new Vector3(0,1,0));
        dir = new Vector3(dir.x, 0, dir.z).normalized;
        //Debug.DrawRay(newStart+new Vector3(0,size.y/2,0), dir, Color.green, 10);
        //Debug.DrawRay(newStart+new Vector3(0,size.y/2,0), -dir, Color.blue, 10);
        if (Physics.Raycast(newStart+new Vector3(0,size.y/2,0), dir, out hit, Mathf.Infinity, RoomGenerator.WallMask))
        {
            if (Physics.Raycast(newStart+new Vector3(0,size.y/2,0), -dir, out hit2, Mathf.Infinity, RoomGenerator.WallMask))
            {
                if (Vector3.Distance(newStart, hit2.point) >= Vector3.Distance(newStart, hit.point))
                    GetInnerWalls(newStart, hit2, depth + 1);
                else if(Vector3.Distance(newStart,hit.point) >= Vector3.Distance(newStart,hit2.point))
                    GetInnerWalls(newStart, hit, depth + 1);
            }
            else 
                GetInnerWalls(newStart, hit, depth + 1);
        }
        else if (Physics.Raycast(newStart+new Vector3(0,size.y/2,0), -dir, out hit2, Mathf.Infinity, RoomGenerator.WallMask))
            GetInnerWalls(newStart, hit2, depth + 1);



    }
    public void SetSize(Vector3 size) {
        this.size = size;
    }

    public Vector3 GetSize() { return size; }

    public void SetZero(Vector3 Zero) {
        this.Zero = Zero;
        this.transform.position = Zero;
    }
    public Vector3 GetZero() { return Zero; }

}
