using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Room : MonoBehaviour
{
    private Bounds RoomBounds;
    public List<GameObject> DoorList;
    public List<GameObject> WallList; 
    protected GameObject Wall = RoomGenerator.Wall;
    protected GameObject Door = RoomGenerator.Door;
    protected GameObject Block = RoomGenerator.Block;
    protected GameObject Player;
    [SerializeField]    protected Vector3 Zero;
    [SerializeField]    protected Vector3 size = new Vector3(16.0f, 4.0f, 16.0f);

    GameObject walls;
    GameObject Floor;
    GameObject Ceiling;

    public void Awake()
    {
        DoorList = new List<GameObject>();
        WallList = new List<GameObject>();
        Player = GameObject.FindWithTag("Player");
        walls = new GameObject("Walls");
        walls.transform.parent = this.gameObject.transform;
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

        Ceiling = GameObject.Instantiate(
            Resources.Load<GameObject>("Michael/Plane"),
            new Vector3(size.x / 2 + Zero.x, size.y, size.z / 2 + Zero.z), 
            Quaternion.Euler(180.0f, 0, 0), 
            this.transform);
        Ceiling.name = "Ceiling";
        //c.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.0f, 1.0f, 1.0f, 1.0f));
        Light light = Ceiling.AddComponent<Light>();
        light.range = 20;
        light.intensity = 1;
        light.lightmapBakeType = LightmapBakeType.Baked;
        Ceiling.transform.localScale = new Vector3(size.x / 10.0f, 1, size.z / 10.0f);
        GetWalls();

        int rand = Random.Range(0, walls.transform.childCount - 1);
        Transform start = walls.transform.GetChild(rand);
        while (start.name == "Door")
            start = walls.transform.GetChild((int)Random.Range(0, walls.transform.childCount - 1));
        GetInnerWalls(start,0);

    }


    public void SetLighting(Color c)
    {
        Light l = Ceiling.GetComponent<Light>();
        l.color = c; 
    }

    public bool InRoom(GameObject Player)
    {
        if (!Player) return false;
        if (RoomBounds.Contains(
            Player.transform.position))
            return true;
        return false;
    }
    private GameObject GetWalls()
    {
        WallList = new List<GameObject>();
        DoorList = new List<GameObject>();
        GameObject n, s, e, w;
        //build walls 
        //North:
        for(int i = 0; i < size.x; i++)
        {
            if (i >= size.x / 2 - 1 && i <= size.x / 2 + 1)
            {
                n = GameObject.Instantiate(Door, new Vector3(Zero.x + i + 0.5f, size.y / 2, Zero.z + size.z), Quaternion.Euler(0, 180.0f, 0), walls.transform);
                n.transform.localScale = new Vector3(n.transform.localScale.x, size.y, n.transform.localScale.z);
                s = GameObject.Instantiate(Door, new Vector3(Zero.x + i + 0.5f, size.y / 2, Zero.z), Quaternion.Euler(0, 180.0f, 0), walls.transform);
                s.transform.localScale = new Vector3(s.transform.localScale.x, size.y, s.transform.localScale.z);
                s.name = n.name = "Door";
                DoorList.Add(s);
                DoorList.Add(n);
            }
            else
            {
                n = GameObject.Instantiate(Wall, new Vector3(Zero.x + i + 0.5f, size.y / 2, Zero.z + size.z), Quaternion.Euler(0, 180.0f, 0), walls.transform);
                n.transform.localScale = new Vector3(n.transform.localScale.x, size.y, n.transform.localScale.z);
                n.name = "NorthWall";
                //South
                s = GameObject.Instantiate(Wall, new Vector3(Zero.x + i + 0.5f, size.y / 2, Zero.z), Quaternion.identity, walls.transform);
                s.transform.localScale = new Vector3(s.transform.localScale.x, size.y, s.transform.localScale.z);
                s.name = "SouthWall";
            }

                WallList.Add(n);
                WallList.Add(s);
        }
        //East
        for(int i = 0; i < size.z; i++)
        {
            if (i >= size.z / 2 - 1 && i <= size.z / 2 + 1)
            {
                w = GameObject.Instantiate(Door, new Vector3(Zero.x, size.y / 2, Zero.z + i + 0.5f), Quaternion.Euler(0, 90.0f, 0), walls.transform);
                w.transform.localScale = new Vector3(w.transform.localScale.x, size.y, w.transform.localScale.z);
                e = GameObject.Instantiate(Door, new Vector3(Zero.x + size.x, size.y / 2, Zero.z + i + 0.5f), Quaternion.Euler(0, 90.0f, 0), walls.transform);
                e.transform.localScale = new Vector3(e.transform.localScale.x, size.y, e.transform.localScale.z);
                w.name = e.name = "Door";
                DoorList.Add(e);
                DoorList.Add(w);
            }
            else
            {
                e = GameObject.Instantiate(Wall, new Vector3(Zero.x + size.x, size.y / 2, Zero.z + i + 0.5f), Quaternion.Euler(0, 90.0f, 0), walls.transform);
                e.transform.localScale = new Vector3(e.transform.localScale.x, size.y, e.transform.localScale.z);
                e.name = "EastWall";
                //West
                w = GameObject.Instantiate(Wall, new Vector3(Zero.x, size.y / 2, Zero.z + i + 0.5f), Quaternion.Euler(0, -90.0f, 0), walls.transform);
                w.transform.localScale = new Vector3(w.transform.localScale.x, size.y, w.transform.localScale.z);
                w.name = "WestWall";
            }
                WallList.Add(e);
                WallList.Add(w);
        }
        return walls;
    }
    public void GetInnerWalls(Transform start, int depth)
    {
        if (depth > 2) return;
        List<GameObject> temp = new List<GameObject>();
        Vector3 direction = start.rotation.eulerAngles + new Vector3(0,90.0f,0);
        Vector3 end = new Vector3();
        switch((int)direction.y%360)
        {
            case 0:
                //travelling east; 
                end = new Vector3(Zero.x + size.x+0.5f, 0.0f, start.position.z);
                break;
            case 90:
                end = new Vector3(start.position.x, 0.0f, Zero.z + size.z+0.5f);
                break;
            case 180:
                end = new Vector3(Zero.x, 0.0f, start.position.z);
                break;
            case -90:
            case 270:
                end = new Vector3(start.position.x, 0.0f, Zero.z);
                break;
            default:
                break;
        }

        float distance = Vector3.Distance(new Vector3(start.position.x, 0.0f, start.position.z), new Vector3(end.x, 0.0f, end.z));
        if (distance < 4) return;
        for (float i = 0.5f; i < distance; i += 1)
        {
            if (i < distance * 0.35f || i > distance * 0.65f)
            {
                float newX = start.position.x + Mathf.Cos(direction.y * Mathf.Deg2Rad) * i + Mathf.Sin(direction.y * Mathf.Deg2Rad) * 0.5f;
                float newZ = start.position.z + Mathf.Sin(direction.y * Mathf.Deg2Rad) * i + Mathf.Cos(direction.y * Mathf.Deg2Rad) * 0.5f;
                Vector3 location = new Vector3(newX, size.y / 4, newZ);

                GameObject w = GameObject.Instantiate(Wall, location, Quaternion.Euler(direction), walls.transform);
                temp.Add(w);

                w.transform.localScale = new Vector3(1.0f, size.y / 2, 0.1f);
            }
        }

        GetInnerWalls(temp[(int)Random.Range(temp.Count*0.4f, temp.Count*0.6f)].transform,depth+1);

    }
    public void SetSize(float x, float y, float z) {
        this.size = new Vector3(x,y,z);
    }

    public Vector2 GetSize() { return size; }

    public void SetZero(Vector3 Zero) {
        this.Zero = Zero;
        this.transform.position = Zero;
    }
}
