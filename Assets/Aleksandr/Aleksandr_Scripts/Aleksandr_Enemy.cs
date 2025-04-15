using UnityEngine;
using UnityEngine.AI;

public class Aleksandr_Enemy : MonoBehaviour, IDamageableAM
{
    public float health = 100.0f;

    private NavMeshAgent agent;
    private float moveTimer;
    public float moveInterval = 3f;
    public float wanderRadius = 10f;

    public AudioClip hitSound; // 🎵 СЮДА КЛИП
    private AudioSource audioSource;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moveTimer = moveInterval;

        // Убедитесь, что есть коллайдер
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        // Настраиваем аудио
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0f)
        {
            MoveToRandomPosition();
            moveTimer = moveInterval;
        }
    }

    void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        // 🎧 Играем звук урона
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        Debug.Log("Enemy health is: " + health);

        if (health <= 0)
        {
            Aleksandr_GameManager gameManager = FindAnyObjectByType<Aleksandr_GameManager>();
            if (gameManager != null)
            {
                gameManager.AddKill();
            }

            Destroy(gameObject);
        }
    }
}
