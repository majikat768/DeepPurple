using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase {
	public static GameObject[] AllItems = Resources.LoadAll<GameObject>("Kyle/Items");
	public static GameObject InvulnPotRef = Resources.Load<GameObject>("Kyle/Items/Invulnerability");



	public static GameObject RandomItemGrabber()
	{
		GameObject i = AllItems[Random.Range(0,AllItems.Length)];
			return i;
	}
}

