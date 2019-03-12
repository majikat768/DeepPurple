using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    //refrence to the player
    private GameObject player;
    private static GameObject enemy;
    public string enemy_t;
    //game objects representing the spawn points.
    //TODO: just pass a ist of vector 3d's
    public List<Vector3> spawnPoints;
    public static List<GameObject> EnemyList;
    // Use this for initialization
    void Awake() {
        spawnPoints = new List<Vector3>();
        EnemyList = new List<GameObject>();
       //if( enemy_t == "basic")
        enemy = Resources.Load<GameObject>("Robert/Soldier");
        Debug.Log(enemy);
        //Spawn();
	}
	
    /** Spawn
     * -------
     * Handles the spawning of enemies.
     * Sequentially goes through the list of spawn points
     * to spawn the enemys
     * --
     * Michael's changes:
     * Spawn() can be a function called by the CombatRoom,
     * given a location (and enemy type?)
     * see changes in CombatRoom
     **/
    public static GameObject Spawn(Vector3 spawnPoint)
    {
        /*
        foreach (GameObject spawnPoint in spawnPoints)
        {
            Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        */
        GameObject e = Instantiate(enemy, spawnPoint, Quaternion.identity);
        EnemyList.Add(e);
        return e;
    }

    public void SetSpawnPoints(List<Vector3> SpawnPoints) { this.spawnPoints = SpawnPoints; }

	void Update () {
		
	}
}
