using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NosSpeed : MonoBehaviour
{

    public float maxSpeed, nosSpeed, nosIncrement;
    public float engineTorque;
    public GameObject nosParticles;
    private void Start()
    {
        engineTorque = GetComponent<RCC_CarControllerV3>().engineTorque + ((PlayerPrefs.GetInt("EngineTorque") + 1) * 100);
        GetComponent<RCC_CarControllerV3>().engineTorque = engineTorque;
        print("engineTorque" + engineTorque);
        maxSpeed = GetComponent<RCC_CarControllerV3>().maxspeed + (10);
        GetComponent<RCC_CarControllerV3>().maxspeed = maxSpeed;
        print("maxSpeed" + maxSpeed);
        nosSpeed = GetComponent<RCC_CarControllerV3>().maxspeed + nosIncrement;
        print("nosSpeed" + nosSpeed);
    }

}
