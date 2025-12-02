using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speddoo : MonoBehaviour {
    public Text speedTxt, booster;

    public GameObject camra;
    private bool isBoosting;
    private float boosterResetDelay = 10f;
    private int boostTime = 5;
    
    // Use this for initialization
    void Start () {
        //a = camera.GetComponent<Camera>().fieldOfView;
        //boosterResetAfter = Time.time;
    }

    private void Update()
    {
        boosterResetDelay -= Time.deltaTime;
        if (boostTime < 5 )
        {
            if (boosterResetDelay <= 0)
            {
                boostTime = 5;
                boosterResetDelay = 10f;
                print("boost refilled");
            }
        }
    }


    void LateUpdate () {
        //speedTxt.text = (VehicleTelemetry.instance.target.speed).ToString("f0");
        booster.text = boostTime.ToString();
        if (isBoosting && boostTime >= 1)
        {
            camra.GetComponent<Camera>().fieldOfView = Mathf.Lerp(camra.GetComponent<Camera>().fieldOfView, 99, Time.deltaTime * 2);
        }
        else //if(!isBoosting)
        {

            if (camra.GetComponent<Camera>().fieldOfView > 80)
            {
               camra.GetComponent<Camera>().fieldOfView = Mathf.Lerp(camra.GetComponent<Camera>().fieldOfView, 72, Time.deltaTime * 2);
            }
        }
        
    }
    IEnumerator BoosterLimit()
    {
        if (boostTime > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                boostTime--;
                yield return new WaitForSeconds(1);
                print(boostTime);
            }
        }
    }
    public void Boost()
    {
        isBoosting = true;
        print("Boost Pressed");

        StartCoroutine("BoosterLimit",0f);
    }
    public void UnBoost()
    {
        print("Boost unpressed");
        StopCoroutine("BoosterLimit");
        isBoosting = false;
    }
}
