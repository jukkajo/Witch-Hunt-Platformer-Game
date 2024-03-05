using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    GameObject healthBar;
    Image healthBarImage; 
    GameObject targetObject;
    DamageElement element;
    public string assignTag = "TODO: Change";
    public string followTargetName = "TODO: Change";
    public Sprite healthMax;
    public Sprite health80;
    public Sprite health60;
    public Sprite health40;
    public Sprite health20;
    public Sprite healthZero;
    
    void Awake()
    {
        if (followTargetName != "TODO: Change") {
            targetObject = GameObject.FindWithTag(followTargetName);
        }

        if (targetObject != null)
        {
            element = targetObject.GetComponent<DamageElement>();
        }
        
        if (assignTag != "TODO: Change") {
            healthBar = GameObject.FindWithTag(assignTag);
        }

        if (healthBar != null)
        {
            healthBarImage = healthBar.GetComponent<Image>();
        }
    }

    public void spriteChange()
    {
        int health = element != null ? element.Health : 0;
        Debug.Log("Health: " + health);
        
        if (healthBarImage != null)
        {
            Debug.Log("Health Image found");
            
            if (health == 0) {
                healthBarImage.sprite = healthZero;
            }
            else if (health > 0 && health <= 20) {
                healthBarImage.sprite = health20;
            }
            else if (health > 20 && health <= 40) {
                healthBarImage.sprite = health40;
            }
            else if (health > 40 && health <= 60) {
                healthBarImage.sprite = health60;
            }
            else if (health > 60 && health <= 80) {
                healthBarImage.sprite = health80;
            }
            else {
                healthBarImage.sprite = healthMax;
            }
        }
        else
        {
            Debug.LogError("Health Image not found");
        }
    }
}

