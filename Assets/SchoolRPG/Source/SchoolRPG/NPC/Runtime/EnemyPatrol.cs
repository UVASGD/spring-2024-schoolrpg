using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;      // Array of waypoints for patrolling
    public float speed = 2f;           // Speed of the enemy while patrolling
    public float chaseSpeed = 4f;      // Speed of the enemy while chasing the player
    public float detectionRange = 10f;  // Maximum distance for detecting the player
    public float fieldOfViewAngle = 90f; // Field of view angle for line of sight detection
    public float searchDuration = 3f;  // How long the enemy waits at the last known player location
    public Transform player;           // Reference to the player's transform
    public LayerMask ignoreLayers;     // Layers to ignore (e.g., ground tilemap)

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;        // NavMeshAgent for pathfinding
    private bool isChasing = false;    // Flag to check if the enemy is chasing the player
    private bool isSearching = false;  // Flag to check if the enemy is searching the player
    private Vector3 lastKnownPlayerPosition; // Last known position of the player
    private float searchTimer = 0f;    // Timer to control search duration
    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component
    private Vector3 previousPosition;      // To track the enemy's movement direction
    private bool inSightBubble = false; // seeing the player in sight bubble collider
    private float lostSightGracePeriod = 2f;
    private float timeSinceLastSeen = 0f;

    private enum EnemyState { Patrolling, Chasing, Searching };
    private EnemyState currentState = EnemyState.Patrolling; // Default state is Patrolling

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        agent.speed = speed;
        previousPosition = transform.position;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        GoToNextWaypoint();
    }

    void Update()
    {
        

        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                //Debug.Log("Patrolling");
                if (IsPlayerInLineOfSight())
                {
                    StartChasing();
                }
                break;

            case EnemyState.Chasing:
                ChasePlayer();
                //Debug.Log("Chasing");
                timeSinceLastSeen += Time.deltaTime;
                if (!IsPlayerInLineOfSight() && timeSinceLastSeen > lostSightGracePeriod)
                {
                    StartSearching();
                }
                break;

            case EnemyState.Searching:
                //Debug.Log("Searching");
                timeSinceLastSeen = 0f;
                SearchForPlayer();
                break;
        }

        // Flip the sprite based on movement direction
        FlipSprite();
        previousPosition = transform.position;
    }

    void Patrol()
    {
        if (isChasing) return; // Exit waypoint logic if chasing

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }
    }

    void GoToNextWaypoint()
    {
        if (isChasing) return; // Exit waypoint logic if chasing
        if (waypoints.Length == 0) return;
        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void StartChasing()
    {
        isChasing = true;
        currentState = EnemyState.Chasing;
        agent.speed = chaseSpeed;
    }

    void ChasePlayer()
    {
        Vector3 playerPosition = player.position;
        if (Vector3.Distance(agent.destination, playerPosition) > 0.5f)
        {
            lastKnownPlayerPosition = player.position;
            agent.destination = playerPosition;
        }
    }

    void StartSearching()
    {
        if (Vector3.Distance(transform.position, player.position) < 2f)  // Close enough to the player
        {
            StartChasing();  // Stay in chase mode
            return;
        }
        isChasing = false;
        isSearching = true;
        currentState = EnemyState.Searching;
        agent.destination = lastKnownPlayerPosition;
        searchTimer = searchDuration;
    }

    void SearchForPlayer()
    {
        if (Vector3.Distance(transform.position, lastKnownPlayerPosition) < 1f)
        {
            // Start the search timer
            searchTimer -= Time.deltaTime;
            //Debug.Log(searchTimer);

            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            // If the player is in line of sight again (now effectively using 360 FOV), go back to chasing
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, ~ignoreLayers);

            if (hit.collider != null && hit.collider.transform == player)
            {
                Debug.Log("fsdsa");
                StartChasing();
            }
            else if (searchTimer <= 0f)
            {
                // After the search time expires, return to patrolling
                isSearching = false;
                currentState = EnemyState.Patrolling;
                agent.speed = speed;
                GoToNextWaypoint();
            }
        }
    }

    bool IsPlayerInLineOfSight()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            Vector2 forward = (transform.position - previousPosition).normalized;
            float angleToPlayer = Vector2.Angle(forward, directionToPlayer);
            //Debug.Log("In range");

            // Draw debug rays for the field of view
            Debug.DrawRay(transform.position, forward * detectionRange, Color.green); // Forward FOV line
            Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, fieldOfViewAngle / 2f) * forward * detectionRange, Color.blue); // Right FOV boundary
            Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -fieldOfViewAngle / 2f) * forward * detectionRange, Color.blue); // Left FOV boundary

            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, ~ignoreLayers);
            if (angleToPlayer < fieldOfViewAngle / 2f || inSightBubble)
            {
                // Perform a raycast, ignoring specified layers
                
                // Draw the ray towards the player (angleToPlayer vector)
                Debug.DrawRay(transform.position, directionToPlayer * detectionRange, Color.red);
                if (hit.collider != null)
                {
                    Debug.Log($"Raycast hit: {hit.collider.gameObject.name}, Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
                }
                if (hit.collider != null && hit.collider.transform == player)// the ifs handle player within sight bubble in case player is right behind them but also accounts for LOS
                {
                    return true;  // Player detected
                }
            }
        }
        return false;
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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.transform == player)
        {
            inSightBubble = true;
        }
        else
        {
            inSightBubble = false;
        }
    }
}
