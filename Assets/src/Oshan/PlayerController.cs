using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody bullet;
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
        bulletClone.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        bulletClone.velocity = transform.forward * 20;
    }
}
