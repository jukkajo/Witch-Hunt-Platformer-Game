using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 25;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 moveBackwards = new Vector2(3, 2);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    void Start()
    {
        // TODO: use dynamic mode with rigidbody
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        rb.angularVelocity = -720f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageElement element = collision.GetComponent<DamageElement>();

        if(element != null)
        {

            Vector2 moveVector = transform.localScale.x > 0 ? moveBackwards : new Vector2(-moveBackwards.x, moveBackwards.y);
            bool gotHit = element.Hit(damage, moveVector);

            if (gotHit)
                Debug.Log(collision.name + " hit for " + damage);
                Destroy(gameObject);
        }
    }
}
