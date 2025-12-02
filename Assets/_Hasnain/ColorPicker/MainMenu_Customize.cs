using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu_Customize : MonoBehaviour
{

   
    public ColorPickerr colorPicker;
  

    public VehicleSetting[] vehicleSetting;

    private VehicleSetting currentVehicle;

    private int currentVehicleNumber = 0;

    bool carbody=false;
    public int carparts;
    [System.Serializable]
    public class VehicleSetting
    {
        public Material bodyMat;
    }

    //Wheel Color

    public void ActiveWheelColor(Image activeImage)
    {
        carparts = 1;
        carbody = !carbody;
        colorPicker.showPicker = carbody;
        activeImage.gameObject.SetActive(carbody);
        
    }

    //Body Color

    public void ActiveBodyColor(Image activeImage)
    {
        carparts = 0;
        carbody = !carbody;
        activeImage.gameObject.SetActive(carbody);
        colorPicker.showPicker = carbody;
       
    }



    public Color col;
    public void Set()
    {
        vehicleSetting[currentVehicleNumber].bodyMat.SetColor("_Color", colorPicker.setColorBody);
        //Mat.SetColor("_Color", colorPicker.setColor);
        col = vehicleSetting[currentVehicleNumber].bodyMat.color;
    }

    //void Update()
    //{
    //   vehicleSetting[currentVehicleNumber].bodyMat.SetColor("_Color", colorPicker.setColorBody);
    //}

}
