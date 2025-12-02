using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private void Update()
    {
        this.transform.LookAt(FindObjectOfType<Camera>().transform);
    }
}
