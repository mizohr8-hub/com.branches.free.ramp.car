using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    public bool Checkpoint;

    public GameObject CollectionEffect;
    public GameObject Particle;
    public GameObject confetti;
    public GameObject UICoin;
    public Transform ConfettiSpawn;
    public AudioClip CoinSound;
    public GameObject timer; 
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.transform.gameObject.SetActive(false);
            PlayerPrefs.SetInt("CoinsPoints", PlayerPrefs.GetInt("CoinsPoints") + 1);
            Debug.LogError(PlayerPrefs.GetInt("CoinsPoints") + "Actual amount");
            if (Checkpoint)
            {
                if (PlayerPrefs.GetInt("CoinsPoints") == 5)
                {
                    if (OpenWorldManager.Instance)
                    {
                        PlayerPrefs.SetInt("Currency" , PlayerPrefs.GetInt("Currency") + 1000);
                        OpenWorldManager.Instance.LevelCompletePanel.SetActive(true);
                        timer.SetActive(false);
                        
                       



                    }
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("CoinsPoints") == 15)
                {
                    if (OpenWorldManager.Instance)
                    {
                        PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 1000);
                        OpenWorldManager.Instance.LevelCompletePanel.SetActive(true);
                        timer.SetActive(false);
                       // Time.timeScale = 0;
                     



                    }
                }
            }
            Invoke("Destroythis", 2f);
            GetComponent<AudioSource>().PlayOneShot(CoinSound);
            Instantiate(CollectionEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }
    }

    public void Destroythis()
    {
       // confetti.SetActive(false);
       // UICoin.SetActive(false);
        Destroy(this.gameObject);
    }
}
