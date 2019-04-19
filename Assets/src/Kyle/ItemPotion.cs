/*	ItemPotion.cs
 *	Name: Kyle Hild
 *	Description: Creates a scriptable item of Potion that creates a new potion, uses the item manager to use
 *	the potions in game
 */

using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item 
{
	public int Health;
	public float timer;
	public ItemManager UI;
	public GameObject x;

	public Potion()
	{
		Health = 20;
		timer = 10f;
	}
	//Overrides the Function in Gabriels scripts Which is declared virtual to overwrite his USE function
	public override void Use()
	{
		Potion Pot = new Potion();
		UI = GameObject.Find ("Item Manager").GetComponent<ItemManager>();
		UI.UsePotion (Pot);
	}
}
