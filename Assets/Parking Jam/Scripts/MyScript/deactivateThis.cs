using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivateThis : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Off), time);
    }

    void Off()
    {
        gameObject.SetActive(false);
    }
}
