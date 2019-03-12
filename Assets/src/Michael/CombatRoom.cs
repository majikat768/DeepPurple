using System.Collections.Generic;
using UnityEngine;

public class CombatRoom : Room
{
    public int numEnemies = 2;
    public GameObject enemies;
    public static List<Vector3> SpawnPoints;

    public new void Start()
    {
        enemies = new GameObject("enemies");
        enemies.AddComponent<EnemyManager>();
        // the Combat Room will have enemies spawn.
        // red blocks
        enemies.transform.parent = this.transform;
        SpawnPoints = new List<Vector3>();
        Debug.Log(SpawnPoints);

        //each location calls Robert's Spawn() function with the new location,
        // and set's the enemy object as a child of this room object.
        // This way I don't think EnemyManager will need to be a component of any object,
        
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 SpawnLocation = new Vector3(Zero.x + Random.Range(1, size.x - 1), 0.5f, Zero.z + Random.Range(1, size.z - 1));
            SpawnPoints.Add(SpawnLocation);
            EnemyManager.Spawn(SpawnLocation).transform.parent = this.transform;
        }

        EnemyManager eScript = enemies.GetComponent<EnemyManager>();
        eScript.enemy_t = "basic";
        eScript.SetSpawnPoints(SpawnPoints);
    }
}
