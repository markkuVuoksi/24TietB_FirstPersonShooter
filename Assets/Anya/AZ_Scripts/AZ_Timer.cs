using UnityEngine;
using TMPro;


public class AZ_Timer : MonoBehaviour
{
    public float totalTime = 60f; // Время в секундах
    private float currentTime;

    public TextMeshProUGUI timerText;
    public bool isRunning = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0, totalTime);

        UpdateTimerUI();

        if (currentTime <= 0)
        {
            OnTimerEnd();
        }

    }
    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }

    void OnTimerEnd()
    {
        isRunning = false;
        Debug.Log("⏰ Время вышло!");
        // Тут можешь вызывать GameManager.Instance.Lose(); или другое
        AZ_GameManager.Instance.Lose();
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public float GetTimeLeft()
    {
        return currentTime;
    }
}
