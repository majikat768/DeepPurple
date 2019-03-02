using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

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
		}
	}

	public void OnRemoveButton()
	{
		GameObject playerCharacter = GameObject.Find("RollerBall");
		Transform player = playerCharacter.transform;
		Instantiate(item.model.transform, new Vector3(player.position.x + 1,player.position.y,player.position.z + 1), player.rotation);
		Inventory.instance.Remove(item);
	}

}
