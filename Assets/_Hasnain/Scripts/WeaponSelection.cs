using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class WeaponSelection : MonoBehaviour
{
    public static WeaponSelection instance;
    public Weapons[] _Weapons;
    public GameObject BuyButton, EquipeButton, notEnoughCoin, weaponObject, shop, model;
    public GameObject[] Model, isSelected;
    public Text[] WeaponLevels;
    public Slider[] WeaponLevels_Slider;
    public int WeaponID;
    public Text WeaponName, WeaponPrice, equipedText;
    public string[] names;
    public GameObject unlockAllGuns;



    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("AllWeapons") == 1)
        {
            unlockAllGuns.SetActive(false);
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        print("Total Guns: " + PlayerPrefs.GetInt("TotalGuns"));



        PlayerPrefs.SetInt(names[0], 1);
        print("Weapon Name Pistol:" + PlayerPrefs.GetInt(names[0]));




    }



    private void Start()
    {
        NewGun();
        print("WeaponID" + WeaponID);
        UpdateWeapon();
        for (int i = 0; i < Model.Length; i++)
        {
            Model[i].SetActive(false);
            isSelected[i].SetActive(false);
        }
        Model[WeaponID].SetActive(true);
        isSelected[WeaponID].SetActive(true);




        //PlayerPrefs.GetInt("TotalGuns", 1);
    }




    void NewGun()
    {
        if (PlayerPrefs.GetInt("AKGUN") == 1)
        {
            panel1.SetActive(true);
        }
    }





    void UpdateWeapon()
    {



        WeaponName.text = _Weapons[WeaponID].weaponName;
        WeaponPrice.text = _Weapons[WeaponID].Price.ToString();
        for (int i = 0; i < 4; i++)
        {
            //WeaponLevels[i].text = _Weapons[WeaponID].weaponLevel[i].ToString();
            WeaponLevels_Slider[i].value = _Weapons[WeaponID].weaponLevel[i];
            //WeaponLevels_Bar[i].fillAmount=(_Weapons[WeaponID].weaponLevel[i]/100f);
        }
        if (_Weapons[WeaponID].Price != 0)
        {
            weaponObject.SetActive(true);
            BuyButton.SetActive(true);
            EquipeButton.SetActive(false);
            equipedText.text = "";
            //PlayButton.SetActive(false);
            //UpdradeButton.SetActive(false);
            //LockButton.SetActive(true);
        }
        else
        {
            WeaponPrice.text = "";
            weaponObject.SetActive(false);
            BuyButton.SetActive(false);
            //LockButton.SetActive(false);
            //UpdradeButton.SetActive(true);
            EquipeButton.SetActive(true);
            //PlayButton.SetActive(true);
            if (PlayerPrefs.GetInt("Equiped") == WeaponID)
            {
                EquipeButton.SetActive(true);
                equipedText.text = "Equipped";
                //PlayButton.SetActive(true);
            }
            else
            {
                EquipeButton.SetActive(true);
                equipedText.text = "";
                //PlayButton.SetActive(false);
            }



        }



    }






    public void Equip()
    {
        PlayerPrefs.SetInt("AKGUN", 2);
        PlayerPrefs.SetInt("Equiped", WeaponID);
        equipedText.text = "Equipped";
        EquipeButton.SetActive(false);
        panel2.SetActive(false);
    }






    public void BuyWeapon()
    {




        if (PlayerPrefs.GetInt("AKGUN") == 1)
        {
            _Weapons[2].Price = 0;
            PlayerPrefs.SetInt(names[2], 1);
            print("Weapon Name:" + PlayerPrefs.GetInt(names[2]));
            //EquipeWeapon();
            EquipeButton.SetActive(true);
            //equipedText.text = "";
            //SoundsManager.Instance.Play_BuySound();
            print("Done");
            PlayerPrefs.SetInt("TotalGuns", PlayerPrefs.GetInt("TotalGuns") + 1);
            UpdateWeapon();
        }
        else
        {
            int coins = 0;



            if (coins >= _Weapons[WeaponID].Price)
            {
                coins -= _Weapons[WeaponID].Price;
                PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - _Weapons[WeaponID].Price);
                //MyCoinManager.Instance.SetCoins(coins - _Weapons[WeaponID].Price);
                _Weapons[WeaponID].Price = 0;
                PlayerPrefs.SetInt(names[WeaponID], 1);
                print("Weapon Name:" + PlayerPrefs.GetInt(names[WeaponID]));
                //EquipeWeapon();
                EquipeButton.SetActive(true);
                //equipedText.text = "";
                //SoundsManager.Instance.Play_BuySound();
                print("Done");
                PlayerPrefs.SetInt("TotalGuns", PlayerPrefs.GetInt("TotalGuns") + 1);
            }
            else
            {
                model.SetActive(false);
                shop.SetActive(true);
                //StartCoroutine(EnoughCoin());
            }
            UpdateWeapon();



        }






    }
    public void BuyWeaponGift()
    {
        _Weapons[WeaponID].Price = 0;
        EquipeButton.SetActive(true);
        print("Done");
        PlayerPrefs.SetInt("TotalGuns", PlayerPrefs.GetInt("TotalGuns") + 1);
        UpdateWeapon();
    }




    IEnumerator EnoughCoin()
    {
        notEnoughCoin.SetActive(true);
        yield return new WaitForSeconds(2f);
        notEnoughCoin.SetActive(false);
    }




    public GameObject panel, panel1, panel2, img2;
    public void WeaponClick(int number)
    {
        if (PlayerPrefs.GetInt("AKGUN") == 1)
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            Model[WeaponID].SetActive(false);
            Model[number].SetActive(true);
            isSelected[WeaponID].SetActive(false);
            isSelected[number].SetActive(true);
            WeaponID = number;
            PlayerPrefs.SetInt("ActiveGun", WeaponID);
            UpdateWeapon();
        }
        else
        {
            Model[WeaponID].SetActive(false);
            Model[number].SetActive(true);
            isSelected[WeaponID].SetActive(false);
            isSelected[number].SetActive(true);
            WeaponID = number;
            PlayerPrefs.SetInt("ActiveGun", WeaponID);
            UpdateWeapon();
        }

    }




    [System.Serializable]
    public class Weapons
    {
        public int weaponID;
        //public int skinID;
        public string weaponName;
        public int weaponPrice;
        public int[] weaponLevel = new int[4];
        //public int[] increamentValue = new int[4];
        //public int[] maximizeValue = new int[4];
        //public int UpgradePrice;
        //public GameObject[] gunParts;
        //public Material[] gunMat;
        //public Material origionalMat;
        public int Price
        {
            get
            {
                weaponPrice = PlayerPrefs.GetInt("WPrice" + weaponID, weaponPrice);
                return weaponPrice;
            }
            set
            {
                weaponPrice = value;
                PlayerPrefs.SetInt("WPrice" + weaponID, weaponPrice);
            }
        }
        //public int SPrice
        //{
        //    get
        //    {
        //        skinPrice = PlayerPrefs.GetInt("SPrice" + skinID, skinPrice);
        //        return skinPrice;
        //    }
        //    set
        //    {
        //        skinPrice = value;
        //        PlayerPrefs.SetInt("SPrice" + skinID, skinPrice);
        //    }
        //}
    }





}