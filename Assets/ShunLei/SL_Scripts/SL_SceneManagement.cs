using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SL_SceneManagement : MonoBehaviour
{
    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        
        //Time.timeScale = 1f; // Reset time scale

         // Start a coroutine to wait before reloading the scene
        //StartCoroutine(DelayedSceneReload());

        // Now reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
