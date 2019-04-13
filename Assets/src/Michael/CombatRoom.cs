using UnityEngine;
using System.Collections.Generic;

public class CombatRoom : Room
{
    public int numEnemies = 2;
    public GameObject enemies; 
    public List<Vector3> SpawnPoints;
    //public GameObject[] spawnPoints;

    protected override void Start()
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
            /*
            GameObject e = GameObject.Instantiate(Block, SpawnPoint, Quaternion.identity,enemies.transform);
            e.name = "Enemy";
            RoomGerator.EnemyList.Add(e);ne
            e.AddComponent<BasicEnemy>();
            e.GetComponent<Renderer>().material.SetColor("_Color",Color.red);
            BasicEnemy eScript = e.GetComponent<BasicEnemy>();
            eScript.moveMax = 7;
            eScript.moveMin = 3;
            */
        }
        EnemyManager eScript = enemies.GetComponent<EnemyManager>();
        eScript.enemy_t = "basic";
        eScript.SpawnPoints = SpawnPoints;
    }
}
