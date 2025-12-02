using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    public static CarSelection instance;
    public cars[] _cars;
    public GameObject BuyButton, EquipeButton/*, notEnoughCoin*/;
    public GameObject[] Model/*, isSelected*/;
    public int carID;
    public Text carName/*, equipedText*/;
    public GameObject carPrice/*, gemPrice*/;
   // public string[] names;
    public GameObject notEnoughCoins/*, unlockAllcarsBtn*/;
    public Text descriptionText;
    public string[] descriptions;
    public Text totalCoins;

    public GameObject[] tryNow;
    public GameObject try1, try2, try3, try4, try5;

    private void OnEnable()
    {
        AllcarsUnlocked();
        //consola
        //ConsoliAds.onRewardedVideoAdCompletedEvent += GetCoins;
        //ConsoliAds.onRewardedVideoAdCompletedEvent -= GetReward;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        print("Total Guns: " + PlayerPrefs.GetInt("TotalGuns"));
        //print("car Name Pistol:" + PlayerPrefs.GetInt(names[0]));
    }

    private void Start()
    {
        unlockCars();
        print("carID" + carID);
        Updatecar();
        //for (int i = 0; i < Model.Length; i++)
        //{
        //    Model[i].SetActive(false);
        //    isSelected[i].SetActive(false);
        //}
        Model[carID].SetActive(true);
       // isSelected[carID].SetActive(true);
        descriptionText.text = descriptions[carID];
        AllcarsUnlocked();
    }
    public void GetCoins()
    {
        //int currency = PlayerPrefs.GetInt("Currency");
        //currency = currency + 1000;
        //PlayerPrefs.SetInt("Currency", currency);
        //print("Video completed");

        tryNow[watchVideoID].SetActive(false);
        print("Try Button Off" + watchVideoID + "Deactivated");
    }
    
    public void unlockCars()
    {
        if(PlayerPrefs.GetInt("UnlockAllCars")==1)
        {
            try1.SetActive(false);
            try2.SetActive(false);
            try3.SetActive(false);
            try4.SetActive(false);
            try5.SetActive(false);
        }
        
    
    }
    public void carInapp()
    {
        PlayerPrefs.SetInt("UnlockAllCars", 1);
    }


    public void GetReward()
    {
        PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 1000);
        RGSK.MenuManager.instance.playerCurrency.text = PlayerPrefs.GetInt("Currency").ToString();
        print("Video completed");


    }
    private void OnDisable()
    {
        //consola
        //ConsoliAds.onRewardedVideoAdCompletedEvent -= GetCoins;
        //ConsoliAds.onRewardedVideoAdCompletedEvent -= GetReward;
    }

    public int watchVideoID;

    public void ShowVideo(int id)
    {
        watchVideoID = id;
        //consola
        //if (ConsoliAds.Instance.IsRewardedVideoAvailable(0))
        //{
        //    ConsoliAds.Instance.ShowRewardedVideo(0);
        //}
        //else
        //{
        //    //noVideoAvailablePanel.SetActive(true);
        //    print("Video not available");
        //}
        GetCoins();
    }
    public void ShowVideo()
    {
        //consola

        //if (ConsoliAds.Instance.IsRewardedVideoAvailable(0))
        //{
        //    ConsoliAds.Instance.ShowRewardedVideo(0);
        //}
        //else
        //{
        //    //noVideoAvailablePanel.SetActive(true);
        //    print("Video not available");
        //}
    }



    void Updatecar()
    {

        carPrice.SetActive(true);
        //gemPrice.SetActive(true);
        carName.text = _cars[carID].carName;
        carPrice.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Price : "+_cars[carID].Price.ToString();
      //  gemPrice.transform.GetChild(0).gameObject.GetComponent<Text>().text = _cars[carID].GemPrice.ToString();
        //for (int i = 0; i < 4; i++)
        //{
        //    print("Sliders: "+_cars[carID].carLevel[i]);
        //}
        if (_cars[carID].Price != 0)
        {
            BuyButton.SetActive(true);
            carPrice.SetActive(true);
            EquipeButton.SetActive(false);
           // equipedText.text = "";
        }
        else
        {
            carPrice.SetActive(false);
           // gemPrice.SetActive(false);
            BuyButton.SetActive(false);
            EquipeButton.SetActive(true);
            if (PlayerPrefs.GetInt("Equiped") == carID)
            {
                EquipeButton.SetActive(true);
                //equipedText.text = "Equipped";
            }
            else
            {
                EquipeButton.SetActive(true);
                //equipedText.text = "";
            }

        }

    }

    public void Buycar()
    {
        int coins = PlayerPrefs.GetInt("Total Coins");

        if (coins >= _cars[carID].Price)
        {
            coins -= _cars[carID].Price;
            PlayerPrefs.SetInt("Total Coins", PlayerPrefs.GetInt("Total Coins") - _cars[carID].Price);
            totalCoins.text= PlayerPrefs.GetInt("Total Coins").ToString();
            print(PlayerPrefs.GetInt("Total Coins"));
            _cars[carID].Price = 0;
          //  _cars[carID].GemPrice = 0;
           // PlayerPrefs.SetInt(names[carID], 1);
           // print("car Name:" + PlayerPrefs.GetInt(names[carID]));
            EquipeButton.SetActive(true);
            print("Done");
            //PlayerPrefs.SetInt("TotalGuns", PlayerPrefs.GetInt("TotalGuns") + 1);

        }
        else
        {
            notEnoughCoins.SetActive(true);
            notEnoughCoins.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Not enough coins!";
        }
        Updatecar();
    }
    public void BuycarWithGems()
    {
        int coins = PlayerPrefs.GetInt("TotalGems");

        if (coins >= _cars[carID].GemPrice)
        {
            coins -= _cars[carID].GemPrice;
            PlayerPrefs.SetInt("TotalGems", PlayerPrefs.GetInt("TotalGems") - _cars[carID].GemPrice);
            _cars[carID].Price = 0;
            _cars[carID].GemPrice = 0;
           // PlayerPrefs.SetInt(names[carID], 1);
           // print("car Name:" + PlayerPrefs.GetInt(names[carID]));
            EquipeButton.SetActive(true);
            print("Done");

        }
        else
        {
            notEnoughCoins.SetActive(true);
            notEnoughCoins.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Not enough gems!";
        }
        Updatecar();
    }
    public void BuycarGift()
    {
        _cars[carID].Price = 0;
        EquipeButton.SetActive(true);
        print("Done");
        PlayerPrefs.SetInt("TotalGuns", PlayerPrefs.GetInt("TotalGuns") + 1);
        Updatecar();
    }


    //IEnumerator EnoughCoin()
    //{
    //    notEnoughCoin.SetActive(true);
    //    yield return new WaitForSeconds(2f);
    //    notEnoughCoin.SetActive(false);
    //}

    public void carClick(int number)
    {
        Model[carID].SetActive(false);
        Model[number].SetActive(true);
        descriptionText.text = descriptions[number];
        //isSelected[carID].SetActive(false);
        //isSelected[number].SetActive(true);
        carID = number;
        PlayerPrefs.SetInt("ActiveGun", carID);
        Updatecar();
    }

    public void OnSelectGun()
    {
        PlayerPrefs.SetInt("Currentcar", carID);
    }

    public void UnlockAllcars()
    {
        PlayerPrefs.SetInt("AllcarsUnlocked", 1);
        AllcarsUnlocked();
    }

    void AllcarsUnlocked()
    {
        if (PlayerPrefs.GetInt("AllcarsUnlocked") == 1)
        {
            for (int i = 0; i < _cars.Length; i++)
            {
                _cars[i].Price = 0;
                _cars[i].GemPrice = 0;
               // PlayerPrefs.SetInt(names[i], 1);
                Updatecar();
            }
            //unlockAllcarsBtn.SetActive(false);
        }
    }



    [System.Serializable]
    public class cars
    {
        public int carID;
        public string carName;
        public int carPrice, gemPrice;
        //public int[] carLevel = new int[4];
        public int Price
        {
            get
            {
                carPrice = PlayerPrefs.GetInt("WPrice" + carID, carPrice);
                return carPrice;
            }
            set
            {
                carPrice = value;
                PlayerPrefs.SetInt("WPrice" + carID, carPrice);
            }
        }
        public int GemPrice
        {
            get
            {
                gemPrice = PlayerPrefs.GetInt("GPrice" + carID, gemPrice);
                return gemPrice;
            }
            set
            {
                gemPrice = value;
                PlayerPrefs.SetInt("GPrice" + carID, gemPrice);
            }
        }
    }
}