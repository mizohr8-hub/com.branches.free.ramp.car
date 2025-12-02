using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapSoundScript : MonoBehaviour
{
    public AudioSource audio;
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag=="Player")
        {
            print("In");
            audio.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audio.Stop();
            print("out");
        }
    }
}
