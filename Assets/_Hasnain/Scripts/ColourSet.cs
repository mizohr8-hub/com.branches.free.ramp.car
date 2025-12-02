using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSet : MonoBehaviour
{
    public ColorPicker colorPicker;
    public Material[] Mat;
    Color col;
    public bool setColor = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Set()
    {
        Mat[RGSK.MenuManager.instance.vehicleIndex].SetColor("_Color", colorPicker.setColor);
        col = Mat[RGSK.MenuManager.instance.vehicleIndex].color;
    }



    // Update is called once per frame
    void Update()
    {
        Mat[RGSK.MenuManager.instance.vehicleIndex].SetColor("_Color", colorPicker.setColor);
    }
}
