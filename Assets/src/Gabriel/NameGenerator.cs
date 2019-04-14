/*
*  NameGenerator.cs
*  Programmer: Gabriel Hasenoehrl
*  Description: This is used to give a new name to the player each
*  playthrough.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameGenerator : MonoBehaviour 
{

	public TextMeshProUGUI textName;

	void Start () 
	{
		int randomNumb = Random.Range(0,9999);
		string subjectNumb = randomNumb.ToString();
		textName.text = "Subject: " + subjectNumb;
	}

}
