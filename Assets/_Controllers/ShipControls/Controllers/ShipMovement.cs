using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;



public class ShipMovement : MonoBehaviour 
{

	public Transform missleSpawnPoint1;
	public Transform missleSpawnPoint2;

	private GameObject spawnedMissile;
	public float missileRate = 2f;

	private float nextFireTime = 0;

	public GameObject missile;
	public float missileSpeedAdjustment = 1f;
	public GameObject missileButton;

	public float speed    = 0f;
	public float accel    = 0.2f;
	public float rotSpeed = 5f;
	public float maxSpeed = 5f;
	public float frwd;

	bool shipMoving = false;
	public bool isPressed = false;

	private Rigidbody rb;
	public ParticleSystem waterFXLeft;
	public ParticleSystem waterFXLeft1;
	public ParticleSystem waterFXRight;
	public ParticleSystem waterFXRight1;

	public Transform shipCamTarget;


	void Start()
	{
		rb = this.gameObject.GetComponent<Rigidbody> ();

	}

	float val;
	public void sliderValueChange(Slider slider)
	{
		frwd = slider.value;
	}

    private void Update()
    {
		if (CrossPlatformInputManager.GetButtonDown("FireShip"))//farwa
		{
			if (Time.time > nextFireTime + missileRate)
			{
				//Debug.LogError("missssle");
				nextFireTime = Time.time;
				spawnedMissile = (GameObject)Instantiate(missile, missleSpawnPoint1.position, missleSpawnPoint1.rotation) as GameObject;
				spawnedMissile.GetComponent<Rigidbody>().AddForce(spawnedMissile.transform.forward * rb.velocity.magnitude * missileSpeedAdjustment, ForceMode.Impulse);
				spawnedMissile = (GameObject)Instantiate(missile, missleSpawnPoint2.position, missleSpawnPoint2.rotation) as GameObject;
				spawnedMissile.GetComponent<Rigidbody>().AddForce(spawnedMissile.transform.forward * rb.velocity.magnitude * missileSpeedAdjustment, ForceMode.Impulse);
				//GameManager.instance.levelStats.cameras[GameManager.instance.objectiveCount].GetComponent<ShakeObject>().ShakeCamera(0.15f, 0.2f);
				nextFireTime += missileRate;
			}
		}
	}

    void FixedUpdate()
	{
		

		//For Forward Movement
		if (frwd !=0 || Input.GetKey(KeyCode.UpArrow)) 
		{
			shipMoving = true;	
			rb.AddForce	(transform.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
		} 
		else 
		{
			shipMoving = false;
		}



		//For Steering Ship
		if (shipMoving) 
		{
			if (CrossPlatformInputManager.GetButton ("Left") || Input.GetKey(KeyCode.LeftArrow)) 
			{
				transform.Rotate (-Vector3.up * rotSpeed * Time.deltaTime);
			} 
			else if (CrossPlatformInputManager.GetButton ("Right") || Input.GetKey(KeyCode.RightArrow)) 
			{
				transform.Rotate (Vector3.up * rotSpeed * Time.deltaTime);
			}
		}



		//Front Ship Water Emission Control
		if (shipMoving) 
		{
			if (!waterFXLeft.isPlaying || !waterFXRight.isPlaying || !waterFXLeft1.isPlaying || !waterFXRight1.isPlaying) 
			{
				waterFXLeft.Play ();
				waterFXRight.Play ();
				waterFXLeft1.Play ();
				waterFXRight1.Play ();
			}
		}
		else 
		{
			if (waterFXLeft.isPlaying || waterFXRight.isPlaying || waterFXLeft1.isPlaying || waterFXRight1.isPlaying)
			{
				waterFXLeft.Stop ();
				waterFXRight.Stop ();
				waterFXLeft1.Stop ();
				waterFXRight1.Stop ();
			}
		}
	}
}