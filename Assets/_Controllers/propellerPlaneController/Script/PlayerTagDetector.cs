using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Aeroplane;


public class PlayerTagDetector : MonoBehaviour 
{

	public bool stillAtSpot = false;
	public float stayTime = 4f;

	bool increaseAirplaneDrag = false;

	bool checkIslandDistanceOnce = true;
	public float maxRange = 100f;
	public GameObject cityRangeSpeedAlert;

	public GameObject explosion;
	public GameObject destroyedPlane;
	bool doorOnce = true;


	IEnumerator StartAlert (float waitSec)
	{
		cityRangeSpeedAlert.SetActive (true);
		yield return new WaitForSeconds (waitSec);
		cityRangeSpeedAlert.SetActive (false);
	}


	void Update ()
	{
		if (checkIslandDistanceOnce) 
		{
			//if (GameManager.instance.activePlayer != null) 
			//{
			//	if (GameManager.instance.activePlayer.GetComponent<AeroplaneController> () != null) 
			//	{
			//		if (Vector3.Distance (GameManager.instance.activeObjective.transform.Find("indicator box").transform.position, GameManager.instance.activePlayer.transform.position) <= maxRange) 
			//		{
			//			checkIslandDistanceOnce = false;
			//			StartCoroutine (StartAlert (5));
			//		}
			//	}
			//}
		}


		if (stillAtSpot) 
		{
			FillImage ();
		}

		if (increaseAirplaneDrag) 
		{
			GetComponent<Rigidbody> ().drag += 0.0001f;
		}
	}


	void FillImage()
	{
		
//			StartCoroutine (VehicleParked ());
			VehicleParked();
	}
		
	public void ResetImage ()
	{
	}



	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "SeaWater") 
		{
			//Instantiate Explosion

			if (explosion) 
			{
				Instantiate (explosion, transform.position, transform.rotation);
				GetComponent<Rigidbody> ().isKinematic = true;

				for (int i = 0; i < transform.childCount; i++) 
				{
					transform.GetChild (i).gameObject.SetActive (false);
				}

			//	GameManager.instance.gameOver = true;
			//	GameManager.instance.LevelFail ();
			}
			if (GetComponent<AeroplaneController> () != null) 
			{
				Blast ();
				GetComponent<AeroplaneController> ().enabled = false;
			//	GetComponent<AeroplaneUserControl4Axis> ().enabled = false;
			}

		}

		if (other.gameObject.tag == "Destroy") 
		{
			if (GetComponent<AeroplaneController> () != null) 
			{
				if (GetComponent<AeroplaneController> ().ForwardSpeed > 5) 
				{
					if (explosion) 
					{
						Instantiate (explosion, transform.position, transform.rotation);
					}

					Blast ();
					GetComponent<AeroplaneController> ().enabled = false;
				//	GetComponent<AeroplaneUserControl4Axis> ().enabled = false;

				}
			}
		}

	
	}


	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Door") 
		{
			if (other.transform.childCount > 1) 
			{
				if (doorOnce) 
				{
					doorOnce = false;
					other.transform.GetChild (0).GetComponent<Animator> ().SetTrigger ("OpenDoor");
					other.transform.GetChild (1).GetComponent<Animator> ().SetTrigger ("OpenDoor");
				}
			}
		}
	}



	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "ParkingSpot") 
		{
			stillAtSpot = true;
		}

		if (other.gameObject.tag == "CityLimit") 
		{
			other.transform.parent.gameObject.SetActive (false);
		}

		if (other.gameObject.tag == "CivilianRadius") 
		{
			other.transform.GetChild (0).gameObject.SetActive (true);
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "ParkingSpot") 
		{
			stillAtSpot = false;
			ResetImage ();
		}

		if (other.gameObject.tag == "CivilianRadius") 
		{
			other.transform.GetChild (0).gameObject.SetActive (false);
		}

		if (other.gameObject.tag == "Door") 
		{
			if (other.transform.childCount > 1) 
			{
				if (!doorOnce) 
				{
					doorOnce = true;
					other.transform.GetChild (0).GetComponent<Animator> ().SetTrigger ("CloseDoor");
					other.transform.GetChild (1).GetComponent<Animator> ().SetTrigger ("CloseDoor");
				}
			}
		}

	}


	void VehicleParked ()
	{
		if (stillAtSpot) 
		{
			ResetImage ();
			stillAtSpot = false;


			StartCoroutine (StartFadeInOut ());

			int i = 0;
			switch (i) 
			{

		
			case 0:
				print ("AirplaneParked");
				increaseAirplaneDrag = true;
				GameObject.FindObjectOfType<AeroplaneController> ().enabled = false;
				//GameObject.FindObjectOfType<AeroplaneUserControl4Axis> ().enabled = false;
//				yield return new WaitForSeconds (2f);
				break;
			}
		}
	}


	IEnumerator StartFadeInOut ()
	{

		yield return new WaitForSeconds (2f);

		increaseAirplaneDrag = false;

	
	}



	void Blast()
	{
		GameObject tempBlast = Instantiate (explosion, transform.position, transform.rotation);
		Destroy (tempBlast, 1f);

		Instantiate (destroyedPlane, transform.position, transform.transform.rotation);

		GetComponent<Rigidbody> ().isKinematic = true;
		for (int i = 0; i < transform.childCount; i++) 
		{
			transform.GetChild (i).gameObject.SetActive (false);
		}

		//GameManager.instance.gameOver = true;
		//GameManager.instance.LevelFail ();
	}
}