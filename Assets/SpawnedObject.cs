using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    //public SpawnOnRoad spawner; // Reference to the spawner object

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            Destroy(gameObject); // Destroy the current object
            //spawner.SpawnObject(); // Spawn a new object using the spawner
        }
    }
}