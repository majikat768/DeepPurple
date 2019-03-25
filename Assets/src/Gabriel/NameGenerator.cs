using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameGenerator : MonoBehaviour {

	public TextMeshProUGUI textName;

	// Use this for initialization
	void Start () {
		int randomNumb = Random.Range(0,9999);
		string subjectNumb = randomNumb.ToString();
		textName.text = "Subject: " + subjectNumb;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
