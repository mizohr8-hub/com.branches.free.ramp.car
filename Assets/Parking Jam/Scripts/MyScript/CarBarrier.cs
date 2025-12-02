using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class CarBarrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovableController>())
        {
            GetComponent<Animator>().SetTrigger("open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MovableController>())
        {
            GetComponent<Animator>().SetTrigger("close");
        }
    }
}
