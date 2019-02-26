using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public GameObject launcher;

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
            damageSource.baseDamage = 1;
            damageSource.damageType = DamageType.PROJECTILE;
            attackable.takeDamage(damageSource);
            Debug.Log("Hit");
        }
        Destroy(gameObject);
    }

}
