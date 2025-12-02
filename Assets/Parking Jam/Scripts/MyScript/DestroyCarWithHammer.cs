using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCarWithHammer : MonoBehaviour
{
    public MovableController car;
    // Start is called before the first frame update
    
    public void DestroyCar()
    {
        Handheld.Vibrate();
        car.HammerPowerEffect();
    }

    public void MissileBoom()
    {
        Handheld.Vibrate();
        car.MissileDestroy();
    }
    public void DelayOff()
    {
        Invoke(nameof(DelayOffCo), 2f);
    }
    void DelayOffCo()
    {
        UITouchHandler.Enabled = true;
        UIController.GameUI.PowersEnabled();
        LevelController.MovableFinished(car);
    }
}
