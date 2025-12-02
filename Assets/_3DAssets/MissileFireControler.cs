using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Samples;
using UnityStandardAssets.CrossPlatformInput;

public class MissileFireControler : MonoBehaviour {
	public Transform missleSpawnPoint;
	public float missileRate = 2f;
	public float rotationSpeed = 1f;
	public float moveSpeed = 500f;
	public GameObject missile;
	public float missileSpeedAdjustment = 1f;
	public GameObject missileButton;
	private GameObject spawnedMissile;
	private float nextFireTime = 0;
	private Rigidbody rb;
	private AudioSource myAudio;
	private GameObject enemyTarget;
	private bool lookAtTarget;
	public bool openworld;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		myAudio = GetComponent<AudioSource>();
		if (!openworld)
		{
			//if (GameManager.instance.levelNo == 13)
			//	missileButton.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (CrossPlatformInputManager.GetButtonDown("FireMissile")) {
			if (Time.time > nextFireTime + missileRate) {
				//if (GameManager.instance)
				//{

				//	if (GameManager.instance.shootingCount > 0)
				//	{
				//		nextFireTime = Time.time;
				//		spawnedMissile = (GameObject)Instantiate(missile, missleSpawnPoint.position, missleSpawnPoint.rotation) as GameObject;
				//		spawnedMissile.GetComponent<Rigidbody>().AddForce(spawnedMissile.transform.forward * rb.velocity.magnitude * missileSpeedAdjustment, ForceMode.Impulse);
				//		GameManager.instance.ShootingLimit();
				//		GameManager.instance.levelStats.cameras[GameManager.instance.objectiveCount].GetComponent<ShakeObject>().ShakeCamera(0.15f, 0.2f);
				//		nextFireTime += missileRate;
				//	}
				//}
				//else
    //            {
				//	nextFireTime = Time.time;
				//	spawnedMissile = (GameObject)Instantiate(missile, missleSpawnPoint.position, missleSpawnPoint.rotation) as GameObject;
				//	spawnedMissile.GetComponent<Rigidbody>().AddForce(spawnedMissile.transform.forward * rb.velocity.magnitude * missileSpeedAdjustment, ForceMode.Impulse);
				//	GameManager.instance.levelStats.cameras[GameManager.instance.objectiveCount].GetComponent<ShakeObject>().ShakeCamera(0.15f, 0.2f);
				//	nextFireTime += missileRate;
				//}
			}	
		}

	


		if (lookAtTarget && enemyTarget) {
			missleSpawnPoint.LookAt (enemyTarget.transform);
		} else {
			missleSpawnPoint.localEulerAngles = Vector3.zero;
		}
	}
	public void SetEnemy(Transform obj)
	{
		enemyTarget = obj.gameObject;
		lookAtTarget = true;
	}
	public void UnSetEnemy(Transform obj)
	{
		enemyTarget = obj.gameObject;
		lookAtTarget = false;
	}


}
