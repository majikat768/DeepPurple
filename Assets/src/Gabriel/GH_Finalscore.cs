/*
*  GH_Finalscore.cs
*  Programmer: Gabriel Hasenoehrl
*  Description: This is used in order to keep track of the final score
*  for the player.  Should be displayed at the end of the game.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GH_Finalscore : MonoBehaviour 
{

	public TextMeshProUGUI textScore;
	public Inventory inv;
	public GameObject scoreUI;

	void Start () 
	{
		inv = GetComponent<Inventory>();
	}

	void Update()
	{
		textScore.text = inv.getScore().ToString();
		/*//Testing
		if(Input.GetKeyDown(KeyCode.Z))
		{
			showScore();
		}
        */
	}

	public void showScore()
	{
		scoreUI.SetActive(!scoreUI.activeSelf);
	}
}
