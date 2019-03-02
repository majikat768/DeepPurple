using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;
	public TextMeshProUGUI textMoney;

	Inventory inventory;
	public GameObject inventoryUI;

	InventorySlot[] slots;

	// Use this for initialization
	void Start () {
		inventory = Inventory.instance;
		inventory.OnItemChangedCallBack += UpdateUI;

		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.I))
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
		}
	}

	void UpdateUI ()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if(i < inventory.items.Count)
			{
				slots[i].AddItem(inventory.items[i]);
			}
			else
			{
				slots[i].ClearSlot();
			}
		}
		textMoney.text = inventory.money.ToString();
	}
}
