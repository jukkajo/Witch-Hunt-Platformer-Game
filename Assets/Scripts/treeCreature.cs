using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(DamageElement))]
public class treeCreature : MonoBehaviour
{
    public float walkSpeed = 2f; 
    Rigidbody2D rb;
    DamageElement damageElement;
    
    public enum WalkingDirection { Right, Left }
    private  WalkingDirection _walkDirection;
    private  Vector2 walkDirectionVector = Vector2.right;
    TouchingDirections touchingDirections;
    public DetectionArea attackArea;
    public DetectionArea groundDetectionArea;
    Animator animator;
    private float walkingSpeedReducerRate = 0.02f;
    HealthBar healthBar;
    public string healthBarName = "TODO: Change";
    
    public  WalkingDirection WalkDirection {
        get {
            return _walkDirection;
        }
        
        set {
            if (_walkDirection != value) {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                
                if (value == WalkingDirection.Right) {
                    walkDirectionVector = Vector2.right;
                } else if (value == WalkingDirection.Left) {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }
    
    public bool _hasTarget = false;
    
    public bool HasTarget { get { return _hasTarget; } private set {
    
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    
    }
    
    public bool AllowMovement {
       get {
           return animator.GetBool(AnimationStrings.allowMovement); 
       }
    
    }
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        animator.SetBool(AnimationStrings.allowMovement, true); 
        damageElement = GetComponent<DamageElement>();
        if (healthBarName != "TODO: Change") {
            healthBar = GameObject.FindWithTag(healthBarName).GetComponent<HealthBar>();
        }
    }
    
    void Update()
    {
        if (attackArea.detectedColliders.Count != 0) {
        // Checking for (player's) capsule-collider
        foreach (Collider2D collider in attackArea.detectedColliders){
            Debug.Log(collider.GetType());
            
            if (collider.GetType() == typeof(CapsuleCollider2D))
            {
                HasTarget = true;
                break;
            } 
            
            else {
                HasTarget = false;
            }
        }
        } else {
              HasTarget = false;
        }
    }
    
    private void FixedUpdate() {
        
        if (touchingDirections.Surfaced && touchingDirections.OnWall) {

            ChangeDirection();
        } else if (touchingDirections.Surfaced && groundDetectionArea.detectedColliders.Count == 0) {
            ChangeDirection();
        }
        
        
        if(!damageElement.HaltVelocity) {
            if (AllowMovement) {
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            } else {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkingSpeedReducerRate), rb.velocity.y);
            }
        }
        
    }
    
    private void ChangeDirection() {
        if(WalkDirection == WalkingDirection.Right)
        {
            WalkDirection = WalkingDirection.Left;
        } else if (WalkDirection == WalkingDirection.Left)
        {
            WalkDirection = WalkingDirection.Right;
        } else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }
    
    public void OnHit(int reduceByNumber, Vector2 moveBackwards) {
        healthBar.spriteChange();
        rb.velocity = new Vector2(moveBackwards.x, rb.velocity.y + moveBackwards.y);
    }
}
