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
		Rigidbody clone = Instantiate(item.model.GetComponent<Rigidbody>(), new Vector3(player.position.x,player.position.y + 2f,player.position.z), player.rotation);
		clone.velocity = Camera.main.transform.forward * 15;
		Inventory.instance.Remove(item);
	}

}
