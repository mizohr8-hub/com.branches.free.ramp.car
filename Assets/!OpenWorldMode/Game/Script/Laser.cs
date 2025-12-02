using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	public GameObject laser;
	public float onOffDelay = 1f;
	public float startAfter = 1.5f;
	public bool onOff;
	void Start()
	{
		if(onOff)
		{
			InvokeRepeating("LaserOnOff", startAfter, onOffDelay);
		}
	}

	void LaserOnOff()
	{
		if(laser.activeSelf)
		{
			laser.SetActive(false);
		}
		else
		{
			laser.SetActive(true);
		}
	}
}
