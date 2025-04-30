using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MM_MainMenu : MonoBehaviour
{
   public void PlayGame(string name)
    {
        SceneManager.LoadSceneAsync(name);
        Debug.Log("Scene Loaded");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
