using UnityEngine;

public class FlyingObjectGroundHit : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {

            Debug.Log("Collision with Tilemap!");
            animator.SetBool("isAlive", false);
            Destroy(transform.parent.gameObject);
        }
    }
}

