using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(DamageElement))]
public class PlayerController : MonoBehaviour
{
    public float jumpAction = 8f;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float idleSpeed = 0f;
    public float airWalkSpeed = 1f;
    public float acceleration = 10f;
    private bool isLadder;
    TouchingDirections touchingDirections;
    DamageElement damageElement;
    HealthBar healthBar;
    Rigidbody2D rb;
    Animator animator;
    
    public float movementSpeed {
        get {
                if (AllowMovement) {
                
                if (InMovement && !touchingDirections.OnWall) {
                    if (touchingDirections.Surfaced) {
                        if (Running) {
                            return runSpeed;
                        } else {
                            return walkSpeed;
                        }
                    } else {
                        return airWalkSpeed;
                    }

                    
                } else {
                    return idleSpeed;
                }
                
                } else {
                
                    return idleSpeed;
                }

        }
    }
    
    Vector2 moveInput;
    
    [SerializeField]
    private bool _inMovement = false; 

    public bool InMovement {
        get {
            return _inMovement;
        }
        private set {
            _inMovement = value; 
            animator.SetBool(AnimationStrings.inMovement, value);
        }
        
    }
    
    //TODO: FInish climbing animation
    [SerializeField]
    private bool _climbing = false;
    public bool Climbing
    {
        get { return _climbing; }
        private set
        {
            _climbing = value;
            animator.SetBool(AnimationStrings.climbing, value);
        }
    }

    [SerializeField]
    private bool _running = false;
    
    private bool Running {
        get {
            return _running;
        }
        
        set {
            _running = value;
            animator.SetBool(AnimationStrings.running, value);
        }
    }

    public bool _FacingRight = true;
    public bool FacingRight {
        get {
            return _FacingRight;
        }
        private set {
        
            if(_FacingRight != value) {
                // Changing facing direction via multiplying x by -1
                // Note to myself: affects also child objects
                transform.localScale *= new Vector2(-1, 1);
            }
            _FacingRight = value;
        }
    }

    public bool AllowMovement {
        get {
            return animator.GetBool(AnimationStrings.allowMovement);
        }
    }
    
    public bool IsAlive {
        get { return animator.GetBool(AnimationStrings.isAlive); }

    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageElement = GetComponent<DamageElement>();
        
        /*------- One entity -------- */
        GameObject healthBarObject = GameObject.FindWithTag("HealthBarPlayer");
        if (healthBarObject != null) {
            HealthBar healthBarComponent = healthBarObject.GetComponent<HealthBar>();
            if (healthBarComponent != null)
            {
                healthBar = healthBarComponent;
            } else {
                Debug.LogError("HealthBar component not found on the GameObject with tag 'HealthBarPlayer'");
            }
         }
        /*----------------------------*/
        
        // Set initial values
        animator.SetBool(AnimationStrings.inMovement, false);
        animator.SetBool(AnimationStrings.running, false);
    }

    private void FixedUpdate()
    {
        if (!damageElement.HaltVelocity)
        {
            float targetVelocityX = moveInput.x * movementSpeed;
            float accelerationX = acceleration * Time.fixedDeltaTime;

            if (touchingDirections.Surfaced)
            {
                rb.velocity = Vector2.MoveTowards(rb.velocity, new Vector2(targetVelocityX, rb.velocity.y), accelerationX);
            }
            else
            {
                rb.velocity = Vector2.MoveTowards(rb.velocity, new Vector2(targetVelocityX * 2, rb.velocity.y), accelerationX);
            }

            animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        }

        if (isLadder)
        {
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(rb.velocity.x, verticalInput * 5);
            rb.velocity = climbVelocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            rb.gravityScale = 1;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive) {
            InMovement = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        } else {
            InMovement = false;
        }

    }
    
    private void SetFacingDirection(Vector2 moveInput) {
        if (moveInput.x > 0 && !FacingRight) {
            FacingRight = true;
        } else if (moveInput.x < 0 && FacingRight) {
            FacingRight = false;
        }
        
    }
    
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started) {
            Running = true;
        } else if (context.canceled) {
            Running = false;
        }
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.Surfaced && AllowMovement) {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpAction);
        } else if (context.canceled) {
            Running = false;
        }
        
    }
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started) {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }

    public void OnDistanceAttack(InputAction.CallbackContext context)
    {
        if (context.started) {
            animator.SetTrigger(AnimationStrings.distanceAttack);
        }
    }
    

    public void OnHit(int reduceByNumber, Vector2 moveBackwards) {
        if (healthBar != null) {
            healthBar.spriteChange();
        }
        rb.velocity = new Vector2(moveBackwards.x, rb.velocity.y + moveBackwards.y);
    }
}
