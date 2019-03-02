using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton

	public static Inventory instance;
	
	void Awake()
	{
		if(instance != null)
		{
			Debug.LogWarning("More than 1 instance of Inventory");
			return;
		}
		instance = this;
	}

	#endregion

	public delegate void OnItemChanged();
	public OnItemChanged OnItemChangedCallBack;

	//inventory variables
	int space = 25;
	public int money = 0;

	public List<Item> items = new List<Item>();

	public bool Add(Item item)
	{
		if(!item.isCurrency)
		{
			if(items.Count >= space)
			{
				Debug.Log("Not enough room");
				return false;
			}
			items.Add(item);
			if (OnItemChangedCallBack != null)
			{
				OnItemChangedCallBack.Invoke();
			}
		}
		else
		{
			money++;
			if (OnItemChangedCallBack != null)
			{
				OnItemChangedCallBack.Invoke();
			}
		}
		return true;
	}

	public void Remove (Item item)
	{
		items.Remove(item);
		if (OnItemChangedCallBack != null)
		{
			OnItemChangedCallBack.Invoke();
		}
	}


}
