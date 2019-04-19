/*	FPSDisplay.cs
 *	Name: Kyle Hild
 *	Description: This Code Was Reused by https://wiki.unity3d.com/index.php/FramesPerSecond Author: Dave Hampson
 *	This displays the current FPS in the topleft corner
 */

using UnityEngine;
using System.Collections;

public class FPSDiplay:MonoBehaviour
{
	//No Changes were made to be able to implement into the game this is exact
	float deltaTime = 0.0f;

	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
}