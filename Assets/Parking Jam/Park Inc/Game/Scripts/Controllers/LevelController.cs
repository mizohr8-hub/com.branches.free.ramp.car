#pragma warning disable 649

using System;
using System.Collections;
using UnityEngine;
using Watermelon;

public class LevelController : MonoBehaviour
{
    private static LevelController instance;

    public static EnvironmentController Environment { get; private set; }

    public static Level CurrentLevel { get; private set; }

    public static bool isReplaying = false;

    private static Coroutine replayCoroutine;

    private void Start()
    {
        instance = this;
    }

    public static void Init()
    {
        //GameObject environmentObject = Instantiate(GameControllerParkingJam.EnvironmentSkinProduct.SkinPrefab);
        GameObject environmentObject = Instantiate(GameControllerParkingJam.LevelDatabase.environmentPrefabs[PlayerPrefs.GetInt("TucTucSelectedEnv")]);

        Environment = environmentObject.GetComponent<EnvironmentController>();
        LevelPoolHandler.InitPools();
    }

    public static void ReplayLevel()
    {
        if (replayCoroutine != null && isReplaying)
        {
            instance.StopCoroutine(replayCoroutine);

            isReplaying = false;
        }

        replayCoroutine = instance.StartCoroutine(ReplayLevelCoroutine());
    }

    private static IEnumerator ReplayLevelCoroutine()
    {
        isReplaying = true;

        yield return LevelObjectsSpawner.HideBounceCars();

        yield return LevelObjectsSpawner.SpawnBounceCars();

        isReplaying = false;
    }

    public static void DestroyLevel()
    {
        LevelPoolHandler.ReturnEverythingToPool();

        LevelObjectsSpawner.DisableMovables();
    }

    public static void InitLevel(Level level)
    {
        CurrentLevel = level;

        Environment.Init(level);

        LevelPoolHandler.ReturnEverythingToPool();

        LevelObjectsSpawner.SpawnObstacles();

        LevelObjectsSpawner.SpawnCars();

        TutorialController.Init(CurrentLevel);
    }

    public static void InitLevelWithLogo(Level level)
    {
        CurrentLevel = level;

        Environment.Init(level, true);

        LevelPoolHandler.ReturnEverythingToPool();
    }

    public static void InitLevelWithTransition(Level level)
    {
        UITouchHandler.Enabled = false;
        CurrentLevel = level;

        Environment.EnvironmentWinPanel.HideWinCanvas();
        Environment.NewLevelTransition(level);

        instance.StartCoroutine(LevelObjectsSpawner.SpawnBounceCars());
        instance.StartCoroutine(LevelObjectsSpawner.SpawnBounceObstacles());


        UIController.SetSkipButtonVisibility(true);

        CameraControllerParkingJam.ChangeAngleToGamePosition(CurrentLevel);

        Tween.DelayedCall(1f, () =>
        {
            UITouchHandler.Enabled = true;

            TutorialController.Init(CurrentLevel);
        });
    }


    public static void LoadObstaclesAndCars()
    {
        UITouchHandler.Enabled = false;
        Environment.FirstTap();

        instance.StartCoroutine(LevelObjectsSpawner.SpawnBounceCars());
        instance.StartCoroutine(LevelObjectsSpawner.SpawnBounceObstacles());

        Tween.DelayedCall(1f, () =>
        {
            UITouchHandler.Enabled = true;

            TutorialController.Init(CurrentLevel);
        });
        
    }

    

    public static void MovableFinished(MovableController movable)
    {
        LevelObjectsSpawner.RemoveCar(movable);

        if (PlayerPrefs.GetInt("IsChallenges")==1)
        {
            PlayerPrefs.SetInt("Challenge" + PlayerPrefs.GetInt("ChallengeNumber") + "Completed",1);
            if (movable.isChallengeCar)
            {
                LevelObjectsSpawner.DisableMovables();
                GameControllerParkingJam.FinishLevel();
                FinishLevel();
            }
        }
        else
        {
            if (LevelObjectsSpawner.IsMovablesEmpty)
            {
                GameControllerParkingJam.FinishLevel();
                FinishLevel();
            }
        }
    }

    public static void LevelFail(int l)
    {
        GamePlayAdsHandler.adsHandler.CancelBanner();
        if (l == 0) //npc 
        {
            
            UIController.GameUI.RevivalPanel.SetActive(true);
        }
        else if (l==1)//all turns gone
        {
            UIController.GameUI.MoreTurnsPanel.SetActive(true);
        }
        //UITouchHandler.Enabled = false;
        //UIController.SetReplayButtonVisibility(false);
        //UIController.SetSkipButtonVisibility(false);
        //Environment.Invoke("ShowLosePanel",2f);
    }

    public static void TrueLevelFail()
    {
        UIController.GameUI.RevivalPanel.SetActive(false);
        UIController.GameUI.MoreTurnsPanel.SetActive(false);
        UITouchHandler.Enabled = false;
        UIController.SetReplayButtonVisibility(false);
        UIController.SetSkipButtonVisibility(false);
        Environment.ShowLosePanel();
    }
    public static void FinishLevel()
    {
        UITouchHandler.Enabled = false;
        instance.StartCoroutine(LevelObjectsSpawner.HideBounceObstacles());
        UIController.GameUI.confetti.SetActive(true);
        /*
        Environment.BlendToClear();

        int coinsAmount = CurrentLevel.MovableObjects.Length * 2;
        if (coinsAmount < 15)
        {
            coinsAmount = 14 + GameControllerParkingJam.CurrentLevelId % 2;
        }
        if (coinsAmount > 25)
        {
            coinsAmount = 24 + GameControllerParkingJam.CurrentLevelId % 2;
        }

        Environment.ShowWinPanel(CurrentLevel, coinsAmount);
        */
        instance.Invoke(nameof(ContinueGameWin),2.5f);
    }

    void ContinueGameWin()
    {
        if (PlayerPrefs.GetInt("IsChallenges")==0)
        {
            int coinsAmount = CurrentLevel.MovableObjects.Length * 50;

            UIController.GameUI.LevelComplete(coinsAmount);
        }
        else
        {
            int []challengeReward = {500,600,700,800,900,1000,1100,1200,1300,1400,1500,1600,1700,1800,2000 };
            int coinsAmount = challengeReward[PlayerPrefs.GetInt("ChallengeNumber")] * 2;
            UIController.GameUI.LevelComplete(coinsAmount);
        }
        //Environment.ShowWinPanel(CurrentLevel, coinsAmount);
        UIController.SetReplayButtonVisibility(false);
        UIController.SetSkipButtonVisibility(false);

        CameraControllerParkingJam.ChangeAngleToMenuPosition(CurrentLevel);

    }

    public static Vector3 GetFinishPosition(Transform roadTransform)
    {
        return Environment.GetFinishPosition(roadTransform);
    }

    public static bool IsOtherFinishingMovableClose(MovableController current)
    {
        for(int i = 0; i < LevelObjectsSpawner.MovablesCount; i++)
        {
            MovableController movable = LevelObjectsSpawner.GetCar(i);

            if (movable == current) continue;
            if (!movable.IsFinishing) continue;
            if (movable.IsWaitingForOtherCarToPass) continue;
            if (current.Road != movable.Road) continue;

            Vector3 finishPosition = Environment.GetFinishPosition(current.Road); //Roads.GetFinishPosition(current.Road);

            if (Vector3.Distance(current.Position, finishPosition) > Vector3.Distance(movable.Position, finishPosition) + movable.Data.MovableObject.Size.y) continue;

            if (Vector3.Distance(movable.Position, current.Position) < (movable.Data.MovableObject.Size.y + current.Data.MovableObject.Size.y) / 2 + 10) return true;
        }

        return false;
    }

    public static void ResetEnvironment()
    {
        // GameObject environmentObject = Instantiate(GameControllerParkingJam.EnvironmentSkinProduct.SkinPrefab);
        GameObject environmentObject = Instantiate(GameControllerParkingJam.LevelDatabase.environmentPrefabs[PlayerPrefs.GetInt("TucTucSelectedEnv")]);

        EnvironmentController newEnvironment = environmentObject.GetComponent<EnvironmentController>();

        if (GameControllerParkingJam.WinStage || GameControllerParkingJam.StartStage)
        {
            newEnvironment.Init(Environment);
        }
        else
        {
            newEnvironment.Init(CurrentLevel);
        }

        Destroy(Environment.gameObject);
        Environment = newEnvironment;
    }

}

