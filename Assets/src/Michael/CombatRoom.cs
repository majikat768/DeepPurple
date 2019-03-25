using UnityEngine;

public class CombatRoom : Room
{
    public int numEnemies = 2;
    public GameObject enemies; 

    public new void Start()
    {
        enemies = new GameObject("enemies");
        // the Combat Room will have enemies spawn.
        // red blocks

        enemies.transform.parent = this.transform;
        for(int i = 0; i < numEnemies; i++)
        {
            Vector3 SpawnPoint = Zero + new Vector3(Random.Range(1,size.x-1),0.5f,Random.Range(1,size.z-1));
            GameObject e = GameObject.Instantiate(Block, SpawnPoint, Quaternion.identity,enemies.transform);
            e.name = "Enemy";
            RoomGenerator.EnemyList.Add(e);
            e.AddComponent<BasicEnemy>();
            e.GetComponent<Renderer>().material.SetColor("_Color",Color.red);
            BasicEnemy eScript = e.GetComponent<BasicEnemy>();
            eScript.moveMax = 7;
            eScript.moveMin = 3;
        }
    }
}
