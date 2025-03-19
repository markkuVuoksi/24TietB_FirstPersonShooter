using UnityEngine;
using UnityEngine.SceneManagement;

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
}
