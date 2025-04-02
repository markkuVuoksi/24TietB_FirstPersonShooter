using UnityEngine;
using UnityEngine.UI; // For UI elements
using TMPro;
using System.Collections;
using System.Threading.Tasks;

public class SL_EnemyManager : MonoBehaviour
{
    public static SL_EnemyManager Instance { get; private set; }
    private int enemyCount = 0;

    public float gameTime = 5.0f; // Set time limit (60 seconds)
    public TextMeshProUGUI timerText; // Assign in Inspector (optional)
    public GameObject gameOverPanel; // UI panel to show when the game is over
    public GameObject victoryPanel; // UI panel to show when the game is over
    public bool timerIsRunning = false;
    public bool isGameEnd = false;
    public GameObject gameOverText;
    public GameObject winGameText;
    public TextMeshProUGUI enemyCountText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    void Start()
    {
        gameOverText.SetActive(false);  // Hide at the start
        winGameText.SetActive(false);
    }

    void Update()
    {
        if (gameTime > 0 && !isGameEnd)
        {
            gameTime -= Time.deltaTime; // Decrease time

            // Update UI timer text 
            if (timerText != null)
            {
                timerText.text = "Time left: " + Mathf.Ceil(gameTime).ToString() + " s";
            }

            // Check if time is up
            if (gameTime <= 0)
            {
                GameOver();
                isGameEnd = true;

                // Show the mouse cursor
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None; // Allow the cursor to move freely

            }
        }

        // Update enemies count text 
        if (enemyCountText != null)
        {
            enemyCountText.text = "Enemies : " + enemyCount;
        }
    }

    public void RegisterEnemy()
    {
        enemyCount++;
    }

    public void UnregisterEnemy()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            Debug.Log("All enemies are destroyed.");

            isGameEnd = true;
            Victory();

            // Show the mouse cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Allow the cursor to move freely

        }
    }

    async void GameOver()
    {
        //Time.timeScale = 0f; // Stops physics-based movement
        gameOverText.SetActive(true);  // Show the UI
        await Task.Delay(2000); // 3000ms = 3 seconds
        if (gameOverPanel != null)
        {
            gameOverText.SetActive(false);  // Show the UI
            gameOverPanel.SetActive(true); // Show Game Over UI
        }
        //StartCoroutine(ShowGameOverPanel());

    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(2.1f);

    }

    async void Victory()
    {
        //Time.timeScale = 0f; // Stops physics-based movement
        if (victoryPanel != null)
        {
            winGameText.SetActive(true);  // Show the UI
        }
        
        await Task.Delay(2000); // 3000ms = 3 seconds
        if (victoryPanel != null)
        {
            winGameText.SetActive(false);  // Show the UI
            victoryPanel.SetActive(true); // Show Game Over UI
        }

        Debug.Log("Victory.");
    }
}
