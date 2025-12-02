using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GarageManager: MonoBehaviour
{

    public GameObject[] vehicles;
     int vehicleId = 0;
    public GameObject nextVehicleBtn, preiousVehicleBtn;
    
    private void OnEnable()
    {
        foreach (var item in vehicles)
        {
            item.SetActive(false);
        }
        vehicles[vehicleId].SetActive(true);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (vehicleId == vehicles.Length - 1)
            nextVehicleBtn.SetActive(false);
        else if (vehicleId == 0)
            preiousVehicleBtn.SetActive(false);

        else
        {
            preiousVehicleBtn.SetActive(true);
            preiousVehicleBtn.SetActive(true);
        }
    }
    public void NextVehicle()
    {
        if (vehicleId < vehicles.Length-1)
        {
            vehicleId++;
            
            foreach (var item in vehicles)
            {
                item.SetActive(false);
            }
            vehicles[vehicleId].SetActive(true);
        }
        
    }

    public void PreviousVehicle()
    {
        if (vehicleId>0)
        {
            vehicleId--;
            
            foreach (var item in vehicles)
            {
                item.SetActive(false);
            }
            vehicles[vehicleId].SetActive(true);
        }
        
    }

    public void Play()
    {
        PlayerPrefs.SetInt("VehicleId", vehicleId);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        print(PlayerPrefs.GetInt("VehicleId"));
    }
}
