using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	new public string name = "New Item";
	public Sprite icon = null;
	public bool isCurrency = false;
	public GameObject model;

	//Different items will do different things so this is why
	//it is a virtual function, as each item should overwrite this.
	public virtual void Use()
	{
		Debug.Log("Using " + name);
	}

}
