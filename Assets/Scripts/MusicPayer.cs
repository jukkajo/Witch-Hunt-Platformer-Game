using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPayer : MonoBehaviour
{
    public AudioSource levelChangeMusic, continuousSoundtrack1, continuousSoundtrack2;

    void Start()
    {
        levelChangeMusic.Play();
        continuousSoundtrack1.PlayScheduled(AudioSettings.dspTime + levelChangeMusic.clip.length);
        continuousSoundtrack2.PlayScheduled(AudioSettings.dspTime + levelChangeMusic.clip.length);
    }
}
