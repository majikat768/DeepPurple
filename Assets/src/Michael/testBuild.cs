using UnityEngine;

public class testBuild : MonoBehaviour {

    void Start() {
        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
    }
}
