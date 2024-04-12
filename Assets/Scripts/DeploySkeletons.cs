using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeploySkeletons : MonoBehaviour
{
     
    private void OnTriggerEnter2D(Collider2D detected) {
        if(detected.tag == "Player") {
            
            GameObject targetObject = GameObject.FindWithTag("Skeleton");
            GameObject targetObject2 = GameObject.FindWithTag("Skeleton2");

            Rigidbody2D targetRigidbody = targetObject.GetComponent<Rigidbody2D>();
            targetRigidbody.gravityScale = 1f;
            Rigidbody2D targetRigidbody2 = targetObject2.GetComponent<Rigidbody2D>();
            targetRigidbody2.gravityScale = 1f;
            
            /*   
            Component[] components = targetObject.GetComponents<Component>();

            foreach (Component component in components)
            {
                Debug.Log("Component Type: " + component.GetType());
            }
            */
            treeCreature script1 = targetObject.GetComponent<treeCreature>(); 
            treeCreature script2 = targetObject2.GetComponent<treeCreature>(); 
            script1.walkSpeed = 2f;
            script2.walkSpeed = 2f;
            Destroy(gameObject);
        }
    }
}
