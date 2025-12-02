using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGSK;
public class FinishTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "FinishLine")
        {
           FindObjectOfType<Car_Controller>().enabled = false;
            print(name);
        }
    }
}
