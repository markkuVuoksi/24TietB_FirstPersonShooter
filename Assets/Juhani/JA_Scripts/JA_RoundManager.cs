using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JA_RoundManager : MonoBehaviour
{
    public static JA_RoundManager Instance;

    [Header("Player reference")]
    public GameObject player;

    [Header("Win/Lose UI")]
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public Button replayButtonWin;
    public Button replayButtonLose;

    private int enemiesAlive;

    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        winPanel?.SetActive(false);
        gameOverPanel?.SetActive(false);
        Debug.Log("Enemies alive: " + enemiesAlive);

        replayButtonWin?.onClick.AddListener(PlayAgain);
        replayButtonLose?.onClick.AddListener(PlayAgain);
    }

    public void EnemyKilled()
    {
        enemiesAlive--;
        Debug.Log("Enemy killed -> left: " + enemiesAlive);

        if (enemiesAlive <= 0)
            ShowWin();
    }

    public void ShowWin()
    {
        PauseGame();
        if (winPanel)
            winPanel.SetActive(true);
    }

    public void ShowGameOver()
    {
        PauseGame();
        if (gameOverPanel)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("<color=yellow>GameOverPanel aktivointiin!</color>");
        }

        if (replayButtonLose)
        {
           // Debug.Log("<color=lime>ReplayButtonLose aktiivinen? </color>" + replayButtonLose.activeSelf);
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (player != null)
        {
            var move = player.GetComponent<JA_playerMovement>();
            if (move) move.enabled = false;

            var gun = player.GetComponent<JA_Hitscan>();
            if (gun) gun.enabled = false;
        }
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
