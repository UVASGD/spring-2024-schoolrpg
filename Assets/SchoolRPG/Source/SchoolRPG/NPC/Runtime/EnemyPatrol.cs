using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;      // Array of waypoints for patrolling
    public float speed;           // Speed of the enemy while patrolling
    public float chaseSpeed;      // Speed of the enemy while chasing the player
    public float detectionRange;  // Maximum distance for detecting the player
    [Range(0, 360)]
    public float fieldOfViewAngle; // Field of view angle for line of sight detection
    public float searchDuration;  // How long the enemy waits at the last known player location
    //public Transform player;           // Reference to the player's transform
    public LayerMask ignoreLayers;     // Layers to ignore (e.g., ground tilemap)
    public LayerMask playerMask;       // Player layer to target
    public float detectionTime; // Time the enemy spends detecting
    private float detectionTimer = 0f;

    private GameObject questionMark;  // Reference to the question mark sprite
    private GameObject exclamationMark; // Reference to the exclamation mark sprite
    private Transform player;
    private int currentWaypointIndex = 0;
    private bool playerFound = false;
    private NavMeshAgent agent;        // NavMeshAgent for pathfinding
    private bool isChasing = false;    // Flag to check if the enemy is chasing the player
    private bool isSearching = false;  // Flag to check if the enemy is searching the player
    private Vector3 lastKnownPlayerPosition; // Last known position of the player
    private float searchTimer = 0f;    // Timer to control search duration
    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component
    private Vector3 previousPosition;      // To track the enemy's movement direction
    private bool inSightBubble = false; // seeing the player in sight bubble collider
    private float lostSightGracePeriod = 1f;
    private float timeSinceLastSeen = 0f;

    private enum EnemyState { Patrolling, Chasing, Searching, Detecting};
    private EnemyState currentState = EnemyState.Patrolling; // Default state is Patrolling

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = speed;
        previousPosition = transform.position;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        questionMark = transform.GetChild(0).gameObject;
        exclamationMark = transform.GetChild(1).gameObject;

        StartCoroutine(FindTargetsWithDelay(.2f));
    }

    void Update()
    {
        // for FOV debugging
        Vector2 forward = agent.velocity == Vector3.zero ? (agent.destination - transform.position).normalized : agent.velocity.normalized;
        // Draw debug rays for the field of view
        Debug.DrawRay(transform.position, forward * detectionRange, Color.green); // Forward FOV line
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, fieldOfViewAngle / 2f) * forward * detectionRange, Color.blue); // Right FOV boundary
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -fieldOfViewAngle / 2f) * forward * detectionRange, Color.blue); // Left FOV boundary

        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                if (playerFound)
                {
                    StartDetecting();
                }
                break;

            case EnemyState.Detecting:
                DetectingPlayer();
                break;

            case EnemyState.Chasing:
                ChasePlayer();
                timeSinceLastSeen += Time.deltaTime;
                if (!playerFound && timeSinceLastSeen > lostSightGracePeriod)
                {
                    timeSinceLastSeen = 0f;
                    StartSearching();
                }
                break;

            case EnemyState.Searching:                
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
        questionMark.SetActive(false);
        exclamationMark.SetActive(false);
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
        questionMark.SetActive(false);
        exclamationMark.SetActive(true);
        StartCoroutine(ScaleMark(exclamationMark, Vector3.zero, Vector3.one, 0.25f));
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
        exclamationMark.SetActive(false);
        questionMark.SetActive(true);
        StartCoroutine(ScaleMark(questionMark, Vector3.zero, Vector3.one, 0.25f));
        isChasing = false;
        isSearching = true;
        currentState = EnemyState.Searching;
        agent.destination = lastKnownPlayerPosition;
        searchTimer = searchDuration;
    }

    void StartDetecting()
    {
        currentState = EnemyState.Detecting;
        detectionTimer = detectionTime;  // Reset the detection timer
        exclamationMark.SetActive(false);
        questionMark.SetActive(true);  // Show the question mark above the enemy
        agent.isStopped = true;
        agent.ResetPath();
        StartCoroutine(ScaleMark(questionMark, Vector3.zero, Vector3.one, 0.25f));
    }

    void DetectingPlayer()
    {
        detectionTimer -= Time.deltaTime;    

        if (detectionTimer <= 0f)
        {
            questionMark.SetActive(false);
            exclamationMark.SetActive(true);
            agent.isStopped = false;
            StartChasing(); 
        }
        else if (!playerFound)  // If the player escapes during detection
        {
            questionMark.SetActive(false);
            agent.isStopped = false;
            currentState = EnemyState.Patrolling;
        }
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
                //Debug.Log("fsdsa");
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

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    // copied a lil bit from sebastian lague's tutorial where he has multiple targets, but here I only have one object, the player, as a target
    void FindVisibleTargets()
    {
        Collider2D targetsInViewRadius = Physics2D.OverlapCircle(transform.position, detectionRange, playerMask);
        // if agent is moving, use velocity. if agent is not moving, use destination
        Vector3 forward = agent.velocity == Vector3.zero ? (agent.destination - transform.position).normalized : agent.velocity.normalized;

        if (targetsInViewRadius != null)
        {
            Transform target = targetsInViewRadius.transform; // player
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(forward, dirToTarget);

            //Debug.Log($"Angle to Player: {angleToPlayer}, FOV: {fieldOfViewAngle}");

            if (angleToPlayer < fieldOfViewAngle / 2 || inSightBubble) // Check FOV or sight bubble
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToTarget, detectionRange, ~ignoreLayers);

                if (hit.collider != null && hit.collider.transform == target)
                {
                    //Debug.Log($"Player detected within FOV or sight bubble, {inSightBubble}, {angleToPlayer}");
                    playerFound = true;  // Player is detected
                }
                else
                {
                    //Debug.Log("Raycast blocked or player outside FOV.");
                    playerFound = false;  // No clear line of sight to player
                }
            }
            else
            {
                //Debug.Log("Player outside of FOV and not in sight bubble.");
                playerFound = false;
            }
        }
        else
        {
            Debug.Log("No player in view radius.");
            playerFound = false;
        }
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
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.transform == player)
        {
            inSightBubble = false;
        }
    }

    IEnumerator ScaleMark(GameObject mark, Vector3 startScale, Vector3 endScale, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            mark.transform.localScale = Vector3.Lerp(startScale, endScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        mark.transform.localScale = endScale; // Ensure it reaches the final scale
    }
}

