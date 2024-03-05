using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(BoxCollider2D))]
public class RollingStone : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rbXPosition = 0f;
    public int collisionDamage = 100;
    private float lastXPosition;
    public Vector2 knock = new Vector2(5, 4);
    private System.Type colliderType;
    public AudioClip effect;

    Vector3 rotationSpeed = new Vector3(0, 0, 90);
    TouchingDirections touchingDirections;
    Rigidbody2D rb;
    AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 0.5f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate movement when the player enters the trigger collider
            StartCoroutine(MoveStone());
        }
    }

    IEnumerator MoveStone()
    {
        audioSource.PlayOneShot(effect); 
        while (true)
        {
            if (touchingDirections.Surfaced)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                rbXPosition = rb.position.x;
                StartCoroutine(CheckPositionChange());
            }

            yield return null;
        }
    }

    IEnumerator CheckPositionChange()
    {
        lastXPosition = rb.position.x;

        yield return new WaitForSeconds(1f);

        // Exit if the object is already destroyed
        if (rb == null)
        {
            yield break;
        }

        if (Mathf.Approximately(lastXPosition, rb.position.x) && touchingDirections.OnWall)
        {
            Destroy(gameObject);
        }

        if (touchingDirections.IsPlayer())
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            DamageElement element = player.GetComponent<DamageElement>();

            if (element != null)
            {
                element.Hit(collisionDamage, knock);
                Destroy(gameObject);
            }
        }
    }
    
    private void OnDestroy()
    {
        audioSource.Stop();
    }
}

