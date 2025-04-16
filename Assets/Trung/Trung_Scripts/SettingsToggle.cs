using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsToggle : MonoBehaviour
{
    private GameObject pausePanel;
    private bool isPaused = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPausePanel();

        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // Ẩn khi vừa vào Trung_Scence
        }

        if (scene.name == "Trung_Scence")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1f;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Trung_Scence" && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettings();
        }
    }

    void ToggleSettings()
    {
        if (pausePanel == null)
        {
            FindPausePanel();
            if (pausePanel == null) return;
        }

        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void FindPausePanel()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Pause Panel")
            {
                pausePanel = obj;
                break;
            }
        }
    }
}
