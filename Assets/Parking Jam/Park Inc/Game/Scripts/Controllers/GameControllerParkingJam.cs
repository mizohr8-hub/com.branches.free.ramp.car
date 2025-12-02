#pragma warning disable 649

using UnityEngine;
using Watermelon;
using UnityEngine.EventSystems;
using System;

public class GameControllerParkingJam : MonoBehaviour
{
    private static GameControllerParkingJam instance;

    [SerializeField] LevelDatabase levelDatabase;
    [SerializeField] GameConfigurations gameConfigurations;
    public static GameConfigurations GameConfigurations => instance.gameConfigurations;

    public static LevelDatabase LevelDatabase => instance.levelDatabase;
    public static int CurrentLevelId 
    { 
        get => PrefsSettings.GetInt(PrefsSettings.Key.CurrentLevelID);
        private set => PrefsSettings.SetInt(PrefsSettings.Key.CurrentLevelID, value);
    }

    public static int ActualLevelId
    {
        get => PrefsSettings.GetInt(PrefsSettings.Key.ActualLevelID);
        private set => PrefsSettings.SetInt(PrefsSettings.Key.ActualLevelID, value);
    }

    public static int CarsSkinId
    {
        get => PrefsSettings.GetInt(PrefsSettings.Key.CarSkinID);
        private set => PrefsSettings.SetInt(PrefsSettings.Key.CarSkinID, value);
    }

    public static int EnvironmentSkinId
    {
        get => PrefsSettings.GetInt(PrefsSettings.Key.EnvironmentSkinID);
        private set => PrefsSettings.SetInt(PrefsSettings.Key.EnvironmentSkinID, value);
    }

    public static int CoinsCount
    {
        get => PrefsSettings.GetInt(PrefsSettings.Key.CoinsCountID);
        private set => PrefsSettings.SetInt(PrefsSettings.Key.CoinsCountID, value);
    }

    private static CarSkinProduct characterSkinProduct;
    public static CarSkinProduct CharacterSkinProduct => characterSkinProduct;

    private static EnvironmentSkinProduct environmentSkinProduct;
    public static EnvironmentSkinProduct EnvironmentSkinProduct => environmentSkinProduct;

    public static bool StartStage { get; private set; }
    public static bool WinStage { get; private set; }
    public static int TurnsAfterRewardVideo { get; set; }
    public static bool IsBossLevel { get; set; }
    public static bool IsHammerAttack { get; set; }

    public static bool IsMissileAttack { get; set; }

    public static bool IsRollerAttack { get; set; }
    public void NPCOff()
    {
        if (PlayerPrefs.GetInt("PowersNPC") == 0)
        {
            PlayerPrefs.SetInt("BuyOrVidForPower", 1); // this pref is used to check which power will have increment
            UIController.GameUI.PowerGainOptions.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("PowersNPC", PlayerPrefs.GetInt("PowersNPC") - 1);
            UIController.GameUI.PowersDisabled();
            LevelObjectsSpawner.DisableNPCs();
            Invoke(nameof(TurnOnAllNPCs), 5f);
        }
    }

    public void ObstaclesOff()
    {
        if (PlayerPrefs.GetInt("PowersObstacles")==0)
        {
            PlayerPrefs.SetInt("BuyOrVidForPower", 0);
            UIController.GameUI.PowerGainOptions.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("PowersObstacles", PlayerPrefs.GetInt("PowersObstacles") - 1);
            UIController.GameUI.PowersDisabled();
            LevelObjectsSpawner.DisableObstacles();
            Invoke(nameof(TurnOnAllObjects), 5f);
        }
    }

    void TurnOnAllObjects()
    {
        UIController.GameUI.PowersEnabled();
        LevelObjectsSpawner.TurnOnObstacles();
    }

    void TurnOnAllNPCs()
    {
        UIController.GameUI.PowersEnabled();
        LevelObjectsSpawner.TurnOnNPCs();
    }
    public void PrepareRollerAttack()
    {
        if (PlayerPrefs.GetInt("PowersRoller") == 0)
        {
            PlayerPrefs.SetInt("BuyOrVidForPower", 4);
            UIController.GameUI.PowerGainOptions.SetActive(true);
        }
        else
        {
            UIController.GameUI.PowersDisabled();
            IsRollerAttack = true;
            PlayerPrefs.SetInt("PowersRoller", PlayerPrefs.GetInt("PowersRoller") - 1);
            LevelController.Environment.RollerArray.ForEach((GameObject go) => go.SetActive(true));
            StartCoroutine(CameraControllerParkingJam.ChangeFOVForRollerAttack());
        }
    }
    public void PrepareHammerAttack()
    {
        if (PlayerPrefs.GetInt("PowersHammer") == 0)
        {
            PlayerPrefs.SetInt("BuyOrVidForPower", 3);
            UIController.GameUI.PowerGainOptions.SetActive(true);
        }
        else
        {
            UIController.GameUI.PowersDisabled();
            IsHammerAttack = true;
            PlayerPrefs.SetInt("PowersHammer", PlayerPrefs.GetInt("PowersHammer") - 1);
            LevelObjectsSpawner.TurnOnTargetSelectOfCars();
        }
    }

    public void PrepareMissileAttack()
    {
        if (PlayerPrefs.GetInt("PowersMissile")==0)
        {
            PlayerPrefs.SetInt("BuyOrVidForPower",2);
            UIController.GameUI.PowerGainOptions.SetActive(true);
        }
        else
        {
            UIController.GameUI.PowersDisabled();
            IsMissileAttack = true;
            PlayerPrefs.SetInt("PowersMissile", PlayerPrefs.GetInt("PowersMissile") - 1);
            LevelObjectsSpawner.TurnOnTargetSelectOfCars();
        }
    }
    private void OnEnable()
    {
        //AdsManager.ExtraInterstitialReadyConditions += ExtraInterstitialCondition; //bilal.ads
        StoreController.OnProductSelected += OnProductSelected;
    }
    
    private void OnDisable()
    {
        //AdsManager.ExtraInterstitialReadyConditions -= ExtraInterstitialCondition; //bilal.ads
        StoreController.OnProductSelected -= OnProductSelected;
    }

    private bool ExtraInterstitialCondition()
    {
        if(TurnsAfterRewardVideo <= 4)
        {
            Debug.Log("[AdsManager]: Custom condition - TurnsAfterRewardVideo <= 4");

            return false;
        }

        return true;
    }

    private void Start()
    {
        instance = this;

        TurnsAfterRewardVideo = 5;

        CarSkinProduct characterSkinProduct = StoreController.GetSelectedProduct(StoreProductType.CharacterSkin) as CarSkinProduct;
        GameControllerParkingJam.characterSkinProduct = characterSkinProduct;

        EnvironmentSkinProduct environmentSkinProduct = StoreController.GetSelectedProduct(StoreProductType.EnvironmentSkin) as EnvironmentSkinProduct;
        GameControllerParkingJam.environmentSkinProduct = environmentSkinProduct;

        LevelController.Init();
        //Debug.LogError("ActualLevelId: " + ActualLevelId);
        StartLevel(ActualLevelId, true);

        StartStage = true;
        WinStage = false;

        IsMissileAttack = false;
        IsHammerAttack = false;

        GamePlayAdsHandler.adsHandler.CallingBannerInvoke();

        QualitySettings.vSyncCount = 1;
    }

    public static void StartGame() //start
    {
        StartStage = false;
        LevelController.LoadObstaclesAndCars();
        UIController.SetSkipButtonVisibility(true);
        UIController.PowerButtonsVisibility();
        CameraControllerParkingJam.ChangeAngleToGamePosition(LevelController.CurrentLevel);
    }


    public static void NextLevelBtnClick(int ads=0)
    {
        if (PlayerPrefs.GetInt("IsChallenges") == 1)
        {
            UIController.GameUI.Home();
        }
        else
        {
            if (IsBossLevel)
            {
                PlayerPrefs.SetInt("BossLevelTime" + CurrentLevelId, 1);
            }
            else
            {
                CurrentLevelId++;
            }
            //Debug.LogError(LevelDatabase.LevelsCount);
            // if (CurrentLevelId >= LevelDatabase.LevelsCount)
            //if (CurrentLevelId >= 1000)
            //{
            //    //int oldLevel = ActualLevelId;
            //    //do
            //    //{
            //    //    ActualLevelId = UnityEngine.Random.Range(0, LevelDatabase.LevelsCount);
            //    //} while (ActualLevelId == oldLevel);
            //}
            //else
            //{
            ActualLevelId = CurrentLevelId;
            //}

            UIController.GameUI.NextLevelLoad(ads);
        }
    }

    public static void NextLevel(bool withTransition = true)
    {
        //AdsManager.ShowInterstitial((isDisplayed) => //bilal.ads
        //{
        //    if (isDisplayed)
        //    {
        //        CalculateNextLevel(withTransition);
        //    }
        //    else
        //    {
        //        CalculateNextLevel(withTransition);
        //    }
        //});
    }

    private static void CalculateNextLevel(bool withTransition)
    {
        if (IsBossLevel)
        {
            PlayerPrefs.SetInt("BossLevelTime" + CurrentLevelId,1);
           
        }
        else
        {
            CurrentLevelId++;
        }
        if (CurrentLevelId >= LevelDatabase.LevelsCount)
        {
            int oldLevel = ActualLevelId;
            do
            {
                ActualLevelId = UnityEngine.Random.Range(0, LevelDatabase.LevelsCount);
            } while (ActualLevelId == oldLevel);
        }
        else
        {
            ActualLevelId = CurrentLevelId;
        }

        UITouchHandler.CanReplay = false;

        if (withTransition)
        {
            StartLevelWithTransition(ActualLevelId);
        }
        else
        {
            StartLevel(ActualLevelId, StartStage);
        }
    }

    private static void PreviousLevel()
    {
        if(ActualLevelId > 0)
        {
            ActualLevelId--;
            CurrentLevelId = ActualLevelId;
        }

        StartLevel(ActualLevelId, StartStage);
    }

    private static void FirstLevel()
    {
        ActualLevelId = 0;
        CurrentLevelId = 0;

        StartLevel(ActualLevelId, StartStage);
    }

    public static void PrevLevelDev()
    {
        LevelController.DestroyLevel();
        PreviousLevel();
    }

    public static void FirstLevelDev()
    {
        LevelController.DestroyLevel();
        FirstLevel();
    }

    public static void StartLevel(int levelId, bool withLogo = false)
    {
        Level level = LevelDatabase.GetLevel(levelId);

        if(withLogo) {
            LevelController.InitLevelWithLogo(level);
        } else
        {
            LevelController.InitLevel(level);
        }


        //environment locks set
        if (CurrentLevelId==39)
        {
            PlayerPrefs.SetInt("GameEnvironmentLock", 1);
        }
        else if (CurrentLevelId == 79)
        {
            PlayerPrefs.SetInt("GameEnvironmentLock", 2);
        }
        else if (CurrentLevelId == 119)
        {
            PlayerPrefs.SetInt("GameEnvironmentLock", 3);
        }
        else if (CurrentLevelId == 159)
        {
            PlayerPrefs.SetInt("GameEnvironmentLock", 4);
        }
        else if (CurrentLevelId == 199)
        {
            PlayerPrefs.SetInt("GameEnvironmentLock", 5);
        }
        else if (CurrentLevelId == 239)
        {
            PlayerPrefs.SetInt("GameEnvironmentLock", 6);
        }
        else if (CurrentLevelId == 279)
        {
            PlayerPrefs.SetInt("GameEnvironmentLock", 7);
        }
        else if (CurrentLevelId == 319)
        {
            PlayerPrefs.SetInt("GameEnvironmentLock", 8);
        }
        //

        CameraControllerParkingJam.Init(level);
        UIController.SetLevel(CurrentLevelId);
    }

    public static void StartLevelWithTransition(int levelId) 
    {
        Level level = LevelDatabase.GetLevel(levelId);
        LevelController.InitLevelWithTransition(level);

        UIController.SetLevel(CurrentLevelId);

        WinStage = false;
    }

    public static void FinishLevel()
    {
        WinStage = true;

        GameAudioController.VibrateLevelFinish();
    }

    public static void SkipLevel()
    {
        instance.StartCoroutine(LevelObjectsSpawner.HideBounceCars());
        LevelController.FinishLevel();
        FinishLevel();
    }

    public static void ReplayLevel()
    {
        if(!LevelController.isReplaying) LevelController.ReplayLevel();
    }

    public static void CollectCoins(int amount)
    {
        CoinsCount += amount;
    }

    public static void SpendCoins(int amount)
    {
        CoinsCount -= amount;
    }

    public static void SetCarSkin(CarSkinProduct characterSkinProduct)
    {
        GameControllerParkingJam.characterSkinProduct = characterSkinProduct;

        CarsSkinId = characterSkinProduct.ID;

        // Remove cars
        LevelPoolHandler.DeleteMovablesPools();
        LevelPoolHandler.InitMovablePools();

        if (!GameControllerParkingJam.StartStage && !LevelObjectsSpawner.IsMovablesEmpty)
        {
            LevelObjectsSpawner.SpawnCars();
        }
    }

    public static void SetEnvironmentSkin(EnvironmentSkinProduct environmentSkinProduct)
    {

        GameControllerParkingJam.environmentSkinProduct = environmentSkinProduct;

        EnvironmentSkinId = environmentSkinProduct.ID;

        LevelController.ResetEnvironment();

        // Remove cars
        LevelPoolHandler.DeleteObstaclePools();
        LevelPoolHandler.InitObstaclePools();

        if (!GameControllerParkingJam.StartStage && !LevelObjectsSpawner.IsMovablesEmpty)
        {
            LevelObjectsSpawner.SpawnObstacles();
        }
    }

    private void OnProductSelected(StoreProduct product)
    {
        if(product.Type == StoreProductType.CharacterSkin)
        {
            CarSkinProduct carSkinProduct = product as CarSkinProduct;
            if(carSkinProduct != null)
            {
                SetCarSkin(carSkinProduct);
            }
        }
        else if(product.Type == StoreProductType.EnvironmentSkin)
        {
            EnvironmentSkinProduct environmentSkinProduct = product as EnvironmentSkinProduct;
            if (environmentSkinProduct != null)
            {
                SetEnvironmentSkin(environmentSkinProduct);
            }
        }
    }
}
