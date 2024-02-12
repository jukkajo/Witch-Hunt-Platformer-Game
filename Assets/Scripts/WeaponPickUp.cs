using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPickUp : MonoBehaviour
{
    public Vector3 itemRotationSpeed = new Vector3(0, 1, 0);
    public AudioClip pickUpSound;
    public float volumeLevel = 0.75f;
    public string counterName = "TODO: Change";
    public string counterTextObjectName = "TODO: Change";
    
    GameObject player;
    ProjectileLauncher projectileLauncher;

    GameObject counterCanvasObject;
    GameObject weaponCounter;
    Transform counterTransformObject;
    TextMeshProUGUI textMeshPro;
    
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
         
        
        if(counterName != "TODO: Change" && counterTextObjectName != "TODO: Change") {
        
            counterCanvasObject = GameObject.Find(counterName);
            counterTransformObject = counterCanvasObject?.transform.Find(counterTextObjectName);
            textMeshPro = counterTransformObject?.GetComponent<TextMeshProUGUI>();           
            projectileLauncher = player.GetComponent<ProjectileLauncher>();
        }
        
    }
    

    void OnTriggerEnter2D(Collider2D collision)
    {
    
        // TODO: Increase player specific's weapon's count and update canvas counter
        
        int tmp = projectileLauncher.ProjectileCount;
        projectileLauncher.ProjectileCount = (tmp + 1);
        textMeshPro.text = projectileLauncher.ProjectileCount.ToString();
        
        AudioSource.PlayClipAtPoint(pickUpSound, gameObject.transform.position, volumeLevel);
        Destroy(gameObject);
    }
    
    private void Update()
    {
        transform.Rotate(itemRotationSpeed * Time.deltaTime);
    }
}
