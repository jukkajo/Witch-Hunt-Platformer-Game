using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageElement : MonoBehaviour
{   
    // int for damage, and Vector for backwards movement
    public UnityEvent<int, Vector2> damageElementHit;
    Animator animator;
    HealthBar healthBar;

    [SerializeField]
    private int _startHealth = 100;

    [SerializeField]
    public int _health = 100;
    
    // 0.2 Seconds
    public float canNotHarmTime = 0.2f;
    private float timeAfterLastHit = 0;
    public string healthBarName = "TODO: Change";
    
    public int StartHealth { get { return _startHealth; }
        set { _startHealth = value; }
    
    }
    
    [SerializeField]
    private bool canNotHarm = false;
    
    [SerializeField]
    private bool _isAlive = true;
    
    public bool IsAlive {
        get { return _isAlive; }
        set {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }
    
    public int Health { get { return _health; }
        set {
            _health = value; 
            
            if (_health <= 0) {
                IsAlive = false;
            }
            
        
        }
    
    }

    public void RestoreHealth(int restoreBy) {
        if (IsAlive) {
            if ((Health + restoreBy) < _startHealth) {
                Health += restoreBy;
                Events.damageHealed(gameObject, restoreBy);
            } else {
                Events.damageHealed(gameObject, restoreBy);
                Health = _startHealth;
            }
            if (healthBar != null) { 
                healthBar.spriteChange();
            }
        }
    }

    private void Update() {
        if (canNotHarm) {
            if (timeAfterLastHit > canNotHarmTime) {
                canNotHarm = false;
                timeAfterLastHit = 0;
            }
            // Adding time between frames
            timeAfterLastHit += Time.deltaTime;
        }
    }
    
    private void Awake() {
        animator = GetComponent<Animator>();
        if (healthBarName != "TODO: Change") { 
           healthBar = GameObject.FindWithTag(healthBarName).GetComponent<HealthBar>();
        }
    }
    
    public bool HaltVelocity {
        get {
            return animator.GetBool(AnimationStrings.haltVelocity);
        } set {
            animator.SetBool(AnimationStrings.haltVelocity, value);
        }
    }
    
    public bool Hit(int reduceByNumber, Vector2 moveBackwards) {
       if(IsAlive && !canNotHarm) {
           Health -= reduceByNumber;
           canNotHarm = true;
           
           animator.SetTrigger(AnimationStrings.hit);
           damageElementHit?.Invoke(reduceByNumber, moveBackwards);
           
           Events.damageTaken.Invoke(gameObject, reduceByNumber);
           return true;
       }
       return false;
    }

}
