using UnityEngine;

public class SL_PauseManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pausePanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // // Press Escape or P to toggle pause
        // if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        // {
        //     if (isPaused)
        //         ResumeGame();
        //     else
        //         PauseGame();
        // }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;   // Stop all time-based activity
        isPaused = true;

        // Show the mouse cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Allow the cursor to move freely

        // Optionally show pause UI here
        pausePanel.SetActive(true);

        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;   // Resume time

        isPaused = false;
        // Show the mouse cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor in game mode

        // Optionally hide pause UI here
        pausePanel.SetActive(false);

        
    }
}
