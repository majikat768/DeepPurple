using UnityEngine;

public class BossRoom : Room
{
    public int numEnemies = 2;

    public new void Start()
    {
        // the Boss Room will one reeaal big enemy spawn.
        // red blocks
        Vector3 SpawnPoint = new Vector3(Zero.x + Random.Range(1, size.x - 1), 0.5f, Zero.z + Random.Range(1, size.z - 1));
        GameObject BossMan = GameObject.Instantiate(Block, SpawnPoint, Quaternion.identity, this.transform);
        BossMan.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        BossMan.AddComponent<BasicEnemy>();
        BossMan.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        BasicEnemy eScript = BossMan.GetComponent<BasicEnemy>();
        eScript.moveMax = 7;
        eScript.moveMin = 3;
        eScript.health = 300;
    }
}
