using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour {
    [System.Serializable]
    public class MenuVehicle
    {
        [Header("Details")]
        public string name;
        public GameObject vehicle;
        public int price;


        [Header("Specs")]
        [Range(0, 1)]
        public float speed;
        [Range(0, 1)]
        public float acceleration;
        [Range(0, 1)]
        public float handling;
        [Range(0, 1)]
        public float braking;
    }
    [Header("Vehicle settings")]
    public MenuVehicle[] menuVehicles;


    // MenuVehicle car = new MenuVehicle();


    int vehicleId = 0;
    private void OnEnable()
    {
        vehicleId = PlayerPrefs.GetInt("VehicleId");
    }
    // Use this for initialization
    void Start() {
        //print(menuVehicles[b].name + "  " + menuVehicles[b].price + "  " + menuVehicles[b].speed + "  " + menuVehicles[b].acceleration);
        foreach (var item in menuVehicles)
        {
            item.vehicle.SetActive(false);
        }
        menuVehicles[vehicleId].vehicle.SetActive(true);
    }

    void Update() {

    }

    public void Next()
    {
        if (vehicleId < menuVehicles.Length - 1)
        {
            vehicleId++;
            // print(menuVehicles[b].name + "  " + menuVehicles[b].price + "  " + menuVehicles[b].speed + "  " + menuVehicles[b].acceleration);
            foreach (var item in menuVehicles)
            {
                item.vehicle.SetActive(false);
            }
            menuVehicles[vehicleId].vehicle.SetActive(true);
        }
    }
    public void Reverse()
    {
        if (vehicleId > 0)
        {
            vehicleId--;
            //print(menuVehicles[b].name + "  " + menuVehicles[b].price + "  " + menuVehicles[b].speed + "  " + menuVehicles[b].acceleration);
            foreach (var item in menuVehicles)
            {
                item.vehicle.SetActive(false);
            }
            menuVehicles[vehicleId].vehicle.SetActive(true);
        }
    }
    public void Play()
    {
        PlayerPrefs.SetInt("VehicleId", vehicleId);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
