using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchLocation;
    public GameObject projectilePref;
    //public float rotationSpeed = 720;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePref, launchLocation.position, Quaternion.identity);

        // Apply initial scale based on launcher's scale
        Vector3 scale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            scale.x * transform.localScale.x > 0 ? 1 : -1,
            scale.y,
            scale.z
        );


        //projectile.GetComponent<Rigidbody2D>().angularVelocity = -rotationSpeed * (transform.localScale.x > 0 ? 1 : -1);
    }
}

