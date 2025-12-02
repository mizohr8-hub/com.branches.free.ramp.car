using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class challengeEntry : MonoBehaviour
{
    // Start is called before the first frame update
    public Image vehicleImg;
    public Text ChallengeStatement;
    public Sprite[] vehicleSprites;
    void Start()
    {
        if (PlayerPrefs.GetInt("ChallengeNumber")%5==0) //ambulance
        {
            vehicleImg.sprite = vehicleSprites[0];
            ChallengeStatement.text = "AMBULANCE IS STUCK IN TRAFFIC JAM SOLVE THE PUZZLE TO TAKE IT OUT";
        }
        if (PlayerPrefs.GetInt("ChallengeNumber")%5==1) //school bus
        {
            vehicleImg.sprite = vehicleSprites[1];
            ChallengeStatement.text = "SCHOOL BUS IS STUCK IN TRAFFIC JAM SOLVE THE PUZZLE TO TAKE IT OUT";
        }
        if (PlayerPrefs.GetInt("ChallengeNumber")%5==2) //fire truck
        {
            vehicleImg.sprite = vehicleSprites[2];
            ChallengeStatement.text = "FIRE TRUCK IS STUCK IN TRAFFIC JAM SOLVE THE PUZZLE TO TAKE IT OUT";
        }
        if (PlayerPrefs.GetInt("ChallengeNumber")%5==3) //police car
        {
            vehicleImg.sprite = vehicleSprites[3];
            ChallengeStatement.text = "POLICE CAR IS STUCK IN TRAFFIC JAM SOLVE THE PUZZLE TO TAKE IT OUT";
        }
        if (PlayerPrefs.GetInt("ChallengeNumber")%5==4) //taxi
        {
            vehicleImg.sprite = vehicleSprites[4];
            ChallengeStatement.text = "TAXI IS STUCK IN TRAFFIC JAM SOLVE THE PUZZLE TO TAKE IT OUT";
        }
    }
}
