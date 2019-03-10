using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    //refrence to the player
    private GameObject player;
    private GameObject enemy;
    public string enemy_t;
    //game objects representing the spawn points.
    //TODO: just pass a ist of vector 3d's
    public GameObject[] spawnPoints;
    // Use this for initialization
    void Start () {
       //if( enemy_t == "basic")
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
        foreach (GameObject spawnPoint in spawnPoints)
        {
            Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }
	void Update () {
		
	}
}
