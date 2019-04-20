using UnityEngine;
using System.Collections.Generic;

public class CombatRoom : Room
{
    public int numEnemies = 2;
    public GameObject enemies; 
    public List<Vector3> SpawnPoints;
    //public GameObject[] spawnPoints;

    protected void Start()
    {
        enemies = new GameObject("enemies");
        enemies.gameObject.AddComponent<EnemyManager>();
        // the Combat Room will have enemies spawn.

        enemies.transform.parent = this.transform;
        SpawnPoints = new List<Vector3>();
        //spawnPoints = new GameObject[numEnemies];

        for(int i = 0; i < numEnemies; i++)
        {
            Vector3 spawnPoint = Zero + new Vector3(Random.Range(1,size.x-1),0.5f,Random.Range(1,size.z-1));
            SpawnPoints.Add(spawnPoint);
        }
        EnemyManager eScript = enemies.GetComponent<EnemyManager>();
        eScript.enemy_t = "basic";
        eScript.SpawnPoints = SpawnPoints;
    }
}
