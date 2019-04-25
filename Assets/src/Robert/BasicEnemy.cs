using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Actions))]
public class BasicEnemy : MonoBehaviour, IDamageable ,ICallback
{
  
    private static int count = 0;
    private int id;
    private GameObject player = null;
    private GameObject rifle_1;
    private NavMeshAgent agent;
    private EnemyStats stats;

    public Transform rightGunBone;
    public Transform leftGunBone;
    public GameObject weapon;

 
    public int health = 100;

    //Max distance an enemy will begin moving torward player
    public int moveMax = 30;
    //Min distance to player and enemy will get
    public int moveMin = 5;

    //movement speed of enemy
    public int moveSpeed = 3;

    public float attackRange = 1;
    //stores the actions class for animations
    private Actions action;

    private Vector3 playerPos;

    void Awake()
    {
        stats = GameObject.FindWithTag("EnemyStats").GetComponent<EnemyStats>();
        stats.AddObserver(this);
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.Log("ERROR:Enemy:" + id + " Cannot find Player");
        }
    }
    void Start()
    {
       // target.AddObserver(this);
        //store the amount of enmies on the map
        id = count++;

        agent = GetComponent<NavMeshAgent>();
        action = this.gameObject.GetComponent<Actions>();
        attachWeapon();
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            //debug.log("ERROR:Enemy:" + id + " Cannot find Player");
        }


    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            //debug.log("ERROR:Enemy:" + id + " Cannot find Player");
        }
       //Vector3 playerPos = player.transform.position;
        //debug.log("enemy:" + id + " Player position:" + playerPos);

        float distPlayer = Vector3.Distance(transform.position, playerPos);
        if(attackRange >= distPlayer)
        {
            //debug.log( id + "enemy" +  " Moving torward player");
            //debug.log(id + "enemy" + " agent.speed:" + agent.speed);
            agent.destination = playerPos;

            if (agent.speed > 4)
            {
                action.Run();
            }
            else
            {
                action.Walk();
            }
        }
        else if(attackRange < distPlayer)
        {
            //debug.log("enemy:" + id + " In attack range");
            transform.LookAt(playerPos);
            action.Aiming();
        }
    }

    public void takeDamage(DamageSource damageSource)
    {
        health -= damageSource.baseDamage;
        action.Damage();
        if (health <= 0)
        {
            action.Death();
        }
    }

    //Attaches a gun to each enemy when it is created
    private void attachWeapon()
    {
        GameObject rifle_1 = (GameObject)Instantiate(weapon);
        rifle_1.transform.parent = rightGunBone;
        rifle_1.transform.localPosition = Vector3.zero;
        rifle_1.transform.localRotation = Quaternion.Euler(90, 0, 0);
    }

    public void UpdatePos()
    {
        playerPos = stats.newPlayerPos;
    }
}
