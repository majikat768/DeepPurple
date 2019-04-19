using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemReq")]
public class Requirement : Item {

	public override void Use()
	{
		Debug.Log("Requirement");
	}
}
