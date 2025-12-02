using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLaneObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovableController>())
        {
            other.GetComponent<MovableController>().LaneObjectDestroy();
        }       
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<MovableController>())
        {
            other.GetComponent<MovableController>().LaneObjectDestroy();
        }
          
    }

}
