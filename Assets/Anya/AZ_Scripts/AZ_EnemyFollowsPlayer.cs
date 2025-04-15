using UnityEngine;
using UnityEngine.AI;

public class AZ_EnemyFollowsPlayer : MonoBehaviour
{
        public Transform player;           // Reference to the player's Transform
        private NavMeshAgent agent;        // NavMeshAgent for pathfinding

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();  // Get the NavMeshAgent component
        }

        void Update()
        {
            if (agent != null && player != null)
            {
                agent.SetDestination(player.position); // Move the enemy towards the player
            }
        }
    }


