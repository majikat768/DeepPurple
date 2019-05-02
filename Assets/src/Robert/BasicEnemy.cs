using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Actions))]
public class BasicEnemy : MonoBehaviour
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

    private HealthHolder healthCont;
    private HealthHolder playerHealth;
    private ParticleSystem laser;

    private GameObject gun;

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
    private int lastHealth;


    private Vector3 playerPos;



    private CombatRoom currentRoom;
    void Awake()
    {
        currentRoom = GetComponentInParent<CombatRoom>();
        Debug.Log(currentRoom);
        //stats = GameObject.FindWithTag("EnemyStats").GetComponent<EnemyStats>();
       // stats.AddObserver(this);
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
        healthCont = GetComponent<HealthHolder>();
        agent = GetComponent<NavMeshAgent>();
        action = this.gameObject.GetComponent<Actions>();
        attachWeapon();
        laser = GetComponentInChildren<ParticleSystem>();
        lastHealth = healthCont.health;
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            //debug.log("ERROR:Enemy:" + id + " Cannot find Player");
        }
        playerHealth = player.GetComponent<HealthHolder>();

    }

    // Update is called once per frame
    void Update()
    {
        //find the players gameobject, if not found, raise assertion
        
        
        //debug.log("enemy:" + id + " Player position:" + playerPos);
        float distPlayer = Vector3.Distance(transform.position, playerPos);

        if(attackRange >= distPlayer && currentRoom.PlayerInRoom)
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
        else if(attackRange < distPlayer && currentRoom.PlayerInRoom)
        {
            //debug.log("enemy:" + id + " In attack range");
            transform.LookAt(playerPos);
            action.Aiming();
            Debug.Log("Attacking player.." + id);
            InvokeRepeating("AttackPlayer", 2.0f, 1f);
        }
    }
    public void FixedUpdate()
    {
        playerPos = player.transform.position; 
        //damage was taken
        if (healthCont.health != lastHealth)
        {
            takeDamage();
            lastHealth = healthCont.health;
        }

    }

    public void takeDamage()
    {
        action.Damage();
        if (healthCont.health <= 0)
        {
            action.Death();
            Destroy(gameObject, 3);
        }
        
    }

    //Attaches a gun to each enemy when it is created
    private void attachWeapon()
    {
        GameObject rifle_1 = (GameObject)Instantiate(weapon);
        rifle_1.transform.parent = rightGunBone;
        rifle_1.transform.localPosition = Vector3.zero;
        rifle_1.transform.localRotation = Quaternion.Euler(90, 0, 0);
        gun = rifle_1;
    }
    public void AttackPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(gun.transform.forward, playerPos, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject == player)
            {
                playerHealth.health -= 2;
                Fire(hit);
                action.Attack();
            }
        }
    }
    public void Fire(RaycastHit hit)
    {

        var main = laser.main;
        var shape = laser.shape; 
        main.startLifetime = 1;
        main.startSpeed = 50;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.length = Vector3.Distance(hit.point, gun.transform.position);
        main.startColor = new Color(1, 0, 0);
    }
    public void OnHit()
    {

    }
 
}

