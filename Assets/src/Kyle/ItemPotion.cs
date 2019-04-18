using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item {
	public int Health;
	public float timer;
	public ItemManager UI;
	public GameObject x;

	public Potion()
	{
		Health = 20;
		timer = 10f;
	}

	public override void Use()
	{
		Potion Pot = new Potion();
		Debug.Log("Healing for ");
		UI = GameObject.Find ("Item Manager").GetComponent<ItemManager>();
		UI.UsePotion (Pot);
	}
	/*	
	void Update ()
	{
		float time = 0f;
		int addh = 20;
		HP.addHealth (20);
		while (time < timer) 
		{
			time += Time.deltaTime;
			Debug.Log ("Seconds: " + time);
		}
	}
*/
}
