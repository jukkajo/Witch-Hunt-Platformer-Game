using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DetectionArea area;
    Animator animator;
    Rigidbody2D rb;
    DamageElement element;
    int waypointNumber = 0;
    public List<Transform> travelWaypoints;
    public float walkSpeed = 0.5f;
    public bool _playerNearby = false;
    public float waypointDetectionAccuracy = 0.1f;
    Transform nextWaypoint;
    private AudioSource audioSource;
    
    public AudioClip speech1;
    public float volume1 = 1f;

    public void OnHit(int reduceByNumber, Vector2 moveBackwards) {
        rb.velocity = new Vector2(moveBackwards.x, rb.velocity.y + moveBackwards.y);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        element = GetComponent<DamageElement>();

        // Instantiating the AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public bool PlayerNearby
    {
        get { return _playerNearby; }
        private set
        {
            _playerNearby = value;
            animator.SetBool(AnimationStrings.playerNearby, value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nextWaypoint = travelWaypoints[waypointNumber];       
    }

void FixedUpdate()
{
    bool previousPlayerNearby = PlayerNearby; // Store the previous state

    PlayerNearby = area.detectedColliders.Count > 0;

    if (element.IsAlive)
    {
        if (!PlayerNearby)
        {
            travel();
        }
        else
        {
            // Check if the player has just entered the detection area
            if (!previousPlayerNearby)
            {
                audioSource.clip = speech1;
                audioSource.volume = volume1;
                audioSource.Play();
            }

            rb.velocity = Vector3.zero;
        }
    }
    else
    {
        Destroy(gameObject);
    }
}
    
    private void travel() {
        Vector2 nextWaypointDirection = (nextWaypoint.position - transform.position).normalized;

        // Check if we already are at the waypoint
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        ChangeDirection();
        rb.velocity = nextWaypointDirection * walkSpeed;

        // If within acceptable distance
        if (distance <= waypointDetectionAccuracy) {
            Debug.Log("Within area");
            waypointNumber += 1;
            
            // Should not be higher, but just in case :) 
            if (waypointNumber >= travelWaypoints.Count) {
                waypointNumber = 0;          
            }
            nextWaypoint = travelWaypoints[waypointNumber];
        }
    }
    
    private void ChangeDirection()
    {
        if(transform.localScale.x > 0)
        {
            // Currently facing right
            if(rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }
}

