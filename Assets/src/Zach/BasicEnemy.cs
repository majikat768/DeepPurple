using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IAttackable
{
    private GameObject player;
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
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
