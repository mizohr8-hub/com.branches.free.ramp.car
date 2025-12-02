using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomanStyle : MonoBehaviour {

    public GameObject garage;
    public GameObject red, blue, green;
    public GameObject red1, blue1, green1;
    public GameObject cam;
    public GameObject menupanel;

	// Use this for initialization
	void OnEnable () {

        cam.GetComponent<MenuCamera>().enabled = false;
        menupanel.SetActive(false);
        red.SetActive(true);
        blue.SetActive(true);
        green.SetActive(true);

        garage.SetActive(false);

        red1.SetActive(false);
        blue1.SetActive(false);
        green1.SetActive(false);
    }

    public void SwitchCars()
    {
        menupanel.SetActive(true);

        garage.SetActive(true);

        red.SetActive(false);
        blue.SetActive(false);
        green.SetActive(false);

        red1.SetActive(true);
        blue1.SetActive(true);
        green1.SetActive(true);
    }


}
