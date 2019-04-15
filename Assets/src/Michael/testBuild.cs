using UnityEngine;

public class testBuild : MonoBehaviour {

    void Awake() {
        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
    }
}
