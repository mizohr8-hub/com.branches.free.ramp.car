using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fanrotate : MonoBehaviour
{
    private float m_LastRealTime;
    public RCC_CarControllerV3 rcc;
    public Vector3 rotation;
    private void Start()
    {
        m_LastRealTime = Time.realtimeSinceStartup;
        //rcc = FindObjectOfType<RCC_CarControllerV3>();
    }
    void Update()
    {
        float deltaTime = Time.deltaTime;
        deltaTime = (Time.realtimeSinceStartup - m_LastRealTime);
        m_LastRealTime = Time.realtimeSinceStartup;
        transform.Rotate(rotation * deltaTime * rcc.speed, Space.Self);
    }
}
