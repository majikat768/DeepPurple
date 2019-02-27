using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IAttackable
{
    private GameObject player;
    public int health = 100;

    //Max distance an enemy will begin moving torward player
    public int moveMax = 100;
    //Min distance to player and enemy will get
    public int moveMin = 10;

    //movement speed of enemy
    public int moveSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        float distPlayer = Vector3.Distance(transform.position, playerPos);

        if(distPlayer <= moveMax && distPlayer >= moveMin)
        {
            transform.LookAt(playerPos);
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

    }

    public void takeDamage(DamageSource damageSource)
    {
        health -= damageSource.baseDamage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
