using UnityEngine;

public class Room 
{
    public GameObject room;
    private Vector3 Zero;
    protected GameObject Wall = Resources.Load<GameObject>("Michael/Wall");
    protected GameObject Floor = Resources.Load<GameObject>("Michael/Floor");
    protected GameObject Block = Resources.Load<GameObject>("Michael/Block");
    protected GameObject PlayerBall = Resources.Load<GameObject>("Oshan/RollerBall");
    protected Vector3 size = RoomGenerator.GetSize();

    public Room(Vector3 Zero)
    {
        this.Zero = Zero;

        room = new GameObject("Room");
        // instantiate plane for floor; set to correct size (default plane size is 10 units)

    }
    public void Generate() {
        GameObject f = GameObject.Instantiate(Floor, new Vector3(size.x / 2 + Zero.x, 0, size.z / 2 + Zero.z), Quaternion.identity,room.transform);
        f.name = "Floor";
        f.transform.localScale = new Vector3(size.x / 10.0f, 1, size.z / 10.0f);

        GameObject walls = getWalls();
    }
    private GameObject getWalls()
    {
        //build walls 
        GameObject walls = new GameObject("Walls");
        walls.transform.parent = room.transform;
        GameObject NorthWall = new GameObject("NorthWall");
        NorthWall.transform.parent = walls.transform;
        GameObject SouthWall = new GameObject("SouthWall");
        SouthWall.transform.parent = walls.transform;
        GameObject EastWall = new GameObject("EastWall");
        EastWall.transform.parent = walls.transform;
        GameObject WestWall = new GameObject("WestWall");
        WestWall.transform.parent = walls.transform;
        GameObject n, s, e, w;
        for(int i = 0; i < size.x; i++) {
            // North
            n = GameObject.Instantiate(Wall, new Vector3(Zero.x + i + 0.5f, size.y/2, Zero.z + size.z), Quaternion.identity);
            n.transform.parent = NorthWall.transform;
            n.transform.localScale = new Vector3(1.0f,size.y,0.1f);
            if(i == size.x/2 || i == size.x/2-1)
            {
                n.name = "Door";
                RoomGenerator.DoorList.Add(n);
            }
            // South
            s = GameObject.Instantiate(Wall, new Vector3(Zero.x + i + 0.5f, size.y/2, Zero.z), Quaternion.identity);
            s.transform.parent = SouthWall.transform;
            s.transform.localScale = new Vector3(1.0f,size.y,0.1f);
            if(i == size.x/2 || i == size.x/2-1)
            {
                s.name = "Door";
                RoomGenerator.DoorList.Add(s);
            }
        }

        for(int i = 0; i < size.z; i++)
        {
            //East
            e = GameObject.Instantiate(Wall, new Vector3(Zero.x+size.x, size.y/2, Zero.z + i + 0.5f), Quaternion.Euler(0,90.0f,0));
            e.transform.parent = EastWall.transform;
            e.transform.localScale = new Vector3(1.0f,size.y,0.1f);
            if(i == size.z/2 || i == size.z/2-1)
            {
                e.name = "Door";
                RoomGenerator.DoorList.Add(e);
            }
            // West
            w = GameObject.Instantiate(Wall, new Vector3(Zero.x,size.y/2,Zero.z + i + 0.5f),Quaternion.Euler(0,90.0f,0));
            w.transform.parent = WestWall.transform;
            w.transform.localScale = new Vector3(1.0f,size.y,0.1f);
            if(i == size.z/2 || i == size.z/2-1)
            {
                w.name = "Door";
                RoomGenerator.DoorList.Add(w);
            }
        }
        return walls;
    }

    public void SetSize(float x, float y, float z) {
        this.size = new Vector3(x,y,z);
    }

    public Vector2 GetSize() { return size; }
}
