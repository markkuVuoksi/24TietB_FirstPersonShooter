using UnityEngine;

public class Aleksandr_EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyVariant
    {
        public GameObject prefab;
        public float spawnChance;  // Например, 0.5f, 0.3f, 0.2f — сумма должна быть 1
    }

    public EnemyVariant[] enemyVariants;
    public Transform[] spawnPoints;
    public float spawnInterval = 3f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyVariants.Length == 0) return;

        // Выбираем точку спавна
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Выбираем тип врага на основе вероятности
        GameObject selectedEnemy = ChooseRandomEnemy();

        Instantiate(selectedEnemy, spawnPoint.position, Quaternion.identity);
    }

    GameObject ChooseRandomEnemy()
    {
        float total = 0f;
        foreach (var e in enemyVariants) total += e.spawnChance;

        float randomPoint = Random.value * total;

        float cumulative = 0f;
        foreach (var e in enemyVariants)
        {
            cumulative += e.spawnChance;
            if (randomPoint <= cumulative)
                return e.prefab;
        }

        return enemyVariants[0].prefab; // запасной вариант
    }
}
