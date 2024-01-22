using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damagePerAttack = 8;
    Collider2D attackCollider;
    
    public Vector2 knock = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        DamageElement element = collision.GetComponent<DamageElement>();
        if (element != null) {
          
            Vector2 moveBackwards = transform.parent.localScale.x > 0 ? knock : new Vector2(-knock.x, knock.y);
             
            bool gotHit = element.Hit(damagePerAttack, moveBackwards);
            if (gotHit) {
                Debug.Log(collision.name + " hit with " + damagePerAttack);
            }
        }
    
    }
}
