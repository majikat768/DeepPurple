using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public GameObject launcher;
    public float maxLifeSpan = 2.0f;

    private void Start()
    {
        Destroy(gameObject, maxLifeSpan);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == launcher)
        {
            return;
        }
        IAttackable attackable = (IAttackable) collider.gameObject.GetComponent<IAttackable>();
        if (attackable != null)
        {
            DamageSource damageSource = new DamageSource();
            damageSource.baseDamage = 25;
            damageSource.damageType = DamageType.PROJECTILE;
            attackable.takeDamage(damageSource);
        }
        Destroy(gameObject);
    }

}
