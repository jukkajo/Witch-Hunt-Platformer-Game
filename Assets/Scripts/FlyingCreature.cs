using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCreature : MonoBehaviour
{
    public DetectionArea area;
    Animator animator;
    Rigidbody2D rb;
    DamageElement element;
    public float flySpeed = 3f;
    int waypointNumber = 0;
    Transform nextWaypoint;     
    public bool _hasTarget = false;
    public List<Transform> travelWaypoints;
    public float waypointDetectionAccuracy = 0.1f;
    public Collider2D fallCollider;

    public bool AllowMovement {
       get {
           return animator.GetBool(AnimationStrings.allowMovement); 
       }    
    }

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        element = GetComponent<DamageElement>();
        ChangeDirection();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        nextWaypoint = travelWaypoints[waypointNumber];   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HasTarget = area.detectedColliders.Count > 0;
        if (element.IsAlive) {
            if (AllowMovement) {
                Flight();   
            } else {
                rb.velocity = Vector3.zero;
            }
        } else {
           // Creature dead -> fall to ground
           rb.gravityScale = 2.2f;
        }
    }
    
    private void Flight() {
        Vector2 nextWaypointDirection = (nextWaypoint.position - transform.position).normalized;

        // Check if we already are at the waypoint
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        
        rb.velocity = nextWaypointDirection * flySpeed;
        ChangeDirection();
        
        // If in acceptable distance
        if (distance <= waypointDetectionAccuracy) {
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
    if (rb.velocity.x > 0)
    {
        // Moving left, flip to face left
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    else if (rb.velocity.x < 0)
    {
        // Moving right, flip to face right
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}

    
    public void deathSequence()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        fallCollider.enabled = true;
        
    }
}
