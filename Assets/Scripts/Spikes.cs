using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class Spikes: MonoBehaviour
{
    public int spikeDamage = 100;
    public Vector2 knockBack = new Vector2(0, 0);
    public AudioClip spikeSound;
    public ParticleSystem bloodParticles;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            DamageElement element = player.GetComponent<DamageElement>();

            if (element != null)
            {
                if (spikeSound != null)
                {
                    AudioSource.PlayClipAtPoint(spikeSound, transform.position);
                }
                 if (bloodParticles != null)
                {
                    bloodParticles.transform.position = player.transform.position + new Vector3(0f, -0.8f, 0f); ;
                    bloodParticles.Play();
                }
                element.Hit(spikeDamage, knockBack);
            }
        }
    }
}