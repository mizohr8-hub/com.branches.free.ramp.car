using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGSK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class CarCustomization : MonoBehaviour
{
    public MenuCamera menuCamera;
    public Transform rimTransform, spoilerTransform, objTransform, generalTransform;
    public Objs[] obj;
    public GameObject[] highlighted;
    int selectedRimID;
    int selectedObjID;
    int selectedSpoilerID;
    public string[] rimPrefs, spoilerPrefs, objPrefs;
    public int[] rimPirce, spilerPrice, objPrice;
    public Text priceText;
    public int buyID = 0;
    public GameObject notEnoughCoins, unlockAtDay,objPanel;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            PlayerPrefs.SetInt(rimPrefs[0], 1);
            PlayerPrefs.SetInt(spoilerPrefs[0], 1);
            PlayerPrefs.SetInt(objPrefs[0], 1);
            print("Spoiler" + PlayerPrefs.GetInt("SpoilerID"));
            print("Rim" + PlayerPrefs.GetInt("RimID"));
            RimsStart(PlayerPrefs.GetInt("RimID"));
            SpoilerStart(PlayerPrefs.GetInt("SpoilerID"));
            ObjsOnCar(PlayerPrefs.GetInt("ObjsID"));
        }
        else
        {
            print("Spoiler" + PlayerPrefs.GetInt("SpoilerID"));
            print("Rim" + PlayerPrefs.GetInt("RimID"));
            RimsGP(PlayerPrefs.GetInt("SpoilerID"));
            ObjsOnCarGP(PlayerPrefs.GetInt("ObjsID"));
            SpoilerGP(PlayerPrefs.GetInt("RimID"));
        }
        buyID = 1;
    }

    public void RimsStart(int id)
    {
        if (MenuManager.instance.vehicleIndex != 5)
        {
            for (int i = 0; i < obj[MenuManager.instance.vehicleIndex].rim1.Length; i++)
            {
                obj[MenuManager.instance.vehicleIndex].rim1[i].SetActive(false);
                obj[MenuManager.instance.vehicleIndex].rim2[i].SetActive(false);
                obj[MenuManager.instance.vehicleIndex].rim3[i].SetActive(false);
                obj[MenuManager.instance.vehicleIndex].rim4[i].SetActive(false);
            }
            obj[MenuManager.instance.vehicleIndex].rim1[id].SetActive(true);
            obj[MenuManager.instance.vehicleIndex].rim2[id].SetActive(true);
            obj[MenuManager.instance.vehicleIndex].rim3[id].SetActive(true);
            obj[MenuManager.instance.vehicleIndex].rim4[id].SetActive(true);
        }
    }
    public void SpoilerStart(int id)
    {
        if (MenuManager.instance.vehicleIndex != 5)
        {
            for (int i = 0; i < obj[MenuManager.instance.vehicleIndex].spoilers.Length; i++)
            {
                obj[MenuManager.instance.vehicleIndex].spoilers[i].SetActive(false);
            }
            obj[MenuManager.instance.vehicleIndex].spoilers[id].SetActive(true);
        }
    }

    public GameObject nextBtn, buyBtn;
    public GameObject applyRim, applySpoiler, applyObj;
    public Transform[] cars;
    public bool custimization=false;

    private void OnEnable()
    {
        custimization = true;
    }

    private void OnDisable()
    {
        custimization = false;
    }
    public void Rims(int id)
    {
        buyID = 1;
        selectedRimID = id;
        // menuCamera.target = rimTransform.transform;
        menuCamera.target.transform.DOLocalMove(rimTransform.transform.position, 1f);
        if (PlayerPrefs.GetInt(rimPrefs[id]) == 1)
        {
            if (PlayerPrefs.GetInt(spoilerPrefs[selectedSpoilerID]) == 0)
            {
                selectedSpoilerID = 0;
            }
            if (PlayerPrefs.GetInt(objPrefs[selectedObjID]) == 0)
            {
                selectedObjID = 0;
            }
            applyRim.SetActive(true);
            nextBtn.SetActive(false);
            buyBtn.SetActive(false);
            priceText.gameObject.SetActive(false);
        }
        else if (id == 5)
        {
            nextBtn.SetActive(false);
            buyBtn.SetActive(false);
            priceText.gameObject.SetActive(false);
            unlockAtDay.SetActive(true);
            unlockAtDay.transform.GetChild(0).GetComponent<Text>().text = "Unlock At Day 3!";
            applyRim.SetActive(false);
        }
        else if (id == 1)
        {
            nextBtn.SetActive(false);
            buyBtn.SetActive(false);
            priceText.gameObject.SetActive(false);
            unlockAtDay.SetActive(true);
            unlockAtDay.transform.GetChild(0).GetComponent<Text>().text = "Unlock At Day 5!";
            applyRim.SetActive(false);
        }
        else
        {
            buyBtn.SetActive(true);
            priceText.gameObject.SetActive(true);
            priceText.text = rimPirce[id].ToString();
            nextBtn.SetActive(false);
            applyRim.SetActive(false);
        }

        for (int i = 0; i < obj[MenuManager.instance.vehicleIndex].rim1.Length; i++)
        {
            obj[MenuManager.instance.vehicleIndex].rim1[i].SetActive(false);
            obj[MenuManager.instance.vehicleIndex].rim2[i].SetActive(false);
            obj[MenuManager.instance.vehicleIndex].rim3[i].SetActive(false);
            obj[MenuManager.instance.vehicleIndex].rim4[i].SetActive(false);
        }
        obj[MenuManager.instance.vehicleIndex].rim1[id].SetActive(true);
        obj[MenuManager.instance.vehicleIndex].rim2[id].SetActive(true);
        obj[MenuManager.instance.vehicleIndex].rim3[id].SetActive(true);
        obj[MenuManager.instance.vehicleIndex].rim4[id].SetActive(true);
    }
    public void ObjsOnCar(int id)
    {
        buyID = 3;
        //menuCamera.target = objTransform.transform;

        menuCamera.target.transform.DOLocalMove(objTransform.transform.position, 1f);
        if (MenuManager.instance.vehicleIndex != 5)
        {
            selectedObjID = id;
            for (int i = 0; i < obj[MenuManager.instance.vehicleIndex].objs.Length; i++)
            {
                obj[MenuManager.instance.vehicleIndex].objs[i].SetActive(false);
                highlighted[i].SetActive(false);
            }
            obj[MenuManager.instance.vehicleIndex].objs[id].SetActive(true);
            highlighted[id].SetActive(true);
        }
        if (PlayerPrefs.GetInt(objPrefs[id]) == 1)
        {
            if (PlayerPrefs.GetInt(rimPrefs[selectedRimID]) == 0)
            {
                selectedRimID = 0;
            }
            if (PlayerPrefs.GetInt(spoilerPrefs[selectedSpoilerID]) == 0)
            {
                selectedSpoilerID = 0;
            }
            nextBtn.SetActive(true);
            buyBtn.SetActive(false);
            priceText.text = "";
            priceText.gameObject.SetActive(false);


        }
        else if (id == 5) 
        {
            nextBtn.SetActive(false);
            buyBtn.SetActive(false);
            priceText.gameObject.SetActive(false);
            unlockAtDay.SetActive(true);
            unlockAtDay.transform.GetChild(0).GetComponent<Text>().text = "Unlock At Day 2!";
        }
        else
        {
            buyBtn.SetActive(true);
            nextBtn.SetActive(false);
            priceText.gameObject.SetActive(true);
            priceText.text = objPrice[id].ToString();
        }



        applySpoiler.SetActive(false);
        applyRim.SetActive(false);
        rimPanel.SetActive(false);
        spoilerPanel.SetActive(false);
    }
    public void ObjsOnCarGP(int id)
    {
        for (int i = 0; i < obj[0].objs.Length; i++)
        {
            obj[0].objs[i].SetActive(false);
        }
        obj[0].objs[id].SetActive(true);
    }
    public void RimsGP(int id)
    {
        for (int i = 0; i < obj[0].rim1.Length; i++)
        {
            obj[0].rim1[i].SetActive(false);
            obj[0].rim2[i].SetActive(false);
            obj[0].rim3[i].SetActive(false);
            obj[0].rim4[i].SetActive(false);
            obj[0].rim5[i].SetActive(false);
            obj[0].rim6[i].SetActive(false);
            obj[0].rim7[i].SetActive(false);
            obj[0].rim8[i].SetActive(false);
        }
        obj[0].rim1[PlayerPrefs.GetInt("RimID")].SetActive(true);
        obj[0].rim2[PlayerPrefs.GetInt("RimID")].SetActive(true);
        obj[0].rim3[PlayerPrefs.GetInt("RimID")].SetActive(true);
        obj[0].rim4[PlayerPrefs.GetInt("RimID")].SetActive(true);
        obj[0].rim5[PlayerPrefs.GetInt("RimID")].SetActive(true);
        obj[0].rim6[PlayerPrefs.GetInt("RimID")].SetActive(true);
        obj[0].rim7[PlayerPrefs.GetInt("RimID")].SetActive(true);
        obj[0].rim8[PlayerPrefs.GetInt("RimID")].SetActive(true);
    }
    public void SpoilerGP(int id)
    {
        for (int i = 0; i < obj[0].spoilers.Length; i++)
        {
            obj[0].spoilers[i].SetActive(false);
        }
        obj[0].spoilers[PlayerPrefs.GetInt("SpoilerID")].SetActive(true);
    }
    public void Spoilers(int id)
    {
        buyID = 2;
        // menuCamera.target = spoilerTransform.transform;
        menuCamera.target.transform.DOLocalMove(spoilerTransform.transform.position, 1f);
        //menuCamera.target = spoilerTransform.transform;
        if (MenuManager.instance.vehicleIndex != 5)
        {
            selectedSpoilerID = id;
            for (int i = 0; i < obj[MenuManager.instance.vehicleIndex].spoilers.Length; i++)
            {
                obj[MenuManager.instance.vehicleIndex].spoilers[i].SetActive(false);
            }
            obj[MenuManager.instance.vehicleIndex].spoilers[id].SetActive(true);
        }
        if (PlayerPrefs.GetInt(spoilerPrefs[id]) == 1)
        {
            if (PlayerPrefs.GetInt(rimPrefs[selectedRimID]) == 0)
            {
                selectedRimID = 0;
            }
            if (PlayerPrefs.GetInt(objPrefs[selectedObjID]) == 0)
            {
                selectedObjID = 0;
            }
            applySpoiler.SetActive(true);
            nextBtn.SetActive(false);
            buyBtn.SetActive(false);
            priceText.gameObject.SetActive(false);
            priceText.text = "";
        }

        else if (id == 3)
        {
            nextBtn.SetActive(false);
            buyBtn.SetActive(false);
            priceText.gameObject.SetActive(false);
            unlockAtDay.SetActive(true);
            unlockAtDay.transform.GetChild(0).GetComponent<Text>().text = "Unlock At Day 4!";
            applySpoiler.SetActive(false);
        }
        else
        {
            buyBtn.SetActive(true);
            priceText.gameObject.SetActive(true);
            priceText.text = rimPirce[id].ToString();
            nextBtn.SetActive(false);
            applySpoiler.SetActive(false);
        }
    }

    public void OnApply()
    {
        objPanel.SetActive(true);
        applySpoiler.SetActive(false);
        applyRim.SetActive(false);
        menuCamera.target.transform.DOLocalMove(objTransform.transform.position, 1f);
        nextBtn.SetActive(true);
    }
    public GameObject rimPanel, spoilerPanel;
    public void OpenRims()
    {
        if (rimPanel.activeInHierarchy)
        {
            objPanel.SetActive(true);
            applyRim.SetActive(false);
            rimPanel.SetActive(false);
            menuCamera.target.transform.DOLocalMove(objTransform.transform.position, 1f);
        }
        else
        {
            objPanel.SetActive(false);
            applySpoiler.SetActive(false);
            rimPanel.SetActive(true);
            spoilerPanel.SetActive(false);
        }
    }
    public void OpenSpoilers()
    {
        if (spoilerPanel.activeInHierarchy)
        {
            applySpoiler.SetActive(false);
            spoilerPanel.SetActive(false);
            menuCamera.target.transform.DOLocalMove(objTransform.transform.position, 1f);
            objPanel.SetActive(true);
        }
        else
        {
            applyRim.SetActive(false);
            rimPanel.SetActive(false);
            spoilerPanel.SetActive(true);
            objPanel.SetActive(false);
        }
    }


    public void SelectVehicle()
    {
        if (MenuManager.instance.vehicleIndex != 5)
        {
            print("Spoiler" + PlayerPrefs.GetInt("SpoilerID"));
            print("Rim" + PlayerPrefs.GetInt("RimID"));
            print("Objs" + PlayerPrefs.GetInt("ObjsID"));
            PlayerPrefs.SetInt("SpoilerID", selectedSpoilerID);
            PlayerPrefs.SetInt("RimID", selectedRimID);
            PlayerPrefs.SetInt("ObjsID", selectedObjID);
        }
        PlayerPrefs.SetInt("RimID", selectedRimID);
    }

    public void Buy()
    {
        if (buyID == 1)
        {
            if (PlayerData.currency>= rimPirce[selectedRimID])
            {
                PlayerData.currency -= rimPirce[selectedRimID];
                MenuManager.instance.playerCurrency.text = PlayerData.currency.ToString();
                PlayerPrefs.SetInt(rimPrefs[selectedRimID], 1);
                UpdateUI();
            }
            else
            {
                notEnoughCoins.SetActive(true);
            }
        }
        if (buyID == 2)
        {
            if (PlayerData.currency>= spilerPrice[selectedSpoilerID])
            {
                PlayerData.currency -= spilerPrice[selectedSpoilerID];
                MenuManager.instance.playerCurrency.text = PlayerData.currency.ToString();
                PlayerPrefs.SetInt(spoilerPrefs[selectedSpoilerID], 1);
                UpdateUI();
            }
            else
            {
                notEnoughCoins.SetActive(true);
            }
        }
        if (buyID == 3)
        {
            if (PlayerData.currency>= objPrice[selectedObjID])
            {
                PlayerData.currency -= objPrice[selectedObjID];
                MenuManager.instance.playerCurrency.text = PlayerData.currency.ToString();
                PlayerPrefs.SetInt(objPrefs[selectedObjID], 1);
                UpdateUI();
            }
            else
            {
                notEnoughCoins.SetActive(true);
            }
        }
    }

    public void CustomizeBac()
    {
      //  menuCamera.target = objTransform.transform;

        menuCamera.target.transform.DOLocalMove(generalTransform.transform.position, 1f);
    }


    void UpdateUI()
    {
        buyBtn.SetActive(false);
        nextBtn.SetActive(true);
        priceText.gameObject.SetActive(false);
    }


    [System.Serializable]
    public class Objs
    {
        public GameObject[] rim1, rim2, rim3, rim4, rim5, rim6, rim7, rim8, spoilers, objs;

    }
}
