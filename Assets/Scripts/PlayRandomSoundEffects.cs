using UnityEngine;

public class PlayRandomSoundEffects : MonoBehaviour
{
    public AudioClip[] soundEffects;
    public AudioSource audioSource;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (soundEffects.Length > 0)
            {
                int randomIndex = Random.Range(0, soundEffects.Length);
                audioSource.pitch = 0.8f;
                audioSource.PlayOneShot(soundEffects[randomIndex]);
            }
        }
    }
}

