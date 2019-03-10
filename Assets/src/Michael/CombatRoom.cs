using UnityEngine;

public class CombatRoom : Room
{
    public int numEnemies = 2;
    public GameObject enemies;
    private GameObject[] spawnPoints;
    public new void Start()
    {
        enemies = new GameObject("enemies");
        enemies.AddComponent<EnemyManager>();
        // the Combat Room will have enemies spawn.
        // red blocks
        enemies.transform.parent = this.transform;
        spawnPoints = new GameObject[numEnemies];
        Debug.Log(spawnPoints);

        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 SpawnLocation = new Vector3(Zero.x + Random.Range(1, size.x - 1), 0.5f, Zero.z + Random.Range(1, size.z - 1));
            spawnPoints[i].transform.parent = this.transform;
            spawnPoints[i].transform.position = SpawnLocation;
            spawnPoints[i].transform.rotation = Quaternion.identity;
        }
        EnemyManager eScript = enemies.GetComponent<EnemyManager>();
        eScript.enemy_t = "basic";
        eScript.spawnPoints = spawnPoints;
    }
}
