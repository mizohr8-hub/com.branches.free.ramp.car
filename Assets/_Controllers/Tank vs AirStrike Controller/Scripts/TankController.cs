using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.UI;


public class TankController : MonoBehaviour
{

	public GameObject tankControlles;
	public WheelCollider[] tankWheels;
	public Transform mainCam;
	public Transform missleSpawnPoint;
	public float missileRate = 2f;
	public float rotationSpeed = 1f;
	public float moveSpeed = 3500f;
	public GameObject missile;
	public float missileSpeedAdjustment = 1f;
	public GameObject missileButton;
	public Material tankTrack;


	private Rigidbody rb;
	public bool throttle;
	public bool reverse;
	public bool steerRight;
	public bool steerLeft;
	private GameObject spawnedMissile;
	private float nextFireTime = 0;
	private AudioSource myAudio;


	void OnEnable(){
		rb = GetComponent<Rigidbody>();
		myAudio = GetComponent<AudioSource>();
	}


	void Update(){

		////////////////// Get The missile Input////////////////////////////
		if (CrossPlatformInputManager.GetButtonDown("FireMissile")) {
			if (Time.time > nextFireTime + missileRate) {
    //            if (GameManager.instance)
    //            {
				//	if (GameManager.instance.shootingCount>0)
				//	{

				//		nextFireTime = Time.time;
				//		spawnedMissile = (GameObject)Instantiate(missile, missleSpawnPoint.position, missleSpawnPoint.rotation) as GameObject;
				//		spawnedMissile.GetComponent<Rigidbody>().AddForce(spawnedMissile.transform.forward * rb.velocity.magnitude * missileSpeedAdjustment, ForceMode.Impulse);
				//		GameManager.instance.ShootingLimit();
				//		mainCam.GetComponent<ShakeObject>().ShakeCamera(0.15f, 0.2f);
				//		nextFireTime += missileRate;
				//	}
				//}
				//else
    //            {
				//	nextFireTime = Time.time;
				//	spawnedMissile = (GameObject)Instantiate(missile, missleSpawnPoint.position, missleSpawnPoint.rotation) as GameObject;
				//	spawnedMissile.GetComponent<Rigidbody>().AddForce(spawnedMissile.transform.forward * rb.velocity.magnitude * missileSpeedAdjustment, ForceMode.Impulse);
				//	mainCam.GetComponent<ShakeObject>().ShakeCamera(0.15f, 0.2f);
				//	nextFireTime += missileRate;
				//}
				
			}	
		}

		
	}
	float temp;
	int multiplier=0;
	void FixedUpdate(){
		
		////////////////// Get The tank Input////////////////////////////

		steerRight = CrossPlatformInputManager.GetButton ("SteerRight");
		steerLeft = CrossPlatformInputManager.GetButton ("SteerLeft");

		if (steerRight) {

				////////////////Rotating Actual tank/////////////////
				transform.eulerAngles = new Vector3 (
					transform.eulerAngles.x
					, transform.eulerAngles.y + rotationSpeed,
					0f);

		} else if (steerLeft) {
				////////////////Rotating Actual tank/////////////////
				transform.eulerAngles = new Vector3 (
					transform.eulerAngles.x
					, transform.eulerAngles.y - rotationSpeed,
					0f);
		}


		throttle = CrossPlatformInputManager.GetButton ("Throttle");
		reverse = CrossPlatformInputManager.GetButton ("Reverse");

		if (throttle) {
			multiplier = -1;

			foreach (WheelCollider wheel in tankWheels) {
				wheel.motorTorque = moveSpeed;

			}
			temp = tankTrack.mainTextureOffset.x;
			temp = temp + Time.fixedDeltaTime;
			tankTrack.mainTextureOffset = new Vector2 (temp, 0);


		} else if (reverse) {
			multiplier = 1;

			foreach (WheelCollider wheel in tankWheels) {
				wheel.motorTorque = ((moveSpeed/1.5f) * (-1f));


			}
			temp = tankTrack.mainTextureOffset.x;
			temp = temp - Time.fixedDeltaTime;
			tankTrack.mainTextureOffset = new Vector2 (temp, 0);


		} else {
			foreach (WheelCollider wheel in tankWheels) {
				wheel.motorTorque = 0;
				//wheel.brakeTorque = Mathf.Infinity;
			}
			tankTrack.mainTextureOffset = new Vector2 (rb.velocity.magnitude*multiplier, 0);


		}



	}


	float ClampAngle(float angle)  {
		angle = Mathf.Abs(angle);
		if (angle > 270) {
			angle -= 360;
			return angle;
		} else {
			return angle;
		}
	}

}
