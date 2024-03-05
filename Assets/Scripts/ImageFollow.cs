using UnityEngine;
using UnityEngine.UI;

public class ImageFollow : MonoBehaviour
{
    public Transform targetObject;
    public Vector3 offset = new Vector3(0f, 1f, 0f);
    public Image imageComponent;

    void Update()
    {
        if (targetObject != null && imageComponent != null)
        {
            Vector3 targetPosition = Camera.main.WorldToScreenPoint(targetObject.position + offset);
            imageComponent.transform.position = targetPosition;
        }
    }
}
