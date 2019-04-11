using UnityEngine;

public class BossRoom : Room
{
    GameObject exit,Exit;
    GameObject BossMan;
    ParticleSystem ExitPS;
    GameObject boss;
    Vector3 SpawnPoint;
    bool bossDead;

    public new void Start()
    {
        base.Start();
        exit = Resources.Load<GameObject>("Michael/Exit");
        Exit = GameObject.Instantiate(exit);
        Exit.transform.parent = this.transform;
        ExitPS = Exit.GetComponent<ParticleSystem>();
        Exit.transform.position = Zero+new Vector3(2,2,2);
        // the Boss Room will one reeaal big enemy spawn.
        // red blocks
        boss = Resources.Load<GameObject>("Robert/Soldier");
        SpawnPoint = new Vector3(Zero.x + Random.Range(1, size.x - 1), 0.5f, Zero.z + Random.Range(1, size.z - 1));

        BossMan = GameObject.Instantiate(boss, SpawnPoint, Quaternion.identity, this.transform);
        BossMan.AddComponent<BasicEnemy>();
        BasicEnemy eScript = BossMan.GetComponent<BasicEnemy>();
        eScript.moveMax = 7;
        eScript.moveMin = 3;
        eScript.health = 300;
        //BossMan.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);

        Destroy(this.transform.Find("Teleporter").gameObject);
    }
    void Update() {
        if(BossMan == null && !bossDead) {
            bossDead = true;
            ExitPS.Play();
        }
    }
}
