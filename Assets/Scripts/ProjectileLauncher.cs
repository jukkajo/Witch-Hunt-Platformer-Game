using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchLocation;
    public GameObject projectilePref;
    // For displaying count of remaining projectiles
    public string counterName = "TODO: Change";
    public string counterTextObjectName = "TODO: Change";
    GameObject counterCanvasObject;
    Transform counterTransformObject;
    TextMeshProUGUI textMeshPro;
    public int _projectileCount; // TODO: if other projectiles, rename, duplicate etc.  

    private void Awake()
    {
        if(counterName != "TODO: Change" && counterTextObjectName != "TODO: Change") {
            counterCanvasObject = GameObject.Find(counterName);
            counterTransformObject = counterCanvasObject?.transform.Find(counterTextObjectName);
            textMeshPro = counterTransformObject?.GetComponent<TextMeshProUGUI>();           
            if (textMeshPro != null)
            {
                textMeshPro.text = ProjectileCount.ToString();
            }
        }
    }

    public int ProjectileCount {
        get {
            return _projectileCount;
        }
        set {
            _projectileCount = value;
        }
    }

    public void FireProjectile()
    {
        if (_projectileCount > 0) {
            GameObject projectile = Instantiate(projectilePref, launchLocation.position, Quaternion.identity);
            Vector3 scale = projectile.transform.localScale;
            projectile.transform.localScale = new Vector3(
                scale.x * transform.localScale.x > 0 ? 1 : -1,
                scale.y,
                scale.z
            );
           
            int tmp = ProjectileCount;
            ProjectileCount = (tmp - 1);
            textMeshPro.text = ProjectileCount.ToString();
        }

    }
}

