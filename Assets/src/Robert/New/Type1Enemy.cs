/* Type1Enemy.cs
 * Programmer: RobertGoes
 * instation of the base classe BaseEnemy, an example of dynamic binding, 
 * with it's use overide, usage of the method subStart that is called by unity is
 * dynamic binding as it happens at runtime
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1Enemy : BaseEnemy
{
    // Use this for initialization
    protected override void subStart()
    {
        Debug.Log("A type2 enemy has spawned");
    }
}