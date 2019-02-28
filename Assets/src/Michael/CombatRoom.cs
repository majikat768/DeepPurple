using UnityEngine;

public class CombatRoom : Room
{
    public int numEnemies = 4;

    public CombatRoom(Vector3 Zero) : base(Zero)
    {
        // the Combat Room will have enemies spawn.
        // red blocks
        GameObject enemies = new GameObject("enemies");
        enemies.transform.parent = room.transform;
        for(int i = 0; i < numEnemies; i++)
        {
            GameObject e = GameObject.Instantiate(Block, new Vector3(Zero.x + Random.Range(1, size - 1), 0.5f, Zero.z+Random.Range(1, size - 1)), Quaternion.identity,enemies.transform);
            e.AddComponent<BasicEnemy>();
            e.GetComponent<Renderer>().material.SetColor("_Color",Color.red);
        }
    }
}
