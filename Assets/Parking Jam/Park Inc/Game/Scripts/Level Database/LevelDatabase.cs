#pragma warning disable 649

using UnityEngine;
namespace Watermelon
{
    [CreateAssetMenu(menuName = "Level Database/Level Database", fileName = "Level Database")]
    public class LevelDatabase : ScriptableObject
    {
        [SerializeField] Level[] levels;
        [SerializeField] Level[] levelsBoss;
        [SerializeField] Level[] ChallengeLevels;
        [SerializeField] Obstacle[] obstacles;
        [SerializeField] MovableObject[] movableObjects;
        public GameObject[] environmentPrefabs;
        //Editor stuff
        [SerializeField] Texture2D placeholderTexture;
        [SerializeField] Texture2D greenTexture;
        [SerializeField] Texture2D redTexture;
        [SerializeField] Texture2D itemBackgroundTexture;

        public int LevelsCount => levels.Length;
        public int ObstaclesCount => obstacles.Length;
        public int MovableObjectsCount => movableObjects.Length;
        public Level GetLevel(int index)
        {
            if (PlayerPrefs.GetInt("IsChallenges")==1)
            {
                   
            }


            if (PlayerPrefs.GetInt("IsChallenges")==1)
            {
                UIController.GameUI.challengeLevelEntry.SetActive(true);
                return ChallengeLevels[PlayerPrefs.GetInt("ChallengeNumber")];
            }

            if ((index%10==0 && PlayerPrefs.GetInt("BossLevelTime"+index)==0) && index!=0) //checking Boss Levels
            {
                GameControllerParkingJam.IsBossLevel = true;
                UIController.GameUI.bossLevelEnter.SetActive(true);

                if (index%200==0)
                {
                    PlayerPrefs.SetInt("BossLevelTurnLimit", 270);
                    UIController.GameUI.bossLevelMoveLimit.transform.parent.gameObject.SetActive(true);
                    UIController.GameUI.bossLevelMoveLimit.gameObject.SetActive(true);
                    UIController.GameUI.bossLevelMoveLimit.text = "MOVES LEFT: " + PlayerPrefs.GetInt("BossLevelTurnLimit");
                }
                else
                {
                    PlayerPrefs.SetInt("BossLevelTurnLimit", 70);
                    UIController.GameUI.bossLevelMoveLimit.transform.parent.gameObject.SetActive(true);
                    UIController.GameUI.bossLevelMoveLimit.gameObject.SetActive(true);
                    UIController.GameUI.bossLevelMoveLimit.text = "MOVES LEFT: " + PlayerPrefs.GetInt("BossLevelTurnLimit");
                }

                if (index/10 >=21 && index/10 <=40)
                {
                    index -= 21;
                    return levelsBoss[index];
                }
                else if (index / 10 >= 41 && index / 10 <= 60)
                {
                    index -= 41;
                    return levelsBoss[index];
                }
                else if (index / 10 >= 61 && index / 10 <= 80)
                {
                    index -= 61;
                    return levelsBoss[index];
                }
                else if (index / 10 >= 81 && index / 10 <= 100)
                {
                    index -= 81;
                    return levelsBoss[index];
                }
                else
                {
                    return levelsBoss[(index / 10) - 1];
                }
            }

            //if (index < 0 || index >= LevelsCount)
            if (index < 0)
            { 
                return null;
            }

            
            if (index >= LevelsCount)
            {
                GameControllerParkingJam.IsBossLevel = false;
                if (index>=300 && index<=399)
                {
                    index -= 100;
                }
                else if (index>=400 && index<=499)
                {
                    index -= 200;
                }
                else if (index>=500 && index<=599)
                {
                    index -= 300;
                }
                else if (index>=600 && index<=699)
                {
                    index -= 400;
                }
                else if (index>=700 && index<=799)
                {
                    index -= 500;
                }
                else if (index>=800 && index<=899)
                {
                    index -= 600;
                }
                else if (index>=900 && index<=999)
                {
                    index -= 700;
                }
                else
                {
                    return null;
                }
            }
            
            GameControllerParkingJam.IsBossLevel = false;
            

            return levels[index];
        }

        public Obstacle GetObstacle(int index)
        {
            if (index < 0 || index >= ObstaclesCount)
            {
                return null; 
            }

            return obstacles[index];
        }

        public MovableObject GetMovableObject(int index)
        {
            if (index < 0 || index >= MovableObjectsCount) return null;

            return movableObjects[index];
        }
    }
}