using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectsWithRoller : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovableController>())
        {
            other.GetComponent<MovableController>().LaneObjectDestroy();
        }
        if (other.GetComponent<ObstacleController>())
        {
            other.GetComponent<ObstacleController>().ObstacleDestroyed = true;
            other.gameObject.SetActive(false);
        }
    }
}
