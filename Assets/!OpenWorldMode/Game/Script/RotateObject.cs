using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its own axis
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
