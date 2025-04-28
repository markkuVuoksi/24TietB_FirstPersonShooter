using UnityEngine;
using UnityEngine.AI;

public class JA_Enemy_AI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f;
    public float fleeDistance = 15f;
    public Transform[] patrolPoints;
    public float patrolWaitTime = 2f;

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private float patrolTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                player = p.transform;
            }
        }

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < detectionRadius)
            {
                Flee();
                return;
            }
        }
        Patrol();
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
                patrolTimer = 0f;
            }
        }
    }

    void Flee()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized;

        Vector3 fleeDestination = transform.position + fleeDirection * fleeDistance;
        agent.SetDestination(fleeDestination);
    }
}
