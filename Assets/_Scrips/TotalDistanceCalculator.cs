using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalDistanceCalculator : MonoBehaviour
{
    public float totalDistance = 0;
    public bool record = true;
    private Vector3 previousLoc;
    public float previousDistance;
    bool temp = false;
   
    public float distance;
    void FixedUpdate()
    {
        if (record)
            RecordDistance();
    }
    void RecordDistance()
    {
        totalDistance += Vector3.Distance(transform.position, previousLoc);
        previousLoc = transform.position;
        if (!temp)
        {
            temp = true;
            previousDistance = totalDistance;
        }
        distance = totalDistance - previousDistance;
        GameHandler.instance.distanceTextGame.text = distance.ToString("F0")+"M";
        GameHandler.instance.needle.value = distance;
    }
    void ToggleRecord() => record = !record;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GasOff")
        {
            GameHandler.instance.gasPadel.gameObject.GetComponent<CanvasGroup>().interactable = false;
            GameHandler.instance.gasPadel.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameHandler.instance.nos.gameObject.GetComponent<CanvasGroup>().interactable = true;
            GameHandler.instance.nos.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            GameHandler.instance.nos.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            GameHandler.instance.rgskCam.transform.position = GameHandler.instance.rccCam.transform.position+ new Vector3(5f, 5f, 0);
            GameHandler.instance.nosImage.gameObject.SetActive(true);
            GameHandler.instance.rccCam.gameObject.SetActive(false);
            GameHandler.instance.rgskCam.gameObject.SetActive(true);
        }
        else if (other.gameObject.tag == "FinishPoint")
        {
            GameHandler.instance.gameStarted = false;
            Handheld.Vibrate();
            Invoke("lvlComp", 5f);

        }
    }

    int id = 0;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pins")
        {
            if (!GameHandler.instance.gameSfx.isPlaying)
            {
                GameHandler.instance.gameSfx.PlayOneShot(GameHandler.instance.pin);
            }
                GameHandler.instance.totalCoins = GameHandler.instance.totalCoins + 5;

            id++;
            GameHandler.instance.coinsImg[id].SetActive(true);
            print(id);
            if (id >= 4) 
            {
                id = 0;
            }

            Handheld.Vibrate();


        }
         else if (collision.gameObject.tag == "WoodBox")
        {
            if (!GameHandler.instance.gameSfx.isPlaying)

            {
                GameHandler.instance.gameSfx.PlayOneShot(GameHandler.instance.boxCrash);
            }

                GameHandler.instance.totalCoins = GameHandler.instance.totalCoins + 5; 
            
            id++;
            GameHandler.instance.coinsImg[id].SetActive(true);
            print(id);
            if (id >= 4)
            {
                id = 0;
            }

            Handheld.Vibrate();
        } 
        else if (collision.gameObject.tag == "Elephant")
        {
            GameHandler.instance.gameSfx.PlayOneShot(GameHandler.instance.elephant);

            Handheld.Vibrate();

        } 
        else if (collision.gameObject.tag == "CarHorn")
        {
            GameHandler.instance.gameSfx.PlayOneShot(GameHandler.instance.carHorn);


            Handheld.Vibrate();
        }
        
    }
   public void lvlComp()
    {


        //GameHandler.instance.rccCanvas.SetActive(false);
        //GameHandler.instance.gameOver.SetActive(true);
        //GameHandler.instance.gameSfx.PlayOneShot(GameHandler.instance.crowd);
        
        
        //if (PlayerPrefs.GetInt("Currentlvl") < 3)
        //{
            GameHandler.instance.nextLevel.SetActive(true);
        //}
        //else if (PlayerPrefs.GetInt("Currentlvl") == 3)
        //{
        //    GameHandler.instance.nextLevel.SetActive(false);
        //}
        //GameHandler.instance.car.SetActive(false);
        //Time.timeScale = 0f;
        //Firebase.Analytics.FirebaseAnalytics.LogEvent("Level " + PlayerPrefs.GetInt("Currentlvl") + " of Ramp Car Jumping Mode Completed");
        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "Level " + PlayerPrefs.GetInt("Currentlvl")+ " of Ramp Car Jumping Mode Completed");//HAiderGA

    }

}
