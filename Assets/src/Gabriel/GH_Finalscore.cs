using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GH_Finalscore : MonoBehaviour {

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
		//Testing
		if(Input.GetKeyDown(KeyCode.Z))
		{
			showScore();
		}
	}

	public void showScore()
	{
		scoreUI.SetActive(!scoreUI.activeSelf);
	}
}
