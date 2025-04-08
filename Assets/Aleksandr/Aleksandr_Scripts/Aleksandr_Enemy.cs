using UnityEngine;
using UnityEngine.AI;

public class Aleksandr_Enemy : MonoBehaviour, IDamageableAM
{
    public float health = 100.0f;

    private NavMeshAgent agent;
    private float moveTimer;
    public float moveInterval = 3f;       // Интервал между сменой цели
    public float wanderRadius = 10f;      // Радиус случайного движения

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moveTimer = moveInterval;
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
        // Случайное направление от текущей позиции
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 2f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Enemy health is: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}