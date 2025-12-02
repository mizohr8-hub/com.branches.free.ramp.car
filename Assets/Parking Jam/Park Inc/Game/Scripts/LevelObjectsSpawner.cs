using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Watermelon;

public class LevelObjectsSpawner : MonoBehaviour
{
    private static List<MovableController> movables;
    private static List<ObstacleController> obstacles;

    private static Dictionary<MovableObjectData, MovableController.State> savedMovables;

    public static int MovablesCount => movables.Count;
    public static bool IsMovablesEmpty => movables.Count == 0;

    public static MovableController GetCar(int index)
    {
        return movables[index];
    }

    void Awake()
    {
        movables = new List<MovableController>();
        obstacles = new List<ObstacleController>();

        savedMovables = new Dictionary<MovableObjectData, MovableController.State>();
    }

    public static void SetInteractables(MovableController interactableCar)
    {
        for(int i = 0; i < movables.Count; i++)
        {
            MovableController car = movables[i];

            car.SetInteractable(car == interactableCar);
        }
    }

    public static void ResetInteractables()
    {
        for (int i = 0; i < movables.Count; i++)
        {
            movables[i].SetInteractable(true);
        }
    }


    public static IEnumerator SpawnBounceCars()
    {

        SpawnCars(false);

        float waitDuration = 0.016f;

        if (movables.Count < 5)
        {
            waitDuration = 0.05f;
        }
        else if (movables.Count < 10)
        {
            waitDuration = 0.03f;
        }

        int i = 0;
        float count = movables.Count;

        foreach (MovableController movable in movables)
        {
            movable.transform.DOScale(Vector3.one * 0.5f, 0.15f).OnComplete(() => {
                movable.transform.DOScale(Vector3.one, 0.35f).SetEasing(Ease.Type.BackOut);
            });

            GameAudioController.PlayBounceSpawn(0.75f + (float)i / LevelController.CurrentLevel.ObstaclesCount * 0.5f);

            i++;

            yield return new WaitForSeconds(waitDuration);
        }

        yield return new WaitForSeconds(0.5f + waitDuration * movables.Count);
    }

    public static IEnumerator HideBounceCars()
    {

        float waitDuration = 0.016f;

        if (movables.Count < 5)
        {
            waitDuration = 0.05f;
        }
        else if (movables.Count < 10)
        {
            waitDuration = 0.03f;
        }

        int i = 0;
        float count = movables.Count;

        foreach (MovableController movable in movables.ToList())
        {
            movable.transform.DOScale(Vector3.one * 0.5f, 0.35f).SetEasing(Ease.Type.BackIn).OnComplete(() =>
            {
                movable.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() =>
                {
                    movable.gameObject.SetActive(false);
                    movable.transform.localScale = Vector3.one;
                    movable.Disable();
                });
            });

            GameAudioController.PlayBounceHide(0.75f + i / count * 0.5f);

            i++;

            yield return new WaitForSeconds(waitDuration);
        }

        yield return new WaitForSeconds(0.5f + waitDuration * movables.Count);

        movables.Clear();
    }

    public static IEnumerator SpawnBounceObstacles()
    {

        SpawnObstacles(false);

        float waitDuration = 0.016f;

        if (obstacles.Count < 5)
        {
            waitDuration = 0.05f;
        }
        else if (obstacles.Count < 10)
        {
            waitDuration = 0.03f;
        }

        for (int i = 0; i < LevelController.CurrentLevel.ObstaclesCount; i++)
        {
            ObstacleController obstacle = obstacles[i];

            obstacle.transform.DOScale(Vector3.one * 0.5f, 0.15f).OnComplete(() =>
            {
                obstacle.transform.DOScale(Vector3.one, 0.35f).SetEasing(Ease.Type.BackOut);
            });

            GameAudioController.PlayBounceSpawn(0.75f + (float) i / LevelController.CurrentLevel.ObstaclesCount * 0.5f);

            yield return new WaitForSeconds(waitDuration);
        }
    }

    public static IEnumerator HideBounceObstacles()
    {

        float waitDuration = 0.016f;

        if (obstacles.Count < 5)
        {
            waitDuration = 0.05f;
        }
        else if (obstacles.Count < 10)
        {
            waitDuration = 0.03f;
        }

        int i = 0;
        float count = obstacles.Count;

        foreach (ObstacleController obstacle in obstacles)
        {
            obstacle.transform.DOScale(Vector3.one * 0.5f, 0.35f).SetEasing(Ease.Type.BackIn).OnComplete(() =>
            {
                obstacle.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() =>
                {
                    obstacle.gameObject.SetActive(false);
                    obstacle.transform.localScale = Vector3.one;
                    obstacle.Disable();
                });
            });

            //GameAudioController.PlayBounceHide(0.75f + i / count * 0.5f);

            i++;

            yield return new WaitForSeconds(waitDuration);
        }

        yield return new WaitForSeconds(0.5f + waitDuration * obstacles.Count);

        obstacles.Clear();

        LevelPoolHandler.ReturnObstaclesToPool();
    }

    public static void SpawnObstacles(bool scaled = true)
    {
        obstacles.Clear();
        int theNPCwalkingIndex=0;

        for (int i = 0; i < LevelController.CurrentLevel.ObstaclesCount; i++)
        {
            ObstacleData obstacleData = LevelController.CurrentLevel.GetObstacle(i);

            ObstacleController obstacle = LevelPoolHandler.GetObstaclePool(obstacleData.Obstacle).GetPooledObject().GetComponent<ObstacleController>();

            if (obstacle.IsMovingNPC && LevelController.CurrentLevel.hasNpcs) //npc  (when loop finds an npc then the index with corresponding move data is checked npcs are dealt with as if they are in a different array hence we start with 0 and then get corresponding values
            {
                int moveDataLength = LevelController.CurrentLevel.nPCWalkPoints[theNPCwalkingIndex].npcWalkingPoints.Length;
                obstacle.movementData = new ObstacleController.MovementData[moveDataLength];
                for (int j = 0; j < moveDataLength; j++)
                {//bilal.createNPC
                    Vector3 thePos = LevelController.CurrentLevel.nPCWalkPoints[theNPCwalkingIndex].npcWalkingPoints[j];
                    Quaternion theQuat = LevelController.CurrentLevel.nPCWalkPoints[theNPCwalkingIndex].npcWalkRotation[j];
                    obstacle.movementData[j].EaseFunction = Ease.Type.Linear;
                    obstacle.movementData[j].Duration = LevelController.CurrentLevel.nPCWalkPoints[theNPCwalkingIndex].moveSpeed;
                    obstacle.movementData[j].IsSnteractable = true;
                    obstacle.movementData[j].Position = thePos;
                    obstacle.movementData[j].Rotation = theQuat;
                }
                theNPCwalkingIndex++;
            }
            obstacle.transform.localScale = Vector3.one;

            obstacle.Init(obstacleData);

            obstacle.transform.localScale = scaled ? Vector3.one : Vector3.zero;
            obstacle.name = "obstacleIndex" + i;
            obstacles.Add(obstacle);
        }
    }

    public static void SpawnCars(bool scaled = true)
    {
        movables.Clear();

        for(int i = 0; i < LevelController.CurrentLevel.MovableObjects.Length; i++)
        {
            MovableObjectData movableData = LevelController.CurrentLevel.MovableObjects[i];
            MovableController movableObject = LevelPoolHandler.GetMovablePool(movableData.MovableObject).GetPooledObject().GetComponent<MovableController>();

            movableObject.Init(movableData);

            movableObject.transform.localScale = scaled ? Vector3.one : Vector3.zero;
            movableObject.name = "CarIndex" + i;
            movables.Add(movableObject);
        }
    }

    public static void RemoveCar(MovableController movable)
    {
        movables.Remove(movable);
        movable.Disable();
    }

    public static void DisableNPCs()
    {
        foreach (ObstacleController obstacle in obstacles)
        {
            if (obstacle.IsMovingNPC)
            {
                obstacle.gameObject.SetActive(false);
            }
        }
    }
    
    public static void DisableObstacles()
    {
        foreach (ObstacleController obstacle in obstacles)
        {
            if (!obstacle.IsMovingNPC)
            {
                obstacle.gameObject.SetActive(false);
            }
        }
       
    }

    public static void TurnOnTargetSelectOfCars()
    {
        foreach (MovableController movable in movables)
        {
            movable.targetSelection.SetActive(true);
        }
    }
    
    public static void TurnOffTargetSelectOfCars()
    {
        foreach (MovableController movable in movables)
        {
            movable.targetSelection.SetActive(false);
        }
    }
    public static void TurnOnObstacles()
    {
        foreach (ObstacleController obstacle in obstacles)
        {
            if (!obstacle.IsMovingNPC && !obstacle.ObstacleDestroyed)
            {
                obstacle.gameObject.SetActive(true);

                obstacle.InitNew();
            }
        }
    }
    
    public static void TurnOnNPCs()
    {
        foreach (ObstacleController obstacle in obstacles)
        {
            if (obstacle.IsMovingNPC && !obstacle.ObstacleDestroyed)
            {
                obstacle.gameObject.SetActive(true);

                obstacle.InitNew();
            }
        }
    }
    public static void DisableMovables()
    {
        foreach (MovableController movable in movables)
        {
            movable.Disable();
        }

    }

    public static void SaveCars()
    {
        savedMovables.Clear();
        foreach (MovableController movable in movables)
        {
            savedMovables.Add(movable.Data, movable.CreateState());
            movable.Disable();
        }
        movables.Clear();
    }

    public static void RestoreCarsFromSave()
    {
        foreach (MovableObjectData movableData in savedMovables.Keys)
        {
            MovableController movableObject = LevelPoolHandler.GetMovablePool(movableData.MovableObject).GetPooledObject().GetComponent<MovableController>();

            movableObject.Init(savedMovables[movableData], movableData);

            movables.Add(movableObject);
        }
    }

    public static void RemoveNPCHitCar()
    {
        foreach (MovableController movable in movables)
        {
            if (movable.hitNPC)
            {
                movable.hitNPC = false;
                //LevelController.MovableFinished(movable);
            }
        }
    }
}
