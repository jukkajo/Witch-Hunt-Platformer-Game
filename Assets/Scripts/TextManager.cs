using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextManager : MonoBehaviour
{   
    // Prefabs
    public GameObject hitText;
    public GameObject healthText;
    public GameObject treasureText;
    public Canvas canvas;
    
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }
    
    private void OnEnable()
    {
        Events.damageTaken += DamageCaused;
        Events.damageHealed += DamageHealed;
        Events.treasurePicked += TreasurePicked;
    }

    private void OnDisable()
    {
        Events.damageTaken -= DamageCaused;
        Events.damageHealed -= DamageHealed;
        Events.treasurePicked -= TreasurePicked; 
    }
    
    public void DamageCaused(GameObject target, int damage) {
    
        Vector3 position = Camera.main.WorldToScreenPoint(target.transform.position);

        TMP_Text tmpText = Instantiate(hitText, position, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damage.ToString();
    }

    public void DamageHealed(GameObject target, int damageHealed) {
    
        Vector3 position = Camera.main.WorldToScreenPoint(target.transform.position);

        TMP_Text tmpText = Instantiate(healthText, position, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damageHealed.ToString();
    }
    
    
    public void TreasurePicked(GameObject target, int amount) {
    
        Vector3 position = Camera.main.WorldToScreenPoint(target.transform.position);

        TMP_Text tmpText = Instantiate(treasureText, position, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();

        tmpText.text = amount.ToString();
    }
}
