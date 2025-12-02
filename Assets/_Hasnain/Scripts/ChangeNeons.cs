using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeNeons : MonoBehaviour
{
    public Material rimNeonMaterial;
    int current_material;
    public GameObject[] spoilders;
    

   
    public void SetMaterial(int index) 
    {
        current_material = index;

        if (current_material==0)
        {
            rimNeonMaterial.color = Color.cyan;
        }
        if (current_material == 1)
        {
            rimNeonMaterial.color = Color.red;
        }
        if (current_material == 2)
        {
            rimNeonMaterial.color = Color.yellow;
        }
        if (current_material == 3)
        {
            rimNeonMaterial.color = Color.green;
        }
        if (current_material == 4)
        {
            rimNeonMaterial.color = Color.magenta;
        }
        if (current_material == 5)
        {
            rimNeonMaterial.color = Color.blue;
        }
    }


    public void ChangeSpoiler(int id)
    {

    }


   //[System.Serializable]
   //public class Rims
   // {
   //     public GameObject[] rims;
   // }



}
