using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Script : MonoBehaviour 
{
	public GameObject fadeOutPanel;
	public float fadeWait;
	public string sceneToLoad;

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
		if(SceneManager.GetActiveScene().buildIndex == 0)
		{
			StartCoroutine(FadeCo());
		}
		else
		{
			StartCoroutine(FadeCo());
		}
	}

	public void QuitGame()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

	public IEnumerator FadeCo()
	{
		if(fadeOutPanel != null)
		{
			Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
		}
		yield return new WaitForSeconds(fadeWait);
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
		while(!asyncOperation.isDone)
		{
			yield return null;
		}
	}
}
