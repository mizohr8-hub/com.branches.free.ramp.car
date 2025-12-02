using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryNew : MonoBehaviour {

	LineRenderer lr;
	PlayerController pc;
	void Start()
	{
		lr = new GameObject ("Trajectory").AddComponent<LineRenderer>().GetComponent<LineRenderer>();
		lr.transform.parent = this.transform;
		pc = this.GetComponent<PlayerController>();
		lr.startWidth = 0.05f;
		lr.endWidth = 0.05f;
	}

	void Update()
	{
		if(Input.GetMouseButton(0))
		{
			lr.enabled =true;
			lr.SetPosition(0, transform.position);
		
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			lr.SetPosition(1, -mousePos);
		}
		else
		lr.enabled =false;

	}
}
