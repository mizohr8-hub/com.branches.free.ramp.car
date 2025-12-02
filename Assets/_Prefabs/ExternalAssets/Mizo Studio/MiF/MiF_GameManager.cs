using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiF_Plugin.GameManager{
	
public class MiF_GameManager  {

		private static MiF_GameManager _instance;

		private MiF_GameManager()
		{
			
		}

		public static MiF_GameManager Instance
		{
			get
			{
				if (_instance==null)
				{
					_instance = new MiF_GameManager();
				}
				return _instance;
			}
		}


		public void showBanner(int indexNo)
		{
#if INIT_MiF
			            if (AdsManager.Instance)
            {
                AdsManager.Instance.ShowAdaptive();
            }		
#endif
		}

		public void showInterstitial(int indexNo)
		{
			#if INIT_MiF

            if (AdsManager.Instance)
            {
                AdsManager.Instance.ShowInterstitial();
            }			
			#endif
		}

		public void GameAnalyticeEventStart(string eventString)
		{
#if INIT_MiF

			GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start,eventString);
#endif

		}
		public void GameAnalyticeEventEnd(string eventString)
		{
#if INIT_MiF

			GameAnalyticsSDK.GameAnalytics.NewProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.End,eventString);
#endif

		}

		public void BuyProduct(int productID)
		{
#if INIT_MiF

			GSF_InAppController.Instance.BuyInAppProduct(productID);
#endif

		}
		public void HideBanner()
		{
#if INIT_MiF
				ConsoliAds.Instance.HideBanner ();
#endif

		}

		public bool adsRemoved()
		{
			if (PlayerPrefs.GetInt ("RemoveAds") == 0)
				return false;
			else
				return true;
		}

		public void showMoreFunURL()
		{
			
#if INIT_MiF
			Application.OpenURL (ConsoliAds.Instance.MoreFunURL ());
#endif
		}

		public void showRateUsURL()
		{
#if INIT_MiF
			Application.OpenURL (ConsoliAds.Instance.RateUsURL ());
#endif
		}


	
	}
}