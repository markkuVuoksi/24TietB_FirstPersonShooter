using UnityEngine;
using UnityEngine.AI;
public class SL_EnemyPatrol : MonoBehaviour
{
     public Transform[] waypoints;  // Array of patrol points
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        // If the enemy is close to the waypoint, move to the next
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            NextWaypoint();
        }
    }

    void NextWaypoint()
    {
        if (waypoints.Length == 0) return;

        // Move to the next waypoint in a loop
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}
