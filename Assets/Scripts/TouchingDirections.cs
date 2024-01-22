using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{   
    public ContactFilter2D castFilter;
    public Collider2D touchCollider;
    
    Animator animator;
    
    public float surfaceClearance = 0.05f;
    public float wallClearance = 0.5f;
    public float ceilingClearance = 0.05f;
    
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    
    /* Since localscale is used for changing direction,
       lets utilize that
    */
    [SerializeField]
    private Vector2 checkWallSide => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    
    //--------------------------------------------------------- 
    [SerializeField]
    private bool _surfaced;
    public bool Surfaced {
        get {
            return _surfaced;
        }
        
        private set {
            _surfaced = value;
            animator.SetBool(AnimationStrings.surfaced, value);
        }
    }
    //---------------------------------------------------------
    [SerializeField]
    private bool _onWall;
    public bool OnWall {
        get {
            return _onWall;
        }
        
        private set {
            _onWall = value;
            animator.SetBool(AnimationStrings.onWall, value);
        }
    }
    //---------------------------------------------------------
    [SerializeField]
    private bool _onCeiling;
    public bool OnCeiling {
        get {
            return _onCeiling;
        }
        
        private set {
            _onCeiling = value;
            animator.SetBool(AnimationStrings.onCeiling, value);
        }
    }
    //---------------------------------------------------------
    private void Awake() {
        touchCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
      Surfaced = touchCollider.Cast(Vector2.down, castFilter, groundHits, surfaceClearance) > 0; 
      OnWall = touchCollider.Cast(checkWallSide, castFilter, wallHits, wallClearance) > 0; 
      OnCeiling = touchCollider.Cast(Vector2.up, castFilter, ceilingHits, ceilingClearance) > 0; 
    }
    
public bool IsPlayer()
{
/*
    for (int i = 0; i < groundHits.Length; i++)
    {
        if (groundHits[i].collider != null && groundHits[i].collider.CompareTag("Player"))
        {
            return true;
        }
    }
*/
    for (int i = 0; i < wallHits.Length; i++)
    {
        if (wallHits[i].collider != null && wallHits[i].collider.CompareTag("Player"))
        {
            return true;
        }
    }
/*
    for (int i = 0; i < ceilingHits.Length; i++)
    {
        if (ceilingHits[i].collider != null && ceilingHits[i].collider.CompareTag("Player"))
        {
            return true;
        }
    }
*/
    return false;
}
    
    public System.Type GetTouchColliderType()
    {
        if (Surfaced && groundHits[0].collider != null)
        {
            return groundHits[0].collider.GetType();
        }
        else if (OnWall && wallHits[0].collider != null)
        {
            return wallHits[0].collider.GetType();
        }
        else if (OnCeiling && ceilingHits[0].collider != null)
        {
            return ceilingHits[0].collider.GetType();
        }
        else
        {
            return null;
        }
    }
}
