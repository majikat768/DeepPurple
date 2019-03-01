using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IAttackable
{
    public Rigidbody bullet;
    private int health = 200;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            attack();
        }
    }

    void attack()
    {
        Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.GetComponent<BasicBullet>().launcher = gameObject;
        bulletClone.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        bulletClone.velocity = Camera.main.transform.forward * 25;
    }

    public void takeDamage(DamageSource damageSource)
    {
        if (!Registry.GetOrDefault<bool>("bcmode", false))
        {
            health -= damageSource.baseDamage;
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
