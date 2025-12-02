using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exploder.Utils;

public class ActivateObj : MonoBehaviour
{
    public GameObject obj;
    public bool isCone = false;
    public bool isGlass;
    public GameObject glassPrefab, conePrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (isCone)
            {
                Debug.Log("Exploded");
                ExploderSingleton.Instance.ExplodeObject(gameObject);
                Handheld.Vibrate();
                Instantiate(conePrefab, other.transform.position, other.transform.rotation);
            }
            else if (isGlass)
            {
                Debug.Log("Glass Exploded");
                ExploderSingleton.Instance.ExplodeObject(gameObject);
                Handheld.Vibrate();
                Instantiate(glassPrefab, other.transform.position, other.transform.rotation);
            }
            else
            {
                obj.SetActive(true);
            }
        }
    }
}
