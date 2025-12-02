using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCustom : MonoBehaviour
{
    public GameObject[] spoilers;

    public void OnChangeSpoiler(int id)
    {
        for (int i = 0; i < spoilers.Length; i++)
        {
            spoilers[i].SetActive(false);
        }
    }
}
