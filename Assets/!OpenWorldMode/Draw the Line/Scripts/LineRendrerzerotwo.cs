using System.Collections.Generic;
using UnityEngine;

public class LineRendrerzerotwo : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int positionsCount;
    private Vector3[] linePositions;

    void Start()
    {
        // Create a LineRenderer component
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Set up the LineRenderer properties
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 0; // Initially, no points in the line
        linePositions = new Vector3[100]; // Adjust the array size based on your needs
    }

    void Update()
    {
        // Check for touch/mouse input
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10; // Set a distance from the camera

            // Convert mouse position to world space
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // Draw the line by adding points to the LineRenderer
            if (positionsCount < linePositions.Length)
            {
                linePositions[positionsCount] = worldMousePos;
                positionsCount++;
                lineRenderer.positionCount = positionsCount;
                lineRenderer.SetPositions(linePositions);
            }
            else
            {
                // Reset the count to start a new line
                positionsCount = 0;
                lineRenderer.positionCount = 0;
            }
        }
    }
}
