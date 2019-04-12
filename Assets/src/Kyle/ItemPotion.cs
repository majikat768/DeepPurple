using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item {

	public override void Use()
	{
		Debug.Log("INVULNERABILITY APPLIED");
	}

}
