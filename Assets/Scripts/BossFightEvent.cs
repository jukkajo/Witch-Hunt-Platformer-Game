using UnityEngine;

public class BossFightEvent : MonoBehaviour
{
    public AudioClip evilLaughClip;
    public AudioClip bossMusic;
    public AudioSource musicSource;

    private bool eventTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !eventTriggered)
        {
            // Play evil laugh audio clip
            if (evilLaughClip != null)
            {
                AudioSource.PlayClipAtPoint(evilLaughClip, transform.position);
            }

            // Start boss fight music
            if (bossMusic != null && musicSource != null)
            {
                // Stop the current music
                musicSource.Stop();

                // Play the boss fight music
                musicSource.clip = bossMusic;
                musicSource.loop = true;
                musicSource.volume = 0.2f;
                musicSource.Play();
            }

            eventTriggered = true; // Set the flag to true to prevent multiple triggers
        }
    }
}