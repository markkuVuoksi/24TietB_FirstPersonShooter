using UnityEngine;
using UnityEngine.SceneManagement;

public class AZ_MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // ��� �����, ������� ����� ���������
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

    // ����� ������
    public void QuitGame()
    {
        Debug.Log("����� �� ����..."); // ��� ���������
        Application.Quit();            // ��� �����
    }

   

}
