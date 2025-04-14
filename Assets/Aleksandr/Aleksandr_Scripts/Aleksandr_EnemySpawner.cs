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
    public int maxEnemies = 5;  // Максимальное количество врагов
    private int currentEnemyCount = 0;  // Текущее количество врагов
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            // Спавним врага, если текущее количество меньше максимального
            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
                timer = 0f;
            }
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyVariants.Length == 0) return;

        // Выбираем точку спавна
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Выбираем тип врага на основе вероятности
        GameObject selectedEnemy = ChooseRandomEnemy();

        // Создаем врага
        Instantiate(selectedEnemy, spawnPoint.position, Quaternion.identity);

        // Увеличиваем счётчик врагов
        currentEnemyCount++;
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

    // Метод для вызова, когда враг уничтожен
    public void OnEnemyDestroyed()
    {
        // Уменьшаем количество активных врагов
        currentEnemyCount--;
    }
}
