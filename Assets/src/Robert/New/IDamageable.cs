/* BaseEnemy.cs
 * Programmer: RobertGoes
 * Serves as an interface between the objects that have health
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable
{
    //Gets the max healh
    int getMaxHealth();
    //Gets current health
    int getHealth();
    //Modifys health by certian amount
    void modifyHealth(int amount);
}


