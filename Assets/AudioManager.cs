using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource backgroundMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }

        // Code to play the provided music clip
        backgroundMusic.clip = musicClip;
        backgroundMusic.Play();
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
}