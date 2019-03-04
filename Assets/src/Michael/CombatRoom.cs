using UnityEngine;

public class CombatRoom : Room
{
    public int numEnemies = 2;

    public CombatRoom(Vector3 Zero,GameObject r) : base(Zero,r)
    {
        // the Combat Room will have enemies spawn.
        // red blocks
        GameObject enemies = new GameObject("enemies");
        enemies.transform.parent = room.transform;
        for(int i = 0; i < numEnemies; i++)
        {
            Vector3 SpawnPoint = new Vector3(Zero.x + Random.Range(1,size.x-1),0.5f,Zero.z + Random.Range(1,size.z-1));
            GameObject e = GameObject.Instantiate(Block, SpawnPoint, Quaternion.identity,enemies.transform);
            RoomGenerator.EnemyList.Add(e);
            e.AddComponent<BasicEnemy>();
            e.GetComponent<Renderer>().material.SetColor("_Color",Color.red);
            BasicEnemy eScript = e.GetComponent<BasicEnemy>();
            eScript.moveMax = 7;
            eScript.moveMin = 3;
        }
    }
}
