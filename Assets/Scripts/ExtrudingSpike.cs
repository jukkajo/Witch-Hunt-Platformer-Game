using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtrudingSpike : MonoBehaviour
{

    public Vector2 moveUp;
    private GameObject player;
    DamageElement element;
    void Start()
    {
        player = GameObject.Find("Player");
        element = player.GetComponent<DamageElement>();
    }
    
    private void OnTriggerEnter(Collider other)
    {

       if (other.CompareTag("Player"))
       {
            Debug.Log("PlaCollision");
            element.Hit(40, moveUp);
       }
    }
}
