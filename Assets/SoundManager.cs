/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public AudioSource audioSource;

    // Play the given audio clip
    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    // Destroy the sound object after the audio clip finishes playing
    public void DestroyAfterPlay(AudioClip clip)
    {
        PlaySound(clip);
        Destroy(gameObject, clip.length);
    }
}
*/