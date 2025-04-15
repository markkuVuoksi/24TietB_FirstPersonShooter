using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    private int round = 1; // Bắt đầu từ round 1
    private int enemiesAlive = 0; // Enemy còn sống trong round hiện tại
    private int enemiesKilled = 0; // Enemy đã giết trong round hiện tại
    private float spawnDelay = 0.5f; // Độ trễ giữa mỗi lần spawn
    private float groundSize = 40f;
    private float minDistanceFromPlayer = 5f;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(StartNewRound());
    }

    public void EnemyDied()
    {
        enemiesKilled++;
        enemiesAlive--;

        // Nếu giết hết tất cả enemy trong round, bắt đầu round mới
        if (enemiesAlive <= 0)
        {
            StartCoroutine(StartNewRound());
        }
    }

    private IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(1.0f); // Đợi 1 giây trước khi bắt đầu round mới

        enemiesKilled = 0;
        enemiesAlive = 0;

        int enemiesToSpawn = round + 1; // Mỗi round spawn nhiều hơn 1 enemy

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }

        // Mỗi 5 round spawn thêm boss
        if (round % 5 == 0)
        {
            SpawnBoss();
        }

        round++;
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("EnemyPrefab chưa được gán!");
            return;
        }

        Vector3 spawnPosition = GetValidSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemiesAlive++;
        Debug.Log("Spawned enemy at " + spawnPosition);
    }

    private void SpawnBoss()
    {
        if (bossPrefab == null)
        {
            Debug.LogError("BossPrefab chưa được gán!");
            return;
        }

        Vector3 spawnPosition = GetValidSpawnPosition();
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Spawned Boss at " + spawnPosition);
    }

    private Vector3 GetValidSpawnPosition()
    {
        int maxAttempts = 10;
        int attempts = 0;
        Vector3 spawnPosition;

        do
        {
            float randomX = Random.Range(-groundSize / 2, groundSize / 2);
            float randomZ = Random.Range(-groundSize / 2, groundSize / 2);
            spawnPosition = new Vector3(randomX, 0, randomZ);
            attempts++;
        }
        while (player != null && Vector3.Distance(spawnPosition, player.position) < minDistanceFromPlayer && attempts < maxAttempts);

        return spawnPosition;
    }
}
