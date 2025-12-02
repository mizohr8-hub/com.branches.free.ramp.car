using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasisScript : MonoBehaviour
{
    public GameObject[] wheels;
    public GameObject wheel;

    public void OnStart()
    {
        print("here");
        wheel.SetActive(true);
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetComponent<MeshRenderer>().enabled = false;
        }
    } 
    public void OnEnd()
    {
        //for (int i = 0; i < wheels.Length; i++)
        //{

        //    wheels[i].GetComponent<MeshRenderer>().enabled = true;
        //}
        //wheel.SetActive(false);
    }
}
