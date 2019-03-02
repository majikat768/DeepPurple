using UnityEngine;

public class Room 
{
    public GameObject room;
    private Vector3 Zero;
    protected GameObject Wall = Resources.Load<GameObject>("Michael/Wall");
    protected GameObject Plane = Resources.Load<GameObject>("Michael/Plane");
    protected GameObject Block = Resources.Load<GameObject>("Michael/Block");
    protected GameObject PlayerBall = Resources.Load<GameObject>("Oshan/RollerBall");
    protected GameObject FirstPerson = Resources.Load<GameObject>("Oshan/Player");
    protected Vector3 size = RoomGenerator.GetSize();

    GameObject walls;
    GameObject NorthWall;
    GameObject SouthWall;
    GameObject EastWall;
    GameObject WestWall;
    GameObject InnerWalls;

    public Room(Vector3 Zero)
    {
        room = new GameObject("Room");
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

        // instantiate plane for floor; set to correct size (default plane size is 10 units)

    }
    public void Generate() {
        GameObject f = GameObject.Instantiate(
            Plane, 
            new Vector3(size.x / 2 + Zero.x, 0, size.z / 2 + Zero.z), 
            Quaternion.identity,
            room.transform);
        f.name = "Floor";
        f.transform.localScale = new Vector3(size.x / 10.0f, 1, size.z / 10.0f);

        GameObject c = GameObject.Instantiate(
            Plane, 
            new Vector3(size.x / 2 + Zero.x, size.y, size.z / 2 + Zero.z), 
            Quaternion.Euler(180.0f, 0, 0), 
            room.transform);
        c.name = "Ceiling";
        c.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.0f, 1.0f, 1.0f, 1.0f));
        c.transform.localScale = new Vector3(size.x / 10.0f, 1, size.z / 10.0f);
        GameObject walls = GetWalls();
        Transform s = walls.transform.GetChild(Random.Range(0, walls.transform.childCount - 1));
        s = s.GetChild(Random.Range(1, s.childCount - 2));
        GetInnerWalls(s,0);

    }
    private GameObject GetWalls()
    {
        //build walls 
        //North:
        GetWall(
            Zero.x + size.x, 
            Zero.z + size.z,
            Zero.x, 
            Zero.z + size.z, 
            NorthWall.transform);
        //South:
        GetWall(
            Zero.x, 
            Zero.z, 
            Zero.x + size.x, 
            Zero.z, 
            SouthWall.transform);
        //East:
        GetWall(
            Zero.x + size.x, 
            Zero.z, 
            Zero.x + size.x, 
            Zero.z + size.z,
            EastWall.transform);
        //West:
        GetWall(
            Zero.x, 
            Zero.z + size.z, 
            Zero.x, 
            Zero.z, 
            WestWall.transform);
        return walls;
    }

    public void GetWall(float x1, float z1, float x2, float z2,Transform parent)
    {
        float direction = Mathf.Atan2(z2 - z1, x2 - x1);
        float distance = Vector3.Distance(new Vector3(x1, 0.0f, z1), new Vector3(x2, 0.0f, z2));
        Vector3 eulerAngle = new Vector3(0, direction*180.0f/Mathf.PI, 0);
        for(float i = 0.5f; i < distance; i += 1)
        {
            Vector3 location = new Vector3(x1 + Mathf.Cos(direction) * i, size.y / 2, z1 + Mathf.Sin(direction) * i);

            GameObject w = GameObject.Instantiate(Wall, location, Quaternion.Euler(eulerAngle), parent);
            w.transform.localScale = new Vector3(1.0f, size.y, 0.1f);

            if(Mathf.Floor(i) == distance/2 || Mathf.Floor(i) == distance/2-1)
            {
                w.name = "Door";
                RoomGenerator.DoorList.Add(w);
            }
        }

    }

    public void GetInnerWalls(Transform start, int depth)
    {
        if (depth > 2) return;
        Vector3 direction = start.rotation.eulerAngles;
        direction = new Vector3(direction.x, direction.y + 90.0f, direction.z);
        Vector3 end = new Vector3(0,0,0);
        switch((int)direction.y%360)
        {
            case 0:
                //travelling east; find difference between start x coord and zero.x+size.x
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
        for (float i = 0.5f; i < distance; i += 1)
        {
            if (i < distance * 0.33 || i > distance * 0.66)
            {
                float newX = start.position.x + Mathf.Cos(direction.y * Mathf.Deg2Rad) * i + Mathf.Sin(direction.y * Mathf.Deg2Rad) * 0.5f;
                float newZ = start.position.z + Mathf.Sin(direction.y * Mathf.Deg2Rad) * i + Mathf.Cos(direction.y * Mathf.Deg2Rad) * 0.5f;
                Vector3 location = new Vector3(newX, size.y / 4, newZ);

                GameObject w = GameObject.Instantiate(Wall, location, Quaternion.Euler(direction), InnerWalls.transform);
                w.transform.localScale = new Vector3(1.0f, size.y/2, 0.1f);
            }
        }

        GetInnerWalls(InnerWalls.transform.GetChild(Random.Range(1, InnerWalls.transform.childCount - 2)),depth+1);

    }
    public void SetSize(float x, float y, float z) {
        this.size = new Vector3(x,y,z);
    }

    public Vector2 GetSize() { return size; }
}
