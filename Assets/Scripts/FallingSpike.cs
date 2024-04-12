using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float resetTime = 2f;
    public AudioClip impactSound;
    public ParticleSystem impactEffect;

    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isFalling)
        {
            rb.velocity = new Vector2(0, -fallSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            if (impactSound != null)
            {
                AudioSource.PlayClipAtPoint(impactSound, transform.position);
            }

            if (impactEffect != null)
            {
                Vector3 collisionPosition = other.ClosestPoint(transform.position);
                impactEffect.transform.position = collisionPosition;
                impactEffect.Play();
            }
            Invoke("ResetSpike", resetTime);
            rb.velocity = Vector2.zero;
        }
    }

    void ResetSpike()
    {
        transform.position = initialPosition;
        isFalling = false;
    }

    public void StartFalling()
    {
        isFalling = true;
    }
}
