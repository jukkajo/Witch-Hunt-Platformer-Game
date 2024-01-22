using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreasureText : MonoBehaviour
{
    public float textFadeTime = 1.2f;
    private float counter = 0f;
    public Vector3 textSpeed = new Vector3(0,80,0);
    RectTransform treasureTextTransform;
    TextMeshProUGUI ugui;
    private Color defaultColor;
    
    void Awake()
    {
        treasureTextTransform = GetComponent<RectTransform>();
        ugui = GetComponent<TextMeshProUGUI>();
        defaultColor = ugui.color;
    }

    void Update()
    {
        treasureTextTransform.position += textSpeed * Time.deltaTime;
        counter += Time.deltaTime;
        if (counter < textFadeTime) {
            float fadeVariable = defaultColor.a * (1 - (counter / textFadeTime));
            ugui.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, fadeVariable);
        } else {
           Destroy(gameObject);
        }
    }
}
