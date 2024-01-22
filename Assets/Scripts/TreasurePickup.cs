using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePickup : MonoBehaviour
{
    public Vector3 itemRotationSpeed = new Vector3(0, 1, 0);
    public int increaseBy = 4;
    public AudioClip pickUpSound;
    public float volumeLevel = 0.75f;
    //Animator playerAnimator

    void OnTriggerEnter2D(Collider2D collision)
    {
        PointsElement element = collision.GetComponent<PointsElement>();
        
        if (element) {
           element.AddToPoints(increaseBy);
        }
        
        AudioSource.PlayClipAtPoint(pickUpSound, gameObject.transform.position, volumeLevel);
        Destroy(gameObject);
    }
    
    private void Update()
    {
        transform.Rotate(itemRotationSpeed * Time.deltaTime);
    }
}
