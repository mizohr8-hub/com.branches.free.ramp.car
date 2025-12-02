using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using Watermelon;

public class InGameStore : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("CoinBundles")]
    public Button tenThousand;
    public Button twentyFiveThousand, thirtyThousand, fiftyThousand;

    [Header("Powers")]
    public int[] powerPrices;
    public Button[] powerPurchaseBtn;

    [Header("Environments")]
    public IAPButton[] environmentIAP;
    public Button[] environmentSelectButton;
    public GameObject[] envHighlighters;
    public Text[] environmentStatus;
 
    public Text notEnoughCoins;
    public Text powerAddition;
    public Text[] coinDislay;
    void Start()
    {
        for (int i = 0; i < powerPurchaseBtn.Length; i++)
        {
            int y = powerPrices[i];
            powerPurchaseBtn[i].onClick.AddListener(() => PurchasePower(y));
        }


        for (int i = 0; i < environmentSelectButton.Length; i++)
        {
            int z = i;
            environmentSelectButton[i].onClick.AddListener(() => EnvironmentSelection(z));
        }
        PlayerPrefs.SetInt("Env0Purchased", 1);

        envHighlighters.ForEach((GameObject go) => go.SetActive(false));
        envHighlighters[PlayerPrefs.GetInt("TucTucSelectedEnv")].SetActive(true);
        for (int i = 0; i < environmentStatus.Length; i++)
        {
            if (PlayerPrefs.GetInt("TucTucSelectedEnv") == i)
            {
                environmentStatus[i].text = "Selected";
                environmentSelectButton[i].gameObject.SetActive(true);

            }
            else if (PlayerPrefs.GetInt("Env" + i + "Purchased") == 1)
            {
                environmentStatus[i].text = "Open";
                environmentSelectButton[i].gameObject.SetActive(true);

            }
        }

        for (int i = 0; i < environmentIAP.Length; i++)
        {
            if (PlayerPrefs.GetInt("Env" + (i+1) + "Purchased") == 1)
            {
                environmentIAP[i].gameObject.SetActive(false);
            }
        }
    }

    public void EnvironmentUnlocking()
    {
        envHighlighters.ForEach((GameObject go) => go.SetActive(false));
        envHighlighters[PlayerPrefs.GetInt("TucTucSelectedEnv")].SetActive(true);
        for (int i = 0; i < environmentStatus.Length; i++)
        {
            if (PlayerPrefs.GetInt("TucTucSelectedEnv") == i)
            {
                environmentSelectButton[i].gameObject.SetActive(true);
                environmentStatus[i].text = "Selected";
            }
            else if (PlayerPrefs.GetInt("Env" + i + "Purchased") == 1)
            {
                environmentSelectButton[i].gameObject.SetActive(true);
                environmentStatus[i].text = "Open";
            }
        }
    }
    public void CoinBundlePurchase(int cbi)
    {
        if (cbi==0) //10000
        {
            PrefsSettings.SetInt(PrefsSettings.Key.CoinsCountID, PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID) + 10000);
            coinDislay.ForEach((Text txt) => txt.text = PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID).ToString());
        }
        else if (cbi==1)//25000
        {
            PrefsSettings.SetInt(PrefsSettings.Key.CoinsCountID, PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID) + 25000);
            coinDislay.ForEach((Text txt) => txt.text = PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID).ToString());
        }
        else if (cbi == 2)//30000
        {
            PrefsSettings.SetInt(PrefsSettings.Key.CoinsCountID, PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID) + 30000);
            coinDislay.ForEach((Text txt) => txt.text = PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID).ToString());
        }
        else if (cbi == 3)//50000
        {
            PrefsSettings.SetInt(PrefsSettings.Key.CoinsCountID, PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID) + 50000);
            coinDislay.ForEach((Text txt) => txt.text = PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID).ToString());
        }
    }

    public void PurchasePower(int price)
    {
        if (price<= PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID))
        {
            PrefsSettings.SetInt(PrefsSettings.Key.CoinsCountID, PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID) - price);
            if (price== powerPrices[0]) //obstacles
            {
                PlayerPrefs.SetInt("PowersObstacles", PlayerPrefs.GetInt("PowersObstacles") + 5);
                powerAddition.gameObject.SetActive(true);
            }
            else if (price == powerPrices[1]) //npc
            {
                PlayerPrefs.SetInt("PowersNPC", PlayerPrefs.GetInt("PowersNPC") + 5);
                powerAddition.gameObject.SetActive(true);
            }
            else if (price == powerPrices[2]) //missile
            {
                PlayerPrefs.SetInt("PowersMissile", PlayerPrefs.GetInt("PowersMissile") + 5);
                powerAddition.gameObject.SetActive(true);
            }
            else if (price == powerPrices[3]) //hammer
            {
                PlayerPrefs.SetInt("PowersHammer", PlayerPrefs.GetInt("PowersHammer") + 5);
                powerAddition.gameObject.SetActive(true);
            }
            else if (price == powerPrices[4]) //roller
            {
                PlayerPrefs.SetInt("PowersRoller", PlayerPrefs.GetInt("PowersRoller") + 5);
                powerAddition.gameObject.SetActive(true);
            }

            coinDislay.ForEach((Text txt) => txt.text = PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID).ToString());
        }
        else
        {
            notEnoughCoins.gameObject.SetActive(true);
        }
    }

    public void EnvironmentSelection(int envId)
    {
        if (envId==0)
        {
            PlayerPrefs.SetInt("TucTucSelectedEnv", envId);
            envHighlighters.ForEach((GameObject go) => go.SetActive(false));
            envHighlighters[envId].SetActive(true);

            for (int i = 0; i < environmentStatus.Length; i++)
            {
                if (envId==i)
                {
                    environmentStatus[i].text = "Selected";
                }
                else if (PlayerPrefs.GetInt("Env" + i + "Purchased")==1)
                {
                    environmentStatus[i].text = "Open";
                }
            }
            
        }
        else if (envId > 0)
        {
            if (PlayerPrefs.GetInt("Env"+envId+"Purchased") == 0)
            {
                
                //
            }
            else
            {
                PlayerPrefs.SetInt("TucTucSelectedEnv", envId);
                envHighlighters.ForEach((GameObject go) => go.SetActive(false));
                envHighlighters[envId].SetActive(true);

                for (int i = 0; i < environmentStatus.Length; i++)
                {
                    if (envId == i)
                    {
                        environmentStatus[i].text = "Selected";
                    }
                    else if (PlayerPrefs.GetInt("Env" + i + "Purchased") == 1)
                    {
                        environmentStatus[i].text = "Open";
                    }
                }
            }
        }
        coinDislay.ForEach((Text txt) => txt.text = PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID).ToString());
    }
}
