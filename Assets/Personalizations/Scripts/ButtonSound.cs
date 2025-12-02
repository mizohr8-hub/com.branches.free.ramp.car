using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour {


    private void Awake()
    {
        Invoke(nameof(DisableeAnim), 5f);
    }
    public void DisableeAnim()
    {
        this.gameObject.GetComponent<Animator>().enabled = false;
    }

    public void PlaySound()
    {
        RGSK.SoundManager.instance.PlaySound("Button", true);
    }
}
