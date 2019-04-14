/*
*  InventorySlot.cs
*  Programmer: Gabriel Hasenoehrl
*  Description: These are the slots for the inventory that change based
*  on the item assigned to the slot.
*/

using UnityEngine;
using UnityEngine.UI;

//Used to update the individual slots of the inventory

public class InventorySlot : MonoBehaviour 
{

	//icon is used to display the sprite for the item
	public Image icon;
	public Button removeButton;

	Item item;

	public void AddItem(Item newItem)
	{
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}

	public void ClearSlot()
	{
		item = null;
		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}

	public void UseItem()
	{
		if(item != null)
		{
			item.Use();
			Inventory.instance.Remove(item);
		}
	}

	public void OnRemoveButton()
	{
		GameObject playerCharacter = GameObject.FindWithTag("Player");
		Transform player = playerCharacter.transform;
		Rigidbody clone = Instantiate(item.model.GetComponent<Rigidbody>(), new Vector3(player.position.x,player.position.y + 2.5f,player.position.z), player.rotation);
		clone.velocity = Camera.main.transform.forward * 15;
		Inventory.instance.Remove(item);
	}

}
