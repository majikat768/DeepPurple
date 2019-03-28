﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    //refrence to the player
    private GameObject player;
    private GameObject enemy;
    public string enemy_t;
    //game objects representing the spawn points.
    //TODO: just pass a ist of vector 3d's
    public List<Vector3> SpawnPoints;
    // Use this for initialization
    void Start () {
       //if( enemy_t == "basic")
       if(SpawnPoints == null) {
           SpawnPoints = this.GetComponent<CombatRoom>().SpawnPoints;
       }
        enemy = Resources.Load<GameObject>("Robert/Soldier");
        Debug.Log(enemy);
        Spawn();
	}
	
    /** Spawn
     * -------
     * Handles the spawning of enemies.
     * Sequentially goes through the list of spawn points
     * to spawn the enemys
     **/
    void Spawn()
    {
        foreach (Vector3 spawnPoint in SpawnPoints)
        {
            GameObject e = Instantiate(enemy, spawnPoint, Quaternion.identity);
            //e.AddComponent<BasicEnemy>();
        }
    }
	void Update () {
		
	}
}
