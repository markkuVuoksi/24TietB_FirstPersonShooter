using UnityEngine;

public class Aleksandr_GameManager : MonoBehaviour
{
    public float timeLimit = 120f;
    public int killTarget = 5;
    public int enemiesKilled = 0;

    private float timer;
    private bool gameEnded = false;

    void Start()
    {
        Time.timeScale = 1f;
        timer = timeLimit;
    }

    void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;

        Debug.Log($"[TIMER] Time left: {timer:F1} sec | Kills: {enemiesKilled}/{killTarget}");

        if (enemiesKilled >= killTarget)
        {
            Win();
        }
        else if (timer <= 0f)
        {
            Lose();
        }
    }

    public void AddKill()
    {
        enemiesKilled++;
        Debug.Log($"[KILL] Enemy defeated! Total: {enemiesKilled}");
    }

    void Win()
    {
        gameEnded = true;
        Debug.Log("🎉 VICTORY! Target reached!");
    }

    void Lose()
    {
        gameEnded = true;
        Debug.Log("💀 DEFEAT! Time's up.");
    }
}
