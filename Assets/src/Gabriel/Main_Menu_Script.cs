/*
*  ChestOpening.cs
*  Programmer: Gabriel Hasenoehrl
*  Description: This is the updated Menu script.  It provides all interactions
*  with the main menu of the game.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Script : MonoBehaviour 
{
	public GameObject fadeOutPanel;
	public float fadeWait;
	public string sceneToLoad;
	public string sceneToLoad3rd;

	private void Awake()
	{
		if(fadeOutPanel != null)
		{
			GameObject panel = Instantiate(fadeOutPanel,Vector3.zero, Quaternion.identity) as GameObject;
			Destroy(panel, 1);
		}
	}

	public void PlayGame()
	{
		StartCoroutine(FadeCo(sceneToLoad));
	}

	public void PlayGame3rd()
	{
		StartCoroutine(FadeCo(sceneToLoad3rd));
	}

	public void QuitGame()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

	public IEnumerator FadeCo(string scene)
	{
		if(fadeOutPanel != null)
		{
			Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
		}
		yield return new WaitForSeconds(fadeWait);
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
		while(!asyncOperation.isDone)
		{
			yield return null;
		}
	}
}
