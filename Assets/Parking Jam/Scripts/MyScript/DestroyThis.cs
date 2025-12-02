using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    // Start is called before the first frame update
    public float delayTime;
    void Start()
    {
        Destroy(gameObject, delayTime);
    }

}
