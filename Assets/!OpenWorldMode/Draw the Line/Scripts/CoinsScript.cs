using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinsScript : MonoBehaviour
{
   
    public Text textComponent;
    public float lerpDuration = 2f;

    private float startValue = 1f;
    private float endValue = 100f;
    private float currentTime = 0f;
    private bool hasStarted = false;
    void Start()
    {
        if (textComponent == null)
        {
            Debug.LogError("Text component not assigned!");
            return;
        }

        // Initialize the text value to the start value
        textComponent.text = startValue.ToString();
    }

    void Update()
    {
        if (textComponent == null)
            return;

        // Check if the parent object is active and the lerping has not started
        if (transform.parent != null && transform.parent.gameObject.activeInHierarchy && !hasStarted)
        {
            // Update the current time based on the elapsed time
            currentTime += Time.deltaTime;

            // Lerp the text value between start and end values
            float lerpValue = Mathf.InverseLerp(0f, lerpDuration, currentTime);
            float lerpedValue = Mathf.Lerp(startValue, endValue, lerpValue);

            // Update the text component with the lerped value
            textComponent.text = Mathf.RoundToInt(lerpedValue).ToString();

            // Check if lerping has reached the end
            if (currentTime > lerpDuration)
            {
                currentTime = 0f;
                hasStarted = true; // Set to true to prevent further lerping
            }
        }
    }

}


