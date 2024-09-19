using UnityEngine;
using UnityEngine.AI; // Required for NavMeshAgent

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;      // Array of waypoints for patrolling
    public float speed = 2f;           // Speed of the enemy while patrolling
    public float chaseSpeed = 4f;      // Speed of the enemy while chasing the player
    public float detectionRange = 5f;  // Distance at which the enemy detects the player
    public Transform player;           // Reference to the player's transform
    public SpriteRenderer spriteRenderer;

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private bool isChasing = false;
    private Vector3 previousPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;  // Set initial patrol speed
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        previousPosition = transform.position;
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
        FlipSprite();
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
        agent.destination = player.position;
    }
    void FlipSprite()
    {
        Vector3 movementDirection = transform.position - previousPosition;


        if (movementDirection.x < 0)
        {
            spriteRenderer.flipX = false;  
        }
        else if (movementDirection.x > 0)
        {
            spriteRenderer.flipX = true; 
        }

        previousPosition = transform.position;
    }
}
