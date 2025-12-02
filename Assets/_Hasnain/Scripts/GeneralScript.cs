using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGSK;
using UnityEngine.UI;

public class GeneralScript : MonoBehaviour
{
    public static GeneralScript instance;
    public AudioClip pumpkinClip,shockSound;
    public AudioClip[] clips;
    public AudioSource source;
    public Transform[] checkPoints;
    public GameObject mainCam;
    public GameObject NOSButton, leftBtn, gasBtn, gasButton;
    public bool slomo = false;
    public GameObject crackParticle;
    public GameObject carGiftPanel;
    public Image  wrongWayImg;
    private void Awake()
    {
        instance = this;
        //ChangeSkyBox();
    }
    public Material[] skyBoxes;

    void ChangeSkyBox()
    {
        if (PlayerPrefs.GetInt("LevelNum") == 4|| PlayerPrefs.GetInt("LevelNum") == 11|| PlayerPrefs.GetInt("LevelNum") == 17)
        {
            RenderSettings.skybox = skyBoxes[1];
        }
        else
        {
            RenderSettings.skybox = skyBoxes[0];
        }
    }
    public void PlayClip()
    {
        //source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
    public void OnPoiterDownBrake()
    {
        if (RaceManager.instance.activePlayer.GetComponent<RCC_CarControllerV3>().direction != -1)
        {
            RaceManager.instance.activePlayer.GetComponent<Rigidbody>().drag = 5;
            Invoke("ReverseDirection", 1f);
        }
        else
        {
            //RaceManager.instance.activePlayer.GetComponent<RCC_CarControllerV3>().direction = 1;
            RaceManager.instance.activePlayer.GetComponent<Rigidbody>().drag = 0.01f;
        }
    }
    public void OnPoiterUpBrake()
    {
        RaceManager.instance.activePlayer.GetComponent<Rigidbody>().drag = 0.01f;
    }


    public void OnPoiterGasDown()
    {
        if (RaceManager.instance.activePlayer.GetComponent<RCC_CarControllerV3>().direction == -1)
        {
            RaceManager.instance.activePlayer.GetComponent<RCC_CarControllerV3>().direction = 1;
            RaceManager.instance.activePlayer.GetComponent<Rigidbody>().drag = 5;
            Invoke("ReverseDirection", 1f);
        }
        else
        {
            RaceManager.instance.activePlayer.GetComponent<RCC_CarControllerV3>().direction = 1;
            RaceManager.instance.activePlayer.GetComponent<Rigidbody>().drag = 0.01f;
        }
    }
    public void OnPoiterUpGas()
    {
        RaceManager.instance.activePlayer.GetComponent<Rigidbody>().drag = 0.01f;
    }
    void ReverseDirection()
    {
        RaceManager.instance.activePlayer.GetComponent<Rigidbody>().drag = 0.01f;
    }
}
