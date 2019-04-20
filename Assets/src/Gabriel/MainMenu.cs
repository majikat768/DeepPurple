/*
*  MainMenu.cs
*  Programmer: Gabriel Hasenoehrl
*  Description: First menu script.  NO LONGER IN USE.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartPushed()
    {
        SceneManager.LoadScene(1);
    }
}
