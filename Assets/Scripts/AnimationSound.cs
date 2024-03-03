using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip effect;
    private float parentXPosition;
    public float hearAreaMargin = 6f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        parentXPosition = transform.position.x;
    }

    void PlayEffect()
    {
    
        // Player position changes constantly, so had to get it on every iteration
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float playerXPosition = player.transform.position.x;
        Debug.Log("Player-pos:  " + playerXPosition + "  Worm-pos:  " + parentXPosition);
            
        if (audioSource != null && !audioSource.isPlaying) {
            
            if ((parentXPosition - hearAreaMargin) < playerXPosition && playerXPosition < (parentXPosition + hearAreaMargin))
            {
                audioSource.PlayOneShot(effect);
            }
        }
    }

}

