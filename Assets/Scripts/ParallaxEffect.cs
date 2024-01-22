using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    Vector2 startingPosition;
    float startZ;

    float depthDistance => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (depthDistance > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(depthDistance / clippingPlane);

    // "=>" = Updating with every frame
    Vector2 camMovement => (Vector2)cam.transform.position - startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMovement * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startZ);
    }
}
