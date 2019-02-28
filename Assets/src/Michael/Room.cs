using UnityEngine;


/**
 * Room class represents a single generic room in a level. Room is instantiated by the Room Generator class.
 * Each RoomType has its own subclass such as CombatRoom or TreasureRoom. The constructors of the subclass
 * define additional unique behaviors for each room type.
 * 
 */
public class Room 
{
    public GameObject room;
    private Vector3 Zero;
    protected GameObject Wall = Resources.Load<GameObject>("Wall");
    protected GameObject Floor = Resources.Load<GameObject>("Floor");
    protected GameObject Block = Resources.Load<GameObject>("Block");
    protected int size;

    public Room(Vector3 Zero)
    {
        this.Zero = Zero;
        size = RoomGenerator.size;

        room = new GameObject("Room");
        // instantiate plane for floor; set to correct size (default plane size is 10 units)
        GameObject f = GameObject.Instantiate(Floor, new Vector3(size / 2 + Zero.x, 0, size / 2 + Zero.z), Quaternion.identity,room.transform);
        f.name = "Floor";
        f.transform.localScale = new Vector3(size / 10.0f, 1, size / 10.0f);

        GameObject walls = getWalls();

    }

    private GameObject getWalls()
    {
        //build walls 
        GameObject walls = new GameObject("Walls");
        walls.transform.parent = room.transform;
        GameObject n, s, e, w;
        for (int i = 0; i < size; i++)
        {
            if (i != size / 2)    // doors.
            {
                // North
                n = GameObject.Instantiate(Wall, new Vector3(Zero.x + i + 0.5f, 1.0f, Zero.z + size), Quaternion.identity);
                n.transform.parent = walls.transform;
                // South
                s = GameObject.Instantiate(Wall, new Vector3(Zero.x + i + 0.5f, 1.0f, Zero.z), Quaternion.identity);
                s.transform.parent = walls.transform;
                // East
                e = GameObject.Instantiate(Wall, new Vector3(Zero.x + size, 1.0f, Zero.z + i + 0.5f), Quaternion.Euler(0, 90.0f, 0));
                e.transform.parent = walls.transform;
                // West
                w = GameObject.Instantiate(Wall, new Vector3(Zero.x, 1.0f, Zero.z + i + 0.5f), Quaternion.Euler(0, 90.0f, 0));
                w.transform.parent = walls.transform;
            }
        }
        return walls;
    }
}
