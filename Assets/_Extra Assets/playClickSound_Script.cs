using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playClickSound_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Gameplay_Script.instance.gameObject.transform.GetChild(3).GetComponent<AudioSource>().Play();
        }
    }
}
