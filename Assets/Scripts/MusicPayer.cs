using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource levelChangeMusic, continuousSoundtrack1, continuousSoundtrack2;
    private bool isMusicPlaying = true;

    void Start()
    {
        PlayMusic();
    }


    public void ToggleMusic()
    {
        if (isMusicPlaying)
        {
            levelChangeMusic.Pause();
            continuousSoundtrack1.Pause();
            continuousSoundtrack2.Pause();
        }
        else
        {
            levelChangeMusic.UnPause();
            continuousSoundtrack1.UnPause();
            continuousSoundtrack2.UnPause();
        }
        isMusicPlaying = !isMusicPlaying;
    }

    private void PlayMusic()
    {
        levelChangeMusic.Play();
        continuousSoundtrack1.PlayScheduled(AudioSettings.dspTime + levelChangeMusic.clip.length);
        continuousSoundtrack2.PlayScheduled(AudioSettings.dspTime + levelChangeMusic.clip.length);
    }
}

