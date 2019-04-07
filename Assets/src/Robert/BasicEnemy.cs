using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Actions))]
public class BasicEnemy : MonoBehaviour, IDamageable
{

    private GameObject player = null;
    private GameObject rifle_1;
    private NavMeshAgent agent;

    public Transform rightGunBone;
    public Transform leftGunBone;
    public GameObject weapon;



    public int health = 100;

    //Max distance an enemy will begin moving torward player
    public int moveMax = 10;
    //Min distance to player and enemy will get
    public int moveMin = 5;

    //movement speed of enemy
    public int moveSpeed = 3;

    //stores the actions class for animations
    private Actions action;

    void Awake()
    {

    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        action = this.gameObject.GetComponent<Actions>();
        attachWeapon();
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        Vector3 playerPos = player.transform.position;
        float distPlayer = Vector3.Distance(transform.position, playerPos);

        if(distPlayer <= moveMax && distPlayer >= moveMin)
        {
            action.Aiming();
            transform.LookAt(playerPos);
            if (moveSpeed > 4)
            {
                action.Run();
            }
            else
            {
                action.Walk();
            }
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            action.Stay();
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
}
