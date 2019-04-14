/*
*  Item.cs
*  Programmer: Gabriel Hasenoehrl
*  Description: This is the basic item for the game. Kyle will use this
*  class mostly.  I created it in order to properly set up my inventory 
*  system.  It is very basic, and should be modified through the virtual
*  function.
*/

using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject 
{

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
