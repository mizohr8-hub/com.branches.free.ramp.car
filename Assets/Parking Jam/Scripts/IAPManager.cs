using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class IAPManager : MonoBehaviour
{
    public MainMenuScript main;
    public InGameStore store;
    public void PurchaseComplete(Product p)
    {
        Debug.Log("purchase ID is " + p.definition.id);
        switch (p.definition.id)
        {
            case "com.mizo.parking.jam.remove.ads":
                PlayerPrefs.SetInt("RemoveAds", 1);
                if (main != null)
                {
                    main.MMCancelBanner();
                }
                break;
            case "com.mizo.parking.jam.ten.thousand.coins":
                if (store!=null)
                {
                    store.CoinBundlePurchase(0);
                }
                break;
            case "com.mizo.parking.jam.twenty.five.thousand.coins":
                if (store!=null)
                {
                    store.CoinBundlePurchase(1);
                }
                break;
            case "com.mizo.parking.jam.thirty.thousand.coins":
                if (store!=null)
                {
                    store.CoinBundlePurchase(2);
                }
                break;
            case "com.mizo.parking.jam.fifty.thousand.coins":
                if (store!=null)
                {
                    store.CoinBundlePurchase(3);
                }
                break;
            case "com.mizo.parking.jam.env.two":
                PlayerPrefs.SetInt("Env" + 1 + "Purchased",1);
                if (store != null)
                {
                    store.EnvironmentUnlocking();
                }
                break;
            case "com.mizo.parking.jam.env.three":
                PlayerPrefs.SetInt("Env" + 2 + "Purchased", 1);
                if (store != null)
                {
                    store.EnvironmentUnlocking();
                }
                break;
            case "com.mizo.parking.jam.env.four":
                PlayerPrefs.SetInt("Env" + 3 + "Purchased", 1);
                if (store != null)
                {
                    store.EnvironmentUnlocking();
                }
                break;
            case "com.mizo.parking.jam.env.five":
                PlayerPrefs.SetInt("Env" + 4 + "Purchased", 1);
                if (store != null)
                {
                    store.EnvironmentUnlocking();
                }
                break;
            case "com.mizo.parking.jam.env.six":
                PlayerPrefs.SetInt("Env" + 5 + "Purchased", 1);
                if (store != null)
                {
                    store.EnvironmentUnlocking();
                }
                break;            
            case "com.mizo.parking.jam.env.seven":
                PlayerPrefs.SetInt("Env" + 6 + "Purchased", 1);
                if (store != null)
                {
                    store.EnvironmentUnlocking();
                }
                break; 
            case "com.mizo.parking.jam.env.eight":
                PlayerPrefs.SetInt("Env" + 7 + "Purchased", 1);
                if (store != null)
                {
                    store.EnvironmentUnlocking();
                }
                break; 
            case "com.mizo.parking.jam.env.nine":
                PlayerPrefs.SetInt("Env" + 8 + "Purchased", 1);
                if (store != null)
                {
                    store.EnvironmentUnlocking();
                }
                break;
            default:
                break;

        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {

    }
}
