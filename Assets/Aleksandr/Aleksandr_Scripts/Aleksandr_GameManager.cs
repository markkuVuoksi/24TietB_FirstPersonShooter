using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Aleksandr_GameManager : MonoBehaviour
{
    public int enemiesKilled = 0;
    public int killTarget = 5;
    public float timeLimit = 120f;
    private float timer;

    public Text timerText;
    public GameObject endGamePanel;   // Панель затемнения
    public Text endText;              // Текст "Победа" / "Поражение"
    public Button restartButton;      // Кнопка перезапуска

    private bool gameEnded = false;

    void Start()
    {
        Debug.Log("Game started");
        timer = timeLimit;
        timerText.text = $"Оставшееся время: {timer:F1}";

        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);
            Debug.Log("End panel скрыта");
        }

        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners(); // На всякий случай убираем старые
            restartButton.onClick.AddListener(RestartGame);
            Debug.Log("Слушатель добавлен к кнопке перезапуска");
        }

        Time.timeScale = 1f; // На случай если была пауза ранее
    }

    void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;
        timerText.text = $"Оставшееся время: {timer:F1}";

        if (enemiesKilled >= killTarget)
        {
            Debug.Log("Условие победы выполнено");
            Win();
        }
        else if (timer <= 0f)
        {
            Debug.Log("Время вышло");
            Lose();
        }
    }

    public void AddKill()
    {
        if (gameEnded) return;
        enemiesKilled++;
        Debug.Log($"Убит враг. Всего: {enemiesKilled}");
    }

    void Win()
    {
        gameEnded = true;
        ShowEndPanel("🎉 Победа!");
    }

    void Lose()
    {
        gameEnded = true;
        ShowEndPanel("💀 Поражение!");
    }

    void ShowEndPanel(string message)
    {
        if (endGamePanel != null)
            endGamePanel.SetActive(true);

        if (endText != null)
            endText.text = message;

        Debug.Log($"Показан экран завершения игры: {message}");
    }

    public void RestartGame()
    {
        Debug.Log("Кнопка перезапуска нажата — загружается текущая сцена");
        Time.timeScale = 1f;

        if (endGamePanel != null)
            endGamePanel.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
