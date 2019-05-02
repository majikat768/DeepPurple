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


