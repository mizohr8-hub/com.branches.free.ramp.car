using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomQuots : MonoBehaviour {
	public List<string> quotsText = new List<string> ();
	public static RandomQuots instance;
	// Use this for initialization
	void Start () {
		instance = this;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public string quotsStr()
	{
		return quotsText[Random.Range(0,quotsText.Count)];
	}
}
