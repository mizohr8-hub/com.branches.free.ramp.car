using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecPointText : MonoBehaviour {
   // public GameObject text;
    private void OnEnable()
    {
      //  Invoke("FalseMe",1f);
    }
    public void FalseMe()
    {
        //Debug.Log("Event work");
        gameObject.SetActive(false);
    }
}
