/*
*  ItemPickup.cs
*  Programmer: Gabriel Hasenoehrl
*  Description: This is attached to any prefab item.  As this is how
*  an item gets properly picked up and added to the inventory system.
*/using UnityEngine;

public class ItemPickup : Interactable 
{

	public Item item;

	public override void Interact()
	{
		// calls base interact method
		base.Interact();

		PickUp();
	}

	//adds a picked up item to the inventory
	void PickUp()
	{
		//Debug.Log("Picking up " + item.name);

		//adds item to the inventory
		bool pickedUp = Inventory.instance.Add(item);

		//checks if inventory is full before destroying item 
		if(pickedUp)
		{
			Destroy(gameObject);
		}
	}
}
