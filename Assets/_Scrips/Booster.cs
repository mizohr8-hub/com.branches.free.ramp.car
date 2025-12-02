using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] float  BoostAmount;
    // Start is called before the first frame update



    private void Start()
    {
        BoostAmount += ((PlayerPrefs.GetInt("EngineTorque") + 1) * 5);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            GameHandler.instance.gameStarted = true;
            BoostAmount += (PlayerPrefs.GetInt("EngineTorque") + 1 * 5);
            col.GetComponent<RCC_CarControllerV3>().downForce = 20;
            col.GetComponent<Rigidbody>().AddForce(transform.forward*BoostAmount,ForceMode.VelocityChange);
        }
    }
}
