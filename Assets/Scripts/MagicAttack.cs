using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    public int damage = 10; // Damage inflicted by the magic attack
    public Vector2 knockBack = new Vector2(0, 0); // Knockback applied to the player
    public AudioClip impactSound; // Sound played upon impact
    public ParticleSystem impactParticles; // Particle system played upon impact

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            DamageElement element = player.GetComponent<DamageElement>();

            if (element != null)
            {
                // Deal damage to the player
                element.Hit(damage, knockBack);

                // Play impact sound
                if (impactSound != null)
                {
                    AudioSource.PlayClipAtPoint(impactSound, transform.position);
                }

                // Play impact particles
                if (impactParticles != null)
                {
                    impactParticles.transform.position = transform.position;
                    impactParticles.Play();
                }

                // Destroy the magic attack
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Ground"))
        {
            // Destroy the magic attack if it hits the ground
            Destroy(gameObject);
        }
    }
}