using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceTagremove : MonoBehaviour
{
    private void OnEnable()
    {
        RGSK.MenuManager.instance.itemPrice.gameObject.SetActive(false);
        RGSK.MenuManager.instance.buyVehicleButton.gameObject.SetActive(false);
        RGSK.MenuManager.instance.locked.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        RGSK.MenuManager.instance.itemPrice.gameObject.SetActive(true);
        RGSK.MenuManager.instance.buyVehicleButton.gameObject.SetActive(true);
        RGSK.MenuManager.instance.locked.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
