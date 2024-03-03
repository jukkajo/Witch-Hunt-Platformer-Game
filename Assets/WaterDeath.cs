using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeath : MonoBehaviour
{
    public Vector2 checkBoxSize = new Vector2(2f, 2f);
    DamageElement element;
    public int damagePerAttack = 200;
    void Update()
    {
        // Perform a physics overlap check in a box-shaped area
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, checkBoxSize, 0f);

        foreach (Collider2D collider in colliders)
        {
            element = collider.GetComponent<DamageElement>();
            if (element) {
               bool gotHit = element.Hit(damagePerAttack, Vector2.zero);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a wireframe box in the Scene view for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, checkBoxSize);
    }
}

