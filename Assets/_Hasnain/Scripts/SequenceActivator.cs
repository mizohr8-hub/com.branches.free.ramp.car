using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;



public class SequenceActivator : MonoBehaviour
{
    [Serializable]
    public class sequence 
    {
        public splineMove _g;
        [Range(0,10f)]
        public float _time = 0.1f;
        public AudioClip _audio;
    }

    public sequence[] _sequence;
    AudioSource _ac;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(ActivateSequence());
        _ac = this.GetComponent<AudioSource>();
    }

    IEnumerator ActivateSequence()
    {
        foreach (sequence s in _sequence)
        {
            s._g.enabled = false;
        }

        foreach (sequence s in _sequence)
        {
            yield return new WaitForSecondsRealtime(s._time);
            s._g.enabled = true;
            if(s._audio!=null)
            PlaySound(s._audio);
        }
    }
    void PlaySound(AudioClip _audio)
    {
        if (_audio)
            if (_ac)
                _ac.PlayOneShot(_audio);
    }
}
