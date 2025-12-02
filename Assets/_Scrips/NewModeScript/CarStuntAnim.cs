using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStuntAnim : MonoBehaviour
{
    public static CarStuntAnim instance;
    public Animator anim;
    public GameObject[] wheels;
    public GameObject[] nos;

    private void Start()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CarStuntNew"))
        {
            Debug.Log("PlayerDetect");
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].GetComponent<RCC_WheelCollider>().enabled = false;
            }
            //for (int i = 0; i < nos.Length; i++)
            //{
            //    nos[i].SetActive(false);
            //}
            anim.enabled = true;

            int num = Random.Range(1, 3);

            anim.SetInteger("CarStuntRotation", num);

            //GameManager.instance.LevelStunts++;
            //print("STUNT<>" + GameManager.instance.LevelStunts++);
            // GameManager.instance.StuntReward++;
            if (GameHandler.instance)
            {
                GameHandler.instance.totalCoins += 200;
                GameHandler.instance.flipCoin.SetActive(true);
            }
        }

       // Invoke("EnableWheelCollider", 4f);


    }

    void EnableWheelCollider()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetComponent<RCC_WheelCollider>().enabled = true;
        }
    }


    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "Collision")
    //    {
    //        PlayerPrefs.SetInt("Hit",PlayerPrefs.GetInt("Hit") + 1) ;
    //        print("Total Hits" + PlayerPrefs.GetInt("Hit"));
    //        print("<<<<<<<<<<is collided>>>>>>>>>>>>>");
    //        GameManager.instance.LevelHits++;
            
    //    }

    //}

}
