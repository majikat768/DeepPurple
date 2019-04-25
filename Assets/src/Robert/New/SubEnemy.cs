using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEnemy : BaseEnemy
{
	// Use this for initialization
	protected override void subStart ()
    {
        Debug.Log("I'm the sub enemy");
	}
	
}
