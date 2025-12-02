using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using RGSK;

[Serializable]
public class CustomReward
{
    public bool hasReward = false;
    public bool rewardClaimed = false;
    public GameObject highlighter;
    public Button btnComponent;
    public Sprite rewardImage;
    public string rewardString;
}

public class CustomDailyReward : MonoBehaviour
{
    public CustomReward[] rewards;
    private static DateTime startDate;
    private static DateTime today;
    private static TimeSpan debugTime;

    [Space][Space][Space]
    public GameObject DailyRewardsPanel;
    public Button closeBtn;
    public Button claimButton;
    public AudioClip Claim_sound;

    [Space]
    public GameObject RewardPanel;
    public Text headingTxt;
    public Image rewardImg;
    public Text rewardTxt;
    public AudioClip rewardClip;

    public Text currentTimer;

    //public GameObject fifteenDaysPanel;
    //public GameObject thirtyDaysPanel;

    public bool debugMode = false;

    public Button advanceDayBtn;


    void Start()
    {
        for (int i = 0; i < highlighter.Length; i++)
        {
            highlighter[i].SetActive(false);
        }
        startTime = true;
        if (debugMode)
        {
            advanceDayBtn.gameObject.SetActive(true);
        }
        else
        {
            advanceDayBtn.gameObject.SetActive(false);
        }

        claimButton.gameObject.SetActive(false);
        closeBtn.onClick.AddListener(OnCloseBtnClick);

        claimButton.onClick.AddListener(OnClaimBtnClick);

        CheckForRewardAlreadyClaimed();
        SetStartDate();

       


    }
    public bool startTime = false;

    private void Update()
    {
        if (startTime) 
        {
            currentTimer.text = GetFormattedTime(GetTimeDifference());
        }
        else
        {
            currentTimer.text = "You can claim your reward.";
        }
    }

    void SetStartDate()
    {
        if (PlayerPrefs.HasKey("DateInitialized")) //if we have the start date saved, we'll use that
        {
            startDate = Convert.ToDateTime(PlayerPrefs.GetString("DateInitialized"));
            Debug.Log("Starting Date is set : " + startDate.ToString());
        }
        else //otherwise...
        {
            startDate = DateTime.Now; //save the start date ->
            Debug.Log("Starting Date get : " + startDate.ToString());
            PlayerPrefs.SetString("DateInitialized", startDate.ToString());
        }
        CheckForDay();
    }

    TimeSpan elapsed;
    private int GetDaysPassed()
    {
        //today = DateTime.Now;
        today = DateTime.Now;

        //days between today and start date -->
        elapsed = today.Subtract(startDate);
        double days;
        if (debugMode == true)
        {
            days = debugTime.TotalDays;
        }
        else
        {
            days = elapsed.TotalDays;
        }

        return (int)days;
    }

    private void CheckForRewardAlreadyClaimed()
    {
        for (int i = 0; i < rewards.Length; i++)
        {
            if (PlayerPrefs.GetInt("RewardGiven" + i) == 1)
            {
                rewards[i].rewardClaimed = true;
                rewards[i].btnComponent.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        for (int i = 0; i < highlighter.Length; i++)
        {
            highlighter[i].SetActive(false);
        }
    }

    public void Advanced_One_Day() // forTest mode
    {
        debugTime = debugTime.Add(new TimeSpan(1, 0, 0, 0));
        Debug.Log(debugTime);
        Debug.Log("Days Passed : " + GetDaysPassed());
        CheckForDay();
    }
    public int dayy;
    private void CheckForDay()
    {
        int currentDay = GetDaysPassed();
        
        print("Current Day is : " + currentDay);
        for (int i = 0; i < rewards.Length; i++)
        {
            if (currentDay == (i))
            {
                CheckForReward(currentDay);
            }
        }
        dayy = currentDay;
    }

    CustomReward currentReward;
    public CarCustomization carCustomization;

    private void CheckForReward(int _day)
    {
        currentReward = rewards[_day];
        if (currentReward.hasReward)
        {
            if (!currentReward.rewardClaimed)
            {
                PlayerPrefs.SetInt("RewardGiven" + _day, 1);
                GiveReward(_day);
            }
        }
    }

    public bool showReward = false;
    public GameObject claimBtn;
    public void Claim()
    {
        claimBtn.SetActive(false);
        switch (dayy)
        {
            //Your Reward Logic here
            case 0:
                {
                    print("1");
                    headingTxt.text = "Day 1 reward";
                    claimButton.gameObject.SetActive(true);
                    //GameConstants.TotalCoins += 500;
                    PlayerData.currency += 1000;
                   // MenuManager.instance.playerCurrency.text = PlayerData.currency.ToString();
                    PlayerPrefs.SetString("ReceivedDate", DateTime.Now.ToString());
                    ShowReward(currentReward);
                    break;
                }
            case 1:
                {
                    print("2");
                    headingTxt.text = "Day 2 reward";
                    claimButton.gameObject.SetActive(true);
                    PlayerPrefs.SetInt(carCustomization.objPrefs[5], 1);
                    //GameConstants.TotalCoins += 1000;





                    PlayerPrefs.SetString("ReceivedDate", DateTime.Now.ToString());
                    ShowReward(currentReward);
                    break;
                }
            case 2:
                {
                    print("3");
                    headingTxt.text = "Day 3 reward";
                    claimButton.gameObject.SetActive(true);
                    PlayerPrefs.SetInt(carCustomization.rimPrefs[5], 1);
                    PlayerPrefs.SetString("ReceivedDate", DateTime.Now.ToString());
                    ShowReward(currentReward);
                    break;
                }
            case 3:
                {
                    print("4");
                    headingTxt.text = "Day 4 reward";
                    claimButton.gameObject.SetActive(true);
                    PlayerPrefs.SetInt(carCustomization.spoilerPrefs[3], 1);
                    PlayerPrefs.SetString("ReceivedDate", DateTime.Now.ToString());
                    ShowReward(currentReward);
                    break;
                }
            case 4:
                {
                    print("5");
                    headingTxt.text = "Day 5 reward";
                    claimButton.gameObject.SetActive(true);
                    PlayerPrefs.SetInt(carCustomization.rimPrefs[1], 1);
                    PlayerPrefs.SetString("ReceivedDate", DateTime.Now.ToString());
                    ShowReward(currentReward);
                    break;
                }
            case 5:
                {
                    headingTxt.text = "Day 6 reward";
                    claimButton.gameObject.SetActive(true);

                  //  MenuManager.instance.menuVehicles[3].unlocked = true;
                   // MenuManager.instance.menuVehicles[3].price = 0;

                    PlayerPrefs.SetString("ReceivedDate", DateTime.Now.ToString());
                    ShowReward(currentReward);
                    break;
                }
            case 6:
                {
                    headingTxt.text = "Day 7 reward";
                    claimButton.gameObject.SetActive(true);

                   // MenuManager.instance.menuVehicles[8].unlocked = true;
                   // MenuManager.instance.menuVehicles[8].price = 0;
                    PlayerPrefs.SetString("ReceivedDate", DateTime.Now.ToString());
                    ShowReward(currentReward);
                    break;
                }
            case 14:
                {
                    headingTxt.text = "Day 15 reward";
                    claimButton.gameObject.SetActive(true);

                   // MenuManager.instance.UnlockAllVehicles();

                    PlayerPrefs.SetString("ReceivedDate", DateTime.Now.ToString());
                    ShowReward(currentReward);
                    break;
                }
            case 29:
                {
                    headingTxt.text = "Day 30 reward";

                   // mainMenu.instance.UnlockAllGame();

                    claimButton.gameObject.SetActive(true);

                    PlayerPrefs.SetString("ReceivedDate", DateTime.Now.ToString());
                    ShowReward(currentReward);
                    break;
                }
            default:
                break;
        }

    }
    public GameObject[] highlighter;
    private void GiveReward(int _day)
    {
        currentReward = rewards[_day];
        DailyRewardsPanel.SetActive(true);
        DailyRewardsPanel.GetComponent<AudioSource>().PlayOneShot(Claim_sound);

        // highlighter[dayy].SetActive(true);
        for (int i = 0; i < highlighter.Length; i++)
        {
            highlighter[i].SetActive(false);
        }
        rewards[_day].highlighter.SetActive(true);
        claimBtn.SetActive(true);
        startTime = false;
     }

    public void OnClaimBtnClick()
    {
     
        RewardPanel.SetActive(false);
        currentReward.btnComponent.transform.GetChild(3).gameObject.SetActive(true);
        startTime = true;
        for (int i = 0; i < highlighter.Length; i++)
        {
            highlighter[i].SetActive(false);
        }
    }
    public void OnCloseBtnClick()
    {
        DailyRewardsPanel.SetActive(false);
    }

    public void ShowRewardsPanel()
    {
        DailyRewardsPanel.SetActive(true);
       
    }

    public string GetFormattedTime(TimeSpan span)
    {
        return string.Format("Next reward in: "+"{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);
    }

    public TimeSpan GetTimeDifference()
    {
        DateTime lastRewardTime = Convert.ToDateTime(PlayerPrefs.GetString("ReceivedDate"));
        TimeSpan difference = (lastRewardTime - DateTime.Now);
        difference = difference.Subtract(debugTime);
        return difference.Add(new TimeSpan(0, 24, 0, 0));
    }

    private void ShowReward(CustomReward currentReward)
    {
        rewardImg.sprite = currentReward.rewardImage;
        rewardTxt.text = currentReward.rewardString;

        RewardPanel.SetActive(true);
        gameObject.GetComponent<AudioSource>().clip = rewardClip;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
