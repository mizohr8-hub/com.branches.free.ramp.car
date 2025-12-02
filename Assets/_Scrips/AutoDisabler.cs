using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisabler : MonoBehaviour
{
    public float duration;
    public bool activate = false;
    public GameObject[] items;
    private void OnEnable()
    {
        if (activate)
        {
            Invoke("EnableThis", duration);
        }
        else
        {
            Invoke("DisableThis", duration);
        }
    }

    void DisableThis()
    {
        gameObject.SetActive(false);
    }
    void EnableThis()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(true);
        }
    }
}
