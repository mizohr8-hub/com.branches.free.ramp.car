using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GiftPanel : MonoBehaviour
{

    public Text ProgressionPercentage;
    public Image ProgressionBar, ProgressionBar1;
    public Image[] carProgessionImage;
    public float panelOpenTime = 1.2f;
    public float nextFillerTime = .5f;
    public GameObject[] carProgressionBars;

    public GameObject[] cars;
    public bool prizeWon = false;
    public Animator animator;
    public GameObject[] rewardImg1;
    public GameObject rewardPanel1;
    private void OnEnable()
    {
        StartCoroutine(Progress());

    }
    public IEnumerator Progress()
    {
        if (PlayerPrefs.GetInt("NewCar1") == 0 && PlayerPrefs.GetInt("OFFRoadMode") <=1)
        {
            carProgressionBars[0].SetActive(true);

            float temp22 = PlayerPrefs.GetInt("OFFRoadMode") / 2.0f;
            float temp33 = ((PlayerPrefs.GetInt("OFFRoadMode") + 1) / 2.0f);
            //dollarText.text = (1.25f * (PlayerPrefs.GetInt("CurrentLevel") + 1)).ToString();
            ProgressionPercentage.text = (temp22 * 100).ToString("F0") + "%";
            ProgressionBar.fillAmount = temp22;
            carProgessionImage[0].fillAmount = temp22; 
            cars[0].SetActive(true);
            yield return new WaitForSecondsRealtime(panelOpenTime);//animation complete wait
            while (true)
            {
                carProgessionImage[0].fillAmount += 0.005f;
                yield return new WaitForSecondsRealtime(0.025f);
                if (carProgessionImage[0].fillAmount >= temp33)
                {
                    break;
                }
            }

           // yield return new WaitForSecondsRealtime(nextFillerTime);

            while (true)
            {
                ProgressionBar.fillAmount += 0.005f;
                yield return new WaitForSecondsRealtime(0.025f);
                if (ProgressionBar.fillAmount >= temp33)
                {
                    break;
                }
            }
            ProgressionBar.fillAmount = temp33;
            ProgressionPercentage.text = (temp33 * 100).ToString("F0") + "%";

            if(ProgressionBar.fillAmount >= 1)
            {
                PlayerPrefs.SetInt("NewCar1", 1);
                PlayerPrefs.SetInt("OurSelectedCar", 1);
                prizeWon = true;
                rewardImg1[0].SetActive(true);
                //You have unlocked car.
            }
        }
        else if(PlayerPrefs.GetInt("NewCar2") == 0 && (PlayerPrefs.GetInt("OFFRoadMode") >= 2 && PlayerPrefs.GetInt("OFFRoadMode") <= 5))
        {
            carProgressionBars[1].SetActive(true);

            float t = PlayerPrefs.GetInt("OFFRoadMode") - 2f;
            float temp22 = t / 4.0f;
            float temp33 = ((t + 1) / 4.0f);
            //dollarText.text = (1.25f * (t + 1)).ToString();
            ProgressionPercentage.text = (temp22 * 100).ToString("F0") + "%";
            ProgressionBar1.fillAmount = temp22;
            carProgessionImage[1].fillAmount = temp22;
            cars[1].SetActive(true);
            yield return new WaitForSecondsRealtime(panelOpenTime);//animation complete wait
            while (true)
            {
                carProgessionImage[1].fillAmount += 0.005f;
                yield return new WaitForSecondsRealtime(0.025f);
                if (carProgessionImage[1].fillAmount >= temp33)
                {
                    break;
                }
            }

           // yield return new WaitForSecondsRealtime(nextFillerTime);
            
            while (true)
            {
                ProgressionBar1.fillAmount += 0.005f;
                yield return new WaitForSecondsRealtime(0.025f);
                if (ProgressionBar1.fillAmount >= temp33)
                {
                    break;
                }
            }
            ProgressionBar1.fillAmount = temp33;
            ProgressionPercentage.text = (temp33 * 100).ToString("F0") + "%";

            if (ProgressionBar1.fillAmount >= 1)
            {
                PlayerPrefs.SetInt("NewCar2", 1);
                PlayerPrefs.SetInt("OurSelectedCar", 2);
                prizeWon = true;
                rewardImg1[1].SetActive(true);
                //You have unlocked car.
            }
        }
        
        yield return new WaitForSecondsRealtime(4f);
        if (prizeWon)
        {
            rewardPanel1.SetActive(true);
        }
        else
        {
            animator.Play("newGiftClose");
        }

        StopCoroutine(Progress());
    }
    public void GiveReward()
    {

    }
    
    public void PanelOff()
    {
       // GameManagerOFFRoad.instance.giftPanelOffRoad.SetActive(false);
       // GameManagerOFFRoad.instance.gameWinPanel.SetActive(true);
        if (PlayerPrefs.GetInt("OFFRoadMode") >= 29)
        {
         //   GameManagerOFFRoad.instance.nextBtnForFinalLevel.SetActive(false);
          //  GameManagerOFFRoad.instance.nextBtnForNewMode.SetActive(true);
        }

        if (PlayerPrefs.GetInt("OFFRoadMode") == 2 && PlayerPrefs.GetInt("Mission1Done") == 0)
        {
            PlayerPrefs.SetInt("Mission1Done", 1); //compplete 3 levels of off road
        }

        if (PlayerPrefs.GetInt("OFFRoadMode") == 29 && PlayerPrefs.GetInt("Mission5Done") == 0)
        {
            PlayerPrefs.SetInt("Mission5Done", 1); //compplete all levels of off road
        }

        GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start, "OFFRoadMode Level " + PlayerPrefs.GetInt("OFFRoadMode") + " Won");//farwa//farwa GA Nov
        //if (prizeWon)
        //{
        //   // GameManagerParking.Instance.GiftPanel.GetComponent<Image>().enabled = false;
        //    rewardCarCutScene.SetActive(true);
        //    Invoke(nameof(EndCutScene), 16f);
        //}
        //else
        //{
        //    //UIManagerParking.Instance.StartCoroutine(UIManagerParking.Instance.GameWin());
        //    //GameManagerParking.Instance.GiftPanel.SetActive(false);
        //}
    }

}
