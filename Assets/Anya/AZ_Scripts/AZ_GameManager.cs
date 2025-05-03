using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AZ_GameManager : MonoBehaviour
{

    public static AZ_GameManager Instance;

    public float timeLimit = 60f;
    private float timer;

    public GameObject winScreen;
    public GameObject loseScreen;
    public TextMeshProUGUI timerText;

    private bool gameEnded = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = timeLimit;
    }

    void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;

        if (timerText != null)
        {
            timerText.text = $"Time: {Mathf.Ceil(timer)}";
        }

        if (timer <= 0f)
        {
            CheckWinCondition();
        }
    }

    void CheckWinCondition()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (enemies.Length == 0)
        {
            Win();
        }
        else
        {
            Lose();
        }
    }

    public void OnEnemyKilled()
    {
        // Если врагов больше нет И ещё остался таймер — победа
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (enemies.Length == 0 && timer > 0f)
        {
            Win();
        }
    }

    public void Win()
    {
        if (gameEnded) return;

        gameEnded = true;
        Time.timeScale = 0f;
        winScreen.SetActive(true);
    }

    public void Lose()
    {
        if (gameEnded) return;

        gameEnded = true;
        Time.timeScale = 0f;
        loseScreen.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
