#pragma warning disable 649

using UnityEngine;
using UnityEngine.EventSystems;

// UI Module v0.9.0
public class UITouchHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isMouseDown = false;
    private DragData dragData;

    public static bool Enabled { get; set; }

    public static bool CanReplay { get; set; }

    void Awake()
    {
        Enabled = false;
        CanReplay = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isMouseDown) return;

        Vector2 delta = dragData.PressPosition - eventData.position;
        
        if (delta.magnitude > 50)//50
        {

            if (TutorialController.Active)
            {
                if(TutorialController.Approve(delta))
                {
                    dragData.Movable.TryMove(delta);

                    isMouseDown = false;

                    TutorialController.NextStep();
                }
            } else
            {
                
                dragData.Movable.TryMove(delta);
                //Debug.LogError("Move Car 1");
                if (GameControllerParkingJam.IsBossLevel)
                {
                    PlayerPrefs.SetInt("BossLevelTurnLimit", PlayerPrefs.GetInt("BossLevelTurnLimit")-1);
                    UIController.GameUI.bossLevelMoveLimit.text = "MOVES LEFT: " + PlayerPrefs.GetInt("BossLevelTurnLimit");
                    if (PlayerPrefs.GetInt("BossLevelTurnLimit")==0)
                    {
                        LevelController.LevelFail(1);
                    }
                }
                CanReplay = true;

                UIController.SetReplayButtonVisibility(true);

                isMouseDown = false;
            }
             
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (!Enabled) return;

        if (GameControllerParkingJam.WinStage)
        {

            if(Physics.Raycast(CameraControllerParkingJam.MainCamera.ScreenPointToRay(eventData.position), out RaycastHit buttonHit, float.PositiveInfinity, 1024))
            {
                if (buttonHit.collider.tag == "WorldSpaceButton")
                {
                    ExecuteEvents.Execute(buttonHit.collider.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
                    return;
                }
            }

            LevelController.Environment.CollectCoins();

            GameControllerParkingJam.TurnsAfterRewardVideo++;
            GameControllerParkingJam.NextLevel();
        }

        if (GameControllerParkingJam.StartStage)
        {
            GameControllerParkingJam.StartGame();
        }

        Ray ray = CameraControllerParkingJam.MainCamera.ScreenPointToRay(eventData.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2000000000, 256)) 
        //if (Physics.Raycast(ray, out hit, 200, 256))
        //if (Physics.Raycast(ray, out hit, float.PositiveInfinity)) 
        {
            MovableController movable = hit.transform.GetComponent<MovableController>();
            if(movable != null && movable.IsInteractable)
            {
                if (GameControllerParkingJam.IsHammerAttack)
                {
                    movable.HammerPowerActivate();
                }
                else if (GameControllerParkingJam.IsMissileAttack)
                {
                    movable.MissileAttackActivate();
                }
                else if (GameControllerParkingJam.IsRollerAttack)
                {
                    //do nothing
                }
                else
                {

                    isMouseDown = true;
                    dragData = new DragData { Movable = movable, PressPosition = eventData.position };
                }
            }
            else if (hit.transform.GetComponent<RollerAttack>() && GameControllerParkingJam.IsRollerAttack)
            {
                hit.transform.GetComponent<RollerAttack>().Rolling();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMouseDown = false;

        Ray ray = CameraControllerParkingJam.MainCamera.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out RaycastHit hit, 200, 8192))//200
        {
            EnvironmentProp prop = hit.collider.GetComponent<EnvironmentProp>();
            prop.Tap();

        }
    }

    private struct DragData
    {
        public MovableController Movable;
        public Vector2 PressPosition;
    }
}
