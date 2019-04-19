using UnityEngine;

// using a script to build a testing scene.
// i'll just add room objects all over the place.

public class testRoomBuilder : MonoBehaviour {

    public Vector3 size;
    public Vector3 Zero;

    RoomGenerator RG;
    void Start() {
        this.gameObject.AddComponent<RoomGenerator>();
        Room room = this.gameObject.AddComponent<StartRoom>();
        room.SetSize(this.size);
        room.SetZero(this.Zero);

        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
    }
}

