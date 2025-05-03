using UnityEngine;
using UnityEngine.SceneManagement;

public class AZ_MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Имя сцены, которую нужно загрузить
    public string gameSceneName = "GameScene";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // Метод выхода
    public void QuitGame()
    {
        Debug.Log("Выход из игры..."); // Для редактора
        Application.Quit();            // Для билда
    }

   

}
