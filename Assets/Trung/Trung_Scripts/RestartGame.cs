using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartCurrentScene()
    {
        Time.timeScale = 1f; // Đảm bảo game không bị pause
        Cursor.lockState = CursorLockMode.Locked; // Ẩn và khóa chuột lại như ban đầu
        Cursor.visible = false;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
