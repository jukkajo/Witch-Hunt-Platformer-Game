using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickHealth : MonoBehaviour
{
    public Vector3 itemRotationSpeed = new Vector3(0, 1, 0);
    public int increaseBy = 15;
    public AudioClip pickUpSound;
    public float volumeLevel = 0.75f;
    //Animator playerAnimator

    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageElement element = collision.GetComponent<DamageElement>();
        if (element) {
           element.RestoreHealth(increaseBy);
        }
        
        //GameObject playerObject = GameObject.FindWithTag("Player");
        //if (playerObject != null)
        //{
           //playerAnimator = playerObject.GetComponent<Animator>();
           AudioSource.PlayClipAtPoint(pickUpSound, gameObject.transform.position, volumeLevel);
        //}
        Destroy(gameObject);
    }
    
    private void Update()
    {
        transform.Rotate(itemRotationSpeed * Time.deltaTime);
    }
}
