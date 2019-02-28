using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Script : MonoBehaviour 
{
	public void PlayGame()
	{
		if(SceneManager.GetActiveScene().buildIndex == 0)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
	}

	public void QuitGame()
	{
		Debug.Log("Quit");
		Application.Quit();
	}
}
