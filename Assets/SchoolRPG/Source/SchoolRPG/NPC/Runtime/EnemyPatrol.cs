using UnityEngine;
using UnityEngine.AI; // Required for NavMeshAgent

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;      // Array of waypoints for patrolling
    public float speed = 2f;           // Speed of the enemy while patrolling
    public float chaseSpeed = 4f;      // Speed of the enemy while chasing the player
    public float detectionRange = 5f;  // Distance at which the enemy detects the player
    public Transform player;           // Reference to the player's transform

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;        // NavMeshAgent for pathfinding
    private bool isChasing = false;    // Flag to check if the enemy is chasing the player

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;  // Set initial patrol speed
        GoToNextWaypoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            // Start chasing the player
            isChasing = true;
            agent.speed = chaseSpeed;  // Increase speed for chasing
            ChasePlayer();
        }
        else
        {
            // Stop chasing and resume patrol
            isChasing = false;
            agent.speed = speed;  // Reset speed to patrol speed
            Patrol();
        }
    }

    void Patrol()
    {
        // If not chasing and reached the current waypoint, go to the next one
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }
    }

    void GoToNextWaypoint()
    {
        // Loop through waypoints
        if (waypoints.Length == 0) return;
        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;  // Move to next waypoint, loop back to start
    }

    void ChasePlayer()
    {
        // Set the player's position as the destination for the NavMeshAgent
        agent.destination = player.position;
    }
}
