using UnityEngine;
using System.Collections;

public class MissileMover : MonoBehaviour
{
    public int Lifetime;
    public float Speed = 80;
	public float damage = 10;
	public GameObject collisionEffect;
	public AudioClip missileSound;
	public ParticleSystem blast;


	private bool callOnce = true;


    private void Start()
    {
        Destroy(gameObject, Lifetime);
		this.GetComponent<Rigidbody> ().AddForce (transform.forward *Speed,ForceMode.Impulse);
		if (GetComponent<AudioSource> ()) {
			GetComponent<AudioSource> ().PlayOneShot (missileSound);
		}
    }


	void OnCollisionEnter(Collision other){

		if (other.transform.CompareTag ("Enemy")) {
		//	other.gameObject.GetComponent<TNT>().DisableTNT ();
		}
		if (other.transform.CompareTag("ShipEnemy"))
		{
			//other.gameObject.GetComponent<TNT>().DisableTNT();
			Instantiate(blast, other.transform.position, other.transform.rotation);
			Destroy(other.gameObject);
		}
		else if (other.transform.CompareTag ("Weapon")) {
			Destroy(other.gameObject);
		} 
		else if (other.transform.CompareTag ("heli")) {
			if (callOnce) {
				callOnce = false;
				Destroy(other.gameObject);
			}
		} 

		if (collisionEffect) {
			GameObject	tempEffect = (GameObject)Instantiate(collisionEffect, other.contacts [0].point, this.transform.rotation) as GameObject;
			Destroy(tempEffect, 2f);
		}

		Destroy(gameObject);
	}


}
