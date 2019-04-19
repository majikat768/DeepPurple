using UnityEngine;

// using a script to build a testing scene.
// i'll just add room objects all over the place.

public class testBuild : MonoBehaviour {

    RoomGenerator RG;
    void Start() {
        RG = RoomGenerator.instance;
        Room StartRoom = new GameObject("Start Room").AddComponent<StartRoom>();
        StartRoom.SetSize(new Vector3(36,24,24));
        StartRoom.SetZero(new Vector3(-12,0,0));
        StartRoom.complexity = -1;

        Room BoxRoom = new GameObject("Box").AddComponent<PuzzleBox>();
        BoxRoom.SetSize(new Vector3(24,4,20));
        BoxRoom.SetZero(new Vector3(64,0,0));

        Room TurretRoom = new GameObject("Turrets").AddComponent<PuzzleTurrets>();
        TurretRoom.SetZero(new Vector3(24,0,0));
        TurretRoom.SetSize(new Vector3(24,4,24));

        Room RabbitRoom = new GameObject("Rabbits").AddComponent<PuzzleRabbits>();
        RabbitRoom.SetZero(new Vector3(0,0,24));
        RabbitRoom.SetSize(new Vector3(48,4,24));

        Room FloatingRoom = new GameObject("Flying").AddComponent<PuzzleLowGravity>();
        FloatingRoom.SetZero(new Vector3(48,0,0));
        FloatingRoom.SetSize(new Vector3(16,4,28));

        /*
        Room PlatformRoom = new GameObject("Platformer").AddComponent<PuzzlePlatforms>();
        PlatformRoom.SetZero(new Vector3(48,0,28));
        PlatformRoom.SetSize(new Vector3(40,4,24));
        /*/

        Room PlatformRoom = new GameObject("Platformer").AddComponent<PuzzleSuperPlatforms>();
        PlatformRoom.SetZero(new Vector3(48,0,28));
        PlatformRoom.SetSize(new Vector3(24,4,24));

        Room PlatformRoom2 = new GameObject("Platformer").AddComponent<PuzzleSuperPlatforms>();
        PlatformRoom2.SetZero(new Vector3(72,0,28));
        PlatformRoom2.SetSize(new Vector3(24,4,24));

        Room PlatformRoom3 = new GameObject("Platformer").AddComponent<PuzzleSuperPlatforms>();
        PlatformRoom3.SetZero(new Vector3(96,0,28));
        PlatformRoom3.SetSize(new Vector3(24,4,24));

        Room PlatformRoom4 = new GameObject("Platformer").AddComponent<PuzzleSuperPlatforms>();
        PlatformRoom4.SetZero(new Vector3(120,0,28));
        PlatformRoom4.SetSize(new Vector3(24,4,24));
        /**/

        Room CombatRoom = new GameObject("Fight").AddComponent<CombatRoom>();
        CombatRoom.SetZero(new Vector3(8,0,48));
        CombatRoom.SetSize(new Vector3(24,4,32));

        Room TreasureRoom = new GameObject("Treasure").AddComponent<TreasureRoom>();
        TreasureRoom.SetZero(new Vector3(64,0,20));
        TreasureRoom.SetSize(new Vector3(24,4,8));
        TreasureRoom.complexity = -1;

        Room Hallway1 = new GameObject("Hallway").AddComponent<Room>();
        Hallway1.SetZero(new Vector3(0,0,-4));
        Hallway1.SetSize(new Vector3(88,4,4));
        Hallway1.addPortal = false;
        Hallway1.complexity = -1;

        Room Hallway2 = new GameObject("Hallway2").AddComponent<Room>();
        Hallway2.SetZero(new Vector3(88,0,-4));
        Hallway2.SetSize(new Vector3(4,4,32));
        Hallway2.addPortal = false;
        Hallway2.complexity = -1;

        Room Boss = new GameObject("Boss Room").AddComponent<BossRoom>();
        Boss.SetZero(new Vector3(32,0,48));
        Boss.SetSize(new Vector3(16,4,24));

        this.gameObject.AddComponent<map>();

        RoomGenerator.BuildDoors();
        RoomGenerator.BakeNavMesh();
    }
}

