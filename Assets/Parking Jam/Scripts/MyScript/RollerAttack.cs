using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator rollAnim;
    public BoxCollider parentCollider;
    public BoxCollider rollerCollider;
    public GameObject originalPos;
    Vector3 startPoint;
    void Start()
    {
        startPoint = originalPos.transform.position;
    }

    public void Rolling()
    {
        LevelController.Environment.RollerArray.ForEach(delegate(GameObject go)
            {
                if (go.name!=this.name)
                {
                    go.SetActive(false);
                }                       
            }
        );
        UITouchHandler.Enabled = false;
        parentCollider.enabled = false;
        rollerCollider.enabled = true;
        rollAnim.enabled = true;
        StartCoroutine(CameraControllerParkingJam.ReturnToOriginalFOV());
        Invoke(nameof(RollerOff), 5f);
    }
    public void RollerOff()
    {
        UITouchHandler.Enabled = true;
        GameControllerParkingJam.IsRollerAttack = false;
        parentCollider.enabled = true;
        rollerCollider.enabled = false;
        rollAnim.enabled = false;
        originalPos.transform.position = startPoint;
        UIController.GameUI.PowersEnabled();
        gameObject.SetActive(false);
    }
}
