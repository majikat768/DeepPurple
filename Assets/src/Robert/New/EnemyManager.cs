/* EnemyManager.cs
 * Programmer: Robert Goes
 * Description: 
 * Manages spawning enemys in each combat room. spawning a random enemy
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    //refrence to the player
    private GameObject enemy;
    private GameObject room;

    //type of enemy requested
    public string enemy_t; 
    //game objects representing the spawn points.s
    public List<Vector3> SpawnPoints;
    // Use this for initialization

    void Start ()
    {
       room = this.transform.parent.gameObject;
       //stores which random enemy will be in the room
       int randType = Random.Range(0, 2);
       if(SpawnPoints == null)
       {
            Debug.LogError("No spawn points were passed to enemy manager");
       }
        enemy = Resources.Load<GameObject>("Robert/Soldier_type" + randType);
        Spawn();
	}
	
    
      // Handles the spawning of enemies.
     //Sequentially goes through the list of spawn points
     // to spawn the enemys
     
    void Spawn()
    {
        foreach (Vector3 spawnPoint in SpawnPoints)
        {
            GameObject e = Instantiate(enemy, spawnPoint, Quaternion.identity);
            e.transform.parent = this.transform;
            //e.AddComponent<BasicEnemy>();
        }
    }
	
}
