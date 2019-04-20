using UnityEngine;

public class BossRoom : Room
{
    GameObject exit,Exit;
    GameObject BossMan;
    ParticleSystem ExitPS;
    GameObject boss;
    Vector3 SpawnPoint;
    bool bossDead;

    protected void Start()
    {
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
        BossMan.name = "Boss";
        BasicEnemy eScript = BossMan.GetComponent<BasicEnemy>();
        BossMan.transform.Find("Soldier").GetComponent<Renderer>().material = new Material(Shader.Find("Standard (Specular setup)"));
        BossMan.transform.Find("Soldier").GetComponent<Renderer>().material.SetColor("_Color",Color.black);
        BossMan.transform.Find("Soldier").GetComponent<Renderer>().material.SetColor("_Specular",Color.white);
        eScript.moveMax = 7;
        eScript.moveMin = 3;
        eScript.health = 300;
        //BossMan.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);

        RG.teleporterList.Remove(this.transform.Find("Teleporter").gameObject);
        Destroy(this.transform.Find("Teleporter").gameObject);

        foreach(Transform w in Walls.transform) {
            w.GetComponent<Renderer>().materials[0].SetColor("_Color",new Color(0.08f,0,0));
        }
    }

    void Update() {
        if(BossMan == null && !bossDead) {
            bossDead = true;
            Exit.AddComponent<CapsuleCollider>().isTrigger = true;
            Exit.GetComponent<CapsuleCollider>().center = Vector3.zero;
            Exit.GetComponent<CapsuleCollider>().radius = 2;
            Exit.GetComponent<CapsuleCollider>().height = 2;
            
            ExitPS.Play();
        }
    }
}
