using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleMusic : MonoBehaviour
{
    GameObject soundText;
    TextMeshProUGUI textMeshProUGUIComponent;
    GameObject musicObject;
    MusicPlayer musicPlayer;

    void Awake()
    {
        musicObject = GameObject.Find("Music");
        soundText = GameObject.FindWithTag("SoundText");
        textMeshProUGUIComponent = soundText.GetComponent<TextMeshProUGUI>();
        musicPlayer = musicObject.GetComponent<MusicPlayer>();
    }

    public void Change()
    {
        if (textMeshProUGUIComponent != null)
        {
            if (textMeshProUGUIComponent.text == "Mute")
            {
                textMeshProUGUIComponent.text = "UnMute";
            }
            else
            {
                textMeshProUGUIComponent.text = "Mute";
            }
        }
        musicPlayer.ToggleMusic();
        
    }
}

