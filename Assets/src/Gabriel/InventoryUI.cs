using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour {


	public Transform itemsParent;
	public TextMeshProUGUI textMoney;
	public TextMeshProUGUI textHealth;
	public TextMeshProUGUI textDamage;
	PlayerHealth HP;

	Inventory inventory;

	//The whole inventory UI
	public GameObject inventoryUI;

	InventorySlot[] slots;

	// Use this for initialization
	void Start () {
		inventory = Inventory.instance;
		//listening for the delegate to get triggered
		//and saying UpdateUI function is listening
		inventory.OnItemChangedCallBack += UpdateUI;

		//finds all children to this parent
		//and looks for the InventorySlot script on the children
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();

		inventoryUI.SetActive(!inventoryUI.activeSelf);
		HP = GetComponent<PlayerHealth>();
	}
	
	void Update () {
		//Toggles inventory
		if(Input.GetKeyDown(KeyCode.I))
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
		}
	    textHealth.text = HP.getPlayerHealth().ToString();
		textDamage.text = HP.getDamage().ToString();
	}

	void UpdateUI ()
	{
		//loops through all the slots of the inventory
		for (int i = 0; i < slots.Length; i++)
		{
			//if there is an item to add
			if(i < inventory.items.Count)
			{
				slots[i].AddItem(inventory.items[i]);
			}
			//if there is no item to add
			else
			{
				slots[i].ClearSlot();
			}
		}
		textMoney.text = inventory.money.ToString();
	}
}
