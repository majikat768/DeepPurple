using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Room 
{
    public GameObject room;
    private Vector3 Zero;
    protected GameObject Wall = Resources.Load<GameObject>("Michael/Wall");
    protected GameObject Block = Resources.Load<GameObject>("Michael/Block");
    protected Vector3 size;

    GameObject walls;
    GameObject NorthWall;
    GameObject SouthWall;
    GameObject EastWall;
    GameObject WestWall;
    GameObject InnerWalls;

    public GameObject Floor;

    public Room(Vector3 Zero,GameObject r)
    {
        room = r;
        this.size = r.GetComponent<RoomGenerator>().GetSize();
        walls = new GameObject("Walls");
        walls.transform.parent = room.transform;
        NorthWall = new GameObject("NorthWall");
        NorthWall.transform.parent = walls.transform;
        SouthWall = new GameObject("SouthWall");
        SouthWall.transform.parent = walls.transform;
        EastWall = new GameObject("EastWall");
        EastWall.transform.parent = walls.transform;
        WestWall = new GameObject("WestWall");
        WestWall.transform.parent = walls.transform;
        InnerWalls = new GameObject("InnerWalls");
        InnerWalls.transform.parent = walls.transform;
        this.Zero = Zero;

        this.Generate();
    }
    public void Generate() {
        Floor = GameObject.Instantiate(
            Resources.Load<GameObject>("Michael/Plane"),
            new Vector3(size.x / 2 + Zero.x, 0, size.z / 2 + Zero.z), 
            Quaternion.identity,
            room.transform);
        Floor.name = "Floor";
        Floor.transform.localScale = new Vector3(size.x / 10.0f, 1, size.z / 10.0f);

        GameObject c = GameObject.Instantiate(
            Resources.Load<GameObject>("Michael/Plane"),
            new Vector3(size.x / 2 + Zero.x, size.y, size.z / 2 + Zero.z), 
            Quaternion.Euler(180.0f, 0, 0), 
            room.transform);
        c.name = "Ceiling";
        c.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.0f, 1.0f, 1.0f, 1.0f));
        c.transform.localScale = new Vector3(size.x / 10.0f, 1, size.z / 10.0f);
        GetWalls();
        int rand = Random.Range(0, walls.transform.childCount - 1);
        GameObject startwall = walls.transform.GetChild(rand).gameObject;
        Transform start = startwall.transform.GetChild((int)Random.Range(startwall.transform.childCount * 0.4f, startwall.transform.childCount * 0.6f)).transform;
        while(start.name == "Door")
            start = startwall.transform.GetChild((int)Random.Range(startwall.transform.childCount * 0.4f, startwall.transform.childCount * 0.6f)).transform;
        GetInnerWalls(start,0);

    }
    private GameObject GetWalls()
    {
        //build walls 
        //North:
        for(int i = 0; i < size.x; i++)
        {
            GameObject n = GameObject.Instantiate(Wall, new Vector3(Zero.x + i+0.5f, size.y / 2, Zero.z + size.z), Quaternion.Euler(0,180.0f,0), NorthWall.transform);
            n.transform.localScale = new Vector3(n.transform.localScale.x, size.y, n.transform.localScale.z);
            n.name = "NorthWall";
        //South
            GameObject s = GameObject.Instantiate(Wall, new Vector3(Zero.x + i+0.5f, size.y / 2, Zero.z), Quaternion.identity, SouthWall.transform);
            s.transform.localScale = new Vector3(s.transform.localScale.x, size.y, s.transform.localScale.z);
            s.name = "SouthWall";
            if(i >= size.x/2-1 && i <= size.x/2+1)
            {
                s.name = n.name = "Door";
                RoomGenerator.DoorList.Add(s);
                RoomGenerator.DoorList.Add(n);
            }
            RoomGenerator.WallList.Add(n);
            RoomGenerator.WallList.Add(s);
        }
        //East
        for(int i = 0; i < size.z; i++)
        {
            GameObject e = GameObject.Instantiate(Wall, new Vector3(Zero.x + size.x, size.y / 2, Zero.z+i+0.5f), Quaternion.Euler(0,90.0f,0), EastWall.transform);
            e.transform.localScale = new Vector3(e.transform.localScale.x, size.y, e.transform.localScale.z);
            e.name = "EastWall";
        //West
            GameObject w = GameObject.Instantiate(Wall, new Vector3(Zero.x, size.y / 2, Zero.z+i+0.5f), Quaternion.Euler(0,-90.0f,0), WestWall.transform);
            w.transform.localScale = new Vector3(w.transform.localScale.x, size.y, w.transform.localScale.z);
            w.name = "WestWall";
            if(i >= size.z/2-1 && i <= size.z/2+1)
            {
                w.name = e.name = "Door";
                RoomGenerator.DoorList.Add(e);
                RoomGenerator.DoorList.Add(w);
            }
            RoomGenerator.WallList.Add(e);
            RoomGenerator.WallList.Add(w);
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
}
