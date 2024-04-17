using UnityEngine;

public class BossFightEvent : MonoBehaviour
{
    public AudioClip evilLaughClip; // Evil laugh audio clip
    public AudioClip bossMusic; // Boss fight music audio clip
    public AudioSource musicSource; // Reference to the AudioSource playing the level music

    private bool eventTriggered = false; // Flag to ensure the event is triggered only once

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
                musicSource.volume = 0.5f;
                musicSource.Play();
            }

            eventTriggered = true; // Set the flag to true to prevent multiple triggers
        }
    }
}