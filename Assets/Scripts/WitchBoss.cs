using UnityEngine;
using System.Collections;

public class WitchBoss : MonoBehaviour
{
    public Transform pointA; // Waypoint A
    public Transform pointB; // Waypoint B
    public float moveSpeed = 5f; // Speed at which the witch moves
    public float fleeDistance = 3f; // Distance at which the witch will flee from the player
    public float stoppingDistance = 10f; // Distance at which the witch will stop if the player is far enough
    public GameObject magicProjectile; // Prefab of the magic projectile
    public float magicAttackDelay = 2f; // Delay between magic attacks
    public float magicAttackRadius = 5f; // Radius of the area where the magic attack can randomly drop
    public string healthBarName = "TODO: Change";

    private Transform targetWaypoint; // Current target waypoint
    private Transform player; // Reference to the player
    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer
    private Animator animator; // Reference to the animator
    private bool isMoving = false; // Flag to track if the witch is moving
    private bool isChanneling = false; // Flag to track if the witch is channeling magic attack
    private float lastMagicAttackTime; // Time when the last magic attack occurred

    Rigidbody2D rb;
    DamageElement damageElement;
    HealthBar healthBar;


    void Start()
    {
        // Initialize target waypoint to pointA
        targetWaypoint = pointA;

        // Find the player object
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the animator component
        animator = GetComponent<Animator>();

        damageElement = GetComponent<DamageElement>();

        rb = GetComponent<Rigidbody2D>();

        if (healthBarName != "TODO: Change")
        {
            healthBar = GameObject.FindWithTag(healthBarName).GetComponent<HealthBar>();
        }
    }

    void Update()
    {
        // Face the player by mirroring the sprite horizontally
        if (player.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true; // Mirror the sprite
        }
        else
        {
            spriteRenderer.flipX = false; // Unmirror the sprite
        }

        // Check if the player is within flee distance
        if (Vector2.Distance(transform.position, player.position) < fleeDistance)
        {
            // If player is too close, flee from the player
            FleePlayer();
        }
        else if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            // If player is far enough, stop moving
            StopMoving();
        }
        else
        {
            // Move towards the target waypoint ignoring the player's position
            MoveToWaypoint();
        }

        // Check if enough time has passed since the last magic attack and if the player is close enough
        if (!isChanneling && Time.time - lastMagicAttackTime > magicAttackDelay && Vector2.Distance(transform.position, player.position) < magicAttackRadius)
        {
            // Start channeling magic attack
            StartCoroutine(ChannelMagicAttack());
        }
    }

    IEnumerator ChannelMagicAttack()
    {
        isChanneling = true;
        StopMoving(); // Stop moving while channeling
        animator.SetTrigger("channelMagic"); // Trigger animation for channeling magic attack

        // Wait for a moment to channel the attack
        yield return new WaitForSeconds(1f);

        // Calculate random position within a certain radius around player
        Vector3 attackPosition = player.position;
        attackPosition.y = 10f;

        // Instantiate magic attack prefab at calculated position
        GameObject magicAttackInstance = Instantiate(magicProjectile, attackPosition, Quaternion.identity);

        lastMagicAttackTime = Time.time; // Update last magic attack time
        yield return new WaitForSeconds(3f); // Wait for the attack to complete
        isChanneling = false; // Finished channeling
        ResumeMoving(); // Resume moving after channeling is finished
    }

    void StopMoving()
    {
        // Stop moving
        isMoving = false;
        animator.SetBool("isMoving", isMoving);
    }

    void ResumeMoving()
    {
        // Resume moving
        isMoving = true;
        animator.SetBool("isMoving", isMoving);
    }

    void MoveToWaypoint()
    {
        // Move towards the target waypoint
        if (!isChanneling) // Ensure the witch does not move while channeling
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

            // Set animation flag to indicate moving
            isMoving = true;
            animator.SetBool("isMoving", isMoving);

            // Check if reached the target waypoint
            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                // Switch to the next waypoint
                if (targetWaypoint == pointA)
                    targetWaypoint = pointB;
                else
                    targetWaypoint = pointA;
            }
        }
    }

    void FleePlayer()
    {
        if (!isChanneling)
        {
            // Calculate direction away from the player
            Vector2 fleeDirection = ((Vector2)transform.position - (Vector2)player.position).normalized;

            // Calculate the new position to flee to
            Vector2 newPosition = (Vector2)transform.position + fleeDirection * moveSpeed * Time.deltaTime;

            // Move towards the new position
            transform.position = Vector2.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void OnHit(int reduceByNumber, Vector2 moveBackwards)
    {
        healthBar.spriteChange();
        rb.velocity = new Vector2(moveBackwards.x, rb.velocity.y + moveBackwards.y);
    }
}