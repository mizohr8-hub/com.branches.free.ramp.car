using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayAudioSources : MonoBehaviour
{
    public static GamePlayAudioSources instance;

    public AudioSource ButtonClick1;
    public AudioClip[] buttonClickSound1;

    public AudioSource GameAmbience;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }
    public void ClickButtonSounds(int s)
    {
        ButtonClick1.PlayOneShot(buttonClickSound1[s]);
    }
}
