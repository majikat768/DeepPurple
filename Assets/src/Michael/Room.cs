using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Room : MonoBehaviour
{
    private Bounds RoomBounds;
    public List<GameObject> DoorList;
    protected GameObject Wall = RoomGenerator.Wall;
    protected GameObject Door = RoomGenerator.Door;
    protected GameObject Block = RoomGenerator.Block;
    protected GameObject Player;
    public Vector3 Zero;
    public Vector3 size;

    public GameObject Walls;
    public GameObject Floor;
    public GameObject Ceiling;

    public void Awake()
    {
        DoorList = new List<GameObject>();
        Player = GameObject.FindWithTag("Player");
        Walls = new GameObject("Walls");
        Walls.transform.parent = this.gameObject.transform;
    }
    public void Start()
    {
    }


    public void Init() {
        this.RoomBounds = new Bounds(new Vector3(Zero.x + size.x / 2, Zero.y + size.y / 2, Zero.z + size.z / 2), size);

        Floor = GameObject.Instantiate(
            Resources.Load<GameObject>("Michael/Plane"),
            new Vector3(size.x / 2 + Zero.x, 0, size.z / 2 + Zero.z), 
            Quaternion.identity,
            this.transform);
        Floor.name = "Floor";
        Floor.transform.localScale = new Vector3(size.x / 10.0f, 1, size.z / 10.0f);
        Floor.GetComponent<Renderer>().material.mainTextureScale = new Vector2(size.x/2, size.z/2);

        Ceiling = GameObject.Instantiate(
            Resources.Load<GameObject>("Michael/Plane"),
            new Vector3(size.x / 2 + Zero.x, size.y, size.z / 2 + Zero.z), 
            Quaternion.Euler(180.0f, 0, 0), 
            this.transform);
        Ceiling.name = "Ceiling";
        Light light = Ceiling.AddComponent<Light>();
        light.range = Mathf.Max(size.x, size.z);
        light.intensity = 0.0f;
        light.shadows = LightShadows.Hard;
        Ceiling.transform.localScale = new Vector3(size.x / 10.0f, 1, size.z / 10.0f);

        // Box Collider tells me when player enters or exits a room;
        // also gets bordering rooms
        gameObject.AddComponent<BoxCollider>().size = size;
        this.GetComponent<BoxCollider>().center = Zero+size/2;
        this.GetComponent<BoxCollider>().isTrigger = true;
        this.gameObject.layer = 2;

    }


    public void SetLighting(Color c, float intensity = 0)
    {
        Ceiling.GetComponent<Light>().color = c; 
        Ceiling.GetComponent<Light>().intensity = intensity;
    }

    public bool InRoom(GameObject Player)
    {
        if (!Player) return false;

        if (RoomBounds.Contains(Player.transform.position))
            return true;
        return false;
    }

    private void OnTriggerEnter(Collider player) {
        if (player.gameObject == Player)
        {
            this.SetLighting(Color.white, 1);
        }
    }
    private void OnTriggerExit(Collider player) {
        if (player.gameObject == Player)
        {
            this.SetLighting(Color.white, 0.5f);
        }
    }

    public void GetWalls()
    {
        BuildWall(Zero + new Vector3(0, size.y / 2, 0), Zero + new Vector3(size.x, size.y / 2, 0));
        BuildWall(Zero + new Vector3(0, size.y / 2, 0), Zero + new Vector3(0, size.y / 2, size.z));
        BuildWall(Zero + new Vector3(size.x, size.y / 2, 0), Zero + new Vector3(size.x, size.y / 2, size.z));
        BuildWall(Zero + new Vector3(0, size.y / 2, size.z), Zero + new Vector3(size.x, size.y / 2, size.z));

        Transform start = Walls.transform.GetChild(Random.Range(0, Walls.transform.childCount - 1));
        Vector3 dir = start.TransformDirection(Vector3.forward)*((start.position.x == Zero.x || start.position.z == Zero.z+size.z? 1 : -1));
        RaycastHit hit;
        Debug.DrawRay(start.position, dir,Color.white,10);
        if (Physics.Raycast(start.position, dir, out hit, Mathf.Infinity, RoomGenerator.WallMask)) 
            GetInnerWalls(start.position, hit.point, 0);
            
    }

    public void BuildWall(Vector3 start, Vector3 end)
    {
        //start at wall's starting location; 
        //increase length of wall segment(segmentEnd - segmentStart) until hits a door, or hits wallEnd
        //build wall there, start again.

        Vector3 SegmentStart = start;
        Vector3 SegmentEnd = start;
        GameObject w;
        float t = 0.0f;
        while(Vector3.Distance(SegmentEnd,end) > 1)
        {
            SegmentEnd = Vector3.Lerp(SegmentEnd, end, t);
            foreach (Collider o in Physics.OverlapBox((SegmentEnd + SegmentStart) / 2, (SegmentEnd - SegmentStart) / 3))
            {
                if(o.name == "Door" && this.DoorList.Contains(o.gameObject))
                {
                    SegmentEnd = o.ClosestPoint(start);
                    w = GameObject.Instantiate(Wall, (SegmentStart + SegmentEnd) / 2,Quaternion.Euler(0,0,0),Walls.transform);
                    w.transform.LookAt(end);
                    w.transform.Rotate(0, 90, 0);
                    w.transform.localScale = new Vector3(Vector3.Distance(SegmentStart,SegmentEnd), size.y, 0.2f);
                    w.GetComponent<Renderer>().material.mainTextureScale = new Vector2(w.transform.localScale.x/4, w.transform.localScale.y/4);
                    w.name = "Wall";
                    w.gameObject.SetActive(false);
                    w.gameObject.SetActive(true);
                    SegmentStart = o.ClosestPoint(end);
                    SegmentEnd = SegmentStart;
                    o.name = "nDoor";
                    break;

                }
            }
            t+=0.1f;
        }
        SegmentEnd = end;
        w = GameObject.Instantiate(Wall, (SegmentStart + SegmentEnd) / 2, Quaternion.Euler(0,0,0),Walls.transform);
        w.name = "Wall";
        w.transform.LookAt(end);
        w.transform.Rotate(0, 90, 0);
        w.transform.localScale = new Vector3(Vector3.Distance(SegmentStart,SegmentEnd), size.y, 0.2f);
        w.GetComponent<Renderer>().material.mainTextureScale = new Vector2(w.transform.localScale.x/4, w.transform.localScale.y/4);
        w.gameObject.SetActive(false);
        w.gameObject.SetActive(true);

    }

    public void GetInnerWalls(Vector3 start, Vector3 end, int depth)
    {
        if (depth > 3) return;
        if (Vector3.Distance(start, end) < 4)   return;
        RaycastHit hit;
        Vector3 dir;
        BuildWall(start, Vector3.Lerp(start,end,0.3f));
        BuildWall(end, Vector3.Lerp(end,start, 0.3f));

        Vector3 newStart = Vector3.Lerp(start, end, Random.Range(0.3f, 0.7f));
        Vector3 newEnd = newStart;
        dir = Vector3.Cross(start, end);
        dir = new Vector3(dir.x, 0, dir.z);
        //Debug.DrawRay(newStart, dir, Color.red, 10);
        if (Physics.Raycast(newStart, dir, out hit, Mathf.Infinity, RoomGenerator.WallMask))
            GetInnerWalls(newStart,hit.point, depth + 1);



    }
    public void SetSize(Vector3 size) {
        this.size = size;
    }

    public Vector2 GetSize() { return size; }

    public void SetZero(Vector3 Zero) {
        this.Zero = Zero;
    }

}
