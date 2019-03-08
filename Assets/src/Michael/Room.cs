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
    public Vector3 size = new Vector3(16.0f, 4.0f, 16.0f);

    public GameObject Walls;
    public GameObject Floor;
    public GameObject Ceiling;
    //public GameObject NorthWall, SouthWall,EastWall, WestWall;

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
        Ceiling.transform.localScale = new Vector3(size.x / 10.0f, 1, size.z / 10.0f);

        // Box Collider tells me when player enters or exits a room.
        gameObject.AddComponent<BoxCollider>().size = size;
        this.GetComponent<BoxCollider>().center = Zero+size/2;
        this.GetComponent<BoxCollider>().isTrigger = true;

        Debug.Log(size);

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
        this.SetLighting(Color.white,2);
    }
    private void OnTriggerExit(Collider player) {
        this.SetLighting(Color.white,0);
    }

    /*
    public List<float> Doorway(GameObject wall) {
        List<float> xs = new List<float>();
        foreach(Transform w in wall.transform) {
            if(w.name == "Door") {
                if(wall.name == "NorthWall" || wall.name == "SouthWall") 
                    xs.Add(w.position.x);
                else 
                    xs.Add(w.position.z);
            }
        }
        return xs;
    } */

    public void BuildWalls()
    {
        //For each wall segment,
        //place at location, translated inward so Walls aren't
        //occupying same location,
        //then scale in y direction by room size y value.
        //each wall is 1 unit wide, and 0.1 unit thick.
        
        for(int i = 0; i < size.x; i++)
        {
            GameObject n, s;
            bool doorHere = false;
            foreach(Collider o in Physics.OverlapBox(new Vector3(Zero.x+i+0.5f,size.y/2,Zero.z+size.z-Wall.transform.localScale.z/2),new Vector3(Wall.transform.localScale.x/4,size.y/2,Wall.transform.localScale.z/2))) {
                if(o.name == "Door")    doorHere = true;
            }
            if(!doorHere) {
                n = GameObject.Instantiate(
                        Wall, 
                        new Vector3(
                            Zero.x + i + 0.5f, 
                            size.y / 2, 
                            Zero.z + size.z - Wall.transform.localScale.z/2),
                        Quaternion.Euler(0, 180.0f, 0), 
                        Walls.transform);
                n.transform.localScale = new Vector3(n.transform.localScale.x, size.y, n.transform.localScale.z);
                n.name = "NorthWall";
            }

            doorHere = false;
            foreach(Collider o in Physics.OverlapBox(new Vector3(Zero.x+i+0.5f,size.y/2,Zero.z-Wall.transform.localScale.z/2),new Vector3(Wall.transform.localScale.x/4,size.y/2,Wall.transform.localScale.z/2))) {
                if(o.name == "Door")    doorHere = true;
            }
            //South
            if(!doorHere) {
            s = GameObject.Instantiate(
                    Wall, new Vector3(
                        Zero.x + i + 0.5f, 
                        size.y / 2, 
                        Zero.z + Wall.transform.localScale.z/2), 
                    Quaternion.identity, 
                    Walls.transform);
            s.transform.localScale = new Vector3(s.transform.localScale.x, size.y, s.transform.localScale.z);
            s.name = "SouthWall";

            }
        }
        for(int i = 0; i < size.z; i++)
        {
            GameObject e, w;
            bool doorHere = false;
            foreach(Collider o in Physics.OverlapBox(new Vector3(Zero.x+size.x-Wall.transform.localScale.z/2,size.y/2,Zero.z+i+0.5f),new Vector3(Wall.transform.localScale.x/4,size.y/2,Wall.transform.localScale.z/4))) {
                if(o.name == "Door")    doorHere = true;
            }
            if(!doorHere) {
                e = GameObject.Instantiate(
                        Wall, 
                        new Vector3(
                            Zero.x + size.x - Wall.transform.localScale.z/2, 
                            size.y / 2, 
                            Zero.z + i + 0.5f), 
                        Quaternion.Euler(0, 90.0f, 0), 
                        Walls.transform);
                e.transform.localScale = new Vector3(e.transform.localScale.x, size.y, e.transform.localScale.z);
                e.name = "EastWall";

            }
            //West
            doorHere = false;
            foreach(Collider o in Physics.OverlapBox(new Vector3(Zero.x-Wall.transform.localScale.z/2,size.y/2,Zero.z+i+0.5f),new Vector3(Wall.transform.localScale.x/4,size.y/2,Wall.transform.localScale.z/4))) {
                if(o.name == "Door")    doorHere = true;
            }
            if(!doorHere) {
                w = GameObject.Instantiate(
                        Wall, 
                        new Vector3(
                            Zero.x + Wall.transform.localScale.z/2, 
                            size.y / 2, 
                            Zero.z + i + 0.5f), 
                        Quaternion.Euler(0, -90.0f, 0), 
                        Walls.transform);
                w.transform.localScale = new Vector3(w.transform.localScale.x, size.y, w.transform.localScale.z);
                w.name = "WestWall";
            }
        }
        int rand = Random.Range(2, Walls.transform.childCount - 3);
        Transform start = Walls.transform.GetChild(rand);
        while(start.name == "Door")
            start = Walls.transform.GetChild(rand);
        GetInnerWalls(start,0);
    }
    public void GetInnerWalls(Transform start, int depth)
    {
        if (depth > 1) return;
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
            case -180:
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
            if (i < distance*0.35f || i > distance * 0.65f)
            {
                float newX = start.position.x + Mathf.Cos(direction.y * Mathf.Deg2Rad) * i + Mathf.Sin(direction.y * Mathf.Deg2Rad) * 0.5f;
                float newZ = start.position.z + Mathf.Sin(direction.y * Mathf.Deg2Rad) * i + Mathf.Cos(direction.y * Mathf.Deg2Rad) * 0.5f;
                Vector3 location = new Vector3(newX, size.y / 4, newZ);

                GameObject w = GameObject.Instantiate(Wall, location, Quaternion.Euler(direction), Walls.transform);
                temp.Add(w);

                w.transform.localScale = new Vector3(w.transform.localScale.x, size.y / 2, w.transform.localScale.z);
            }
        }

        GetInnerWalls(temp[(int)Random.Range(temp.Count*0.35f, temp.Count*0.65f)].transform,depth+1);

    }
    public void SetSize(Vector3 size) {
        this.size = size;
    }

    public Vector2 GetSize() { return size; }

    public void SetZero(Vector3 Zero) {
        this.Zero = Zero;
    }

}
