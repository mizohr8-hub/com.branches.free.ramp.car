using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Shashkay : MonoBehaviour
{
    public Transform[] _objects;
    public Transform[] _pos;//, _oldPos;

    public AudioSource _sound, _sound1;
    // Use this for initialization
    void OnEnable()
    {
        StartCoroutine(_startBtns());
        //StartCoroutine("_startBtns");
    }

    IEnumerator _startBtns()
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            //_objects[i].DOJump(_pos[i].position, 1f, 1, 0.02f, false);
            _objects[i].position = _pos[i].position;
            yield return new WaitForSeconds(0.06f);
            if (_objects[i].GetComponent<AudioSource>())
                _objects[i].GetComponent<AudioSource>().Play();

           
            int a = Random.Range(0, 2);
            if (a == 0)
                _sound.Play();
            else
                _sound1.Play();

        }
        //yield return new WaitForSeconds(0.05f);

    }

}
