using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModeID : MonoBehaviour
{
    public GameObject gasBtn;
    public RCC_CarControllerV3 currentCar;
    public RCC_CarControllerV3 currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Modeis", 3);
    }
    public void OnPressNos()
    {
        //currentPlayer = RCC_SceneManager.Instance.activePlayerVehicle.GetComponent<RCC_CarControllerV3>();
        //if (!currentPlayer.RearLeftWheelCollider.wheelCollider.isGrounded
        //        && !currentPlayer.RearRightWheelCollider.wheelCollider.isGrounded
        //        && !currentPlayer.FrontLeftWheelCollider.wheelCollider.isGrounded
        //        && !currentPlayer.FrontRightWheelCollider.wheelCollider.isGrounded)
        //{
        //    currentPlayer.isNos = true;
        //    gasBtn.GetComponent<RCC_UIController>().pressing = true;
        //}

    }

    public void OnReleaseNos()
    {
        //currentPlayer = RCC_SceneManager.Instance.activePlayerVehicle.GetComponent<RCC_CarControllerV3>();
        //gasBtn.GetComponent<RCC_UIController>().pressing = false;
        //currentPlayer.isNos = false;
    }

}
