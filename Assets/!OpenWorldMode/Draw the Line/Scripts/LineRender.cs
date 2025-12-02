using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{

    public GameObject car;
    public float carSpeed;
    public Canvas canvas;  // Drag and drop the Canvas component here in the Inspector

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    private List<Vector3> linePositions = new List<Vector3>();
    private bool isDrawing = false;
    private int currentLineIndex = 0;
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        edgeCollider = gameObject.AddComponent<EdgeCollider2D>();

        // Set up the LineRenderer properties
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.sortingLayerName = "LineLayer"; // Set your sorting layer name
        lineRenderer.sortingOrder = 0; // Set your sorting order

        // Disable the LineRenderer initially
        lineRenderer.enabled = false;

        // Set up the EdgeCollider
        edgeCollider.isTrigger = false; // Set to true if you want to use it as a trigger
        edgeCollider.enabled = false;
    }

    void Update()
    {
        // Check for touch/mouse input
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }

        if (isDrawing)
        {
            DrawLine();
        }

        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            StopDrawing();
        }
        if (!isDrawing && linePositions.Count > 1)
        {
            MoveCar();
        }
    }

    void StartDrawing()
    {
        isDrawing = true;
        linePositions.Clear();
        lineRenderer.positionCount = 0;
        edgeCollider.points = new Vector2[0];
    }

    void DrawLine()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10; // Set a distance from the camera

        // Convert mouse position to world space
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Draw the line by adding points to the LineRenderer
        linePositions.Add(worldMousePos);
        lineRenderer.positionCount = linePositions.Count;
        lineRenderer.SetPositions(linePositions.ToArray());
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //// Draw the line by adding points to the LineRenderer
        //linePositions.Add(mousePos);
        //lineRenderer.positionCount = linePositions.Count;

        //// Convert Vector2 to Vector3
        //List<Vector3> linePositions3D = new List<Vector3>();
        //foreach (Vector2 pos in linePositions)
        //{
        //    linePositions3D.Add(new Vector3(pos.x, pos.y, 0f));
        //}

        //// Set positions in the LineRenderer
        //lineRenderer.SetPositions(linePositions3D.ToArray());
    }

    void StopDrawing()
    {
        isDrawing = false;

        // Create a new GameObject for the ground surface
        GameObject groundSurface = new GameObject("GroundSurface");
        groundSurface.transform.position = Vector3.zero; // Adjust the position as needed

        // Add a Rigidbody2D and set it to Static
        Rigidbody2D rb = groundSurface.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

        // Add a LineRenderer to the new GameObject
        LineRenderer groundLineRenderer = groundSurface.AddComponent<LineRenderer>();
        groundLineRenderer.startWidth = 0.1f;
        groundLineRenderer.endWidth = 0.1f;
        groundLineRenderer.sortingLayerName = "LineLayer"; // Set your sorting layer name
        groundLineRenderer.sortingOrder = 10; // Set your sorting order

        // Set positions in the LineRenderer
        groundLineRenderer.positionCount = linePositions.Count;

        // Convert Vector2 to Vector3 for LineRenderer positions
        Vector3[] linePositions3D = new Vector3[linePositions.Count];
        for (int i = 0; i < linePositions.Count; i++)
        {
            linePositions3D[i] = new Vector3(linePositions[i].x, linePositions[i].y, linePositions[i].z);
        }

        groundLineRenderer.SetPositions(linePositions3D);

        // Add an EdgeCollider2D to the new GameObject
        EdgeCollider2D groundEdgeCollider = groundSurface.AddComponent<EdgeCollider2D>();
        groundEdgeCollider.isTrigger = false; // Set to true if you want to use it as a trigger

        // Convert Vector2 to local space for EdgeCollider2D points
        Vector2[] edgeColliderPoints = new Vector2[linePositions.Count];
        for (int i = 0; i < linePositions.Count; i++)
        {
            edgeColliderPoints[i] = groundSurface.transform.InverseTransformPoint(linePositions[i]);
        }

        groundEdgeCollider.points = edgeColliderPoints;

        // Disable the LineRenderer and EdgeCollider on the original GameObject
        lineRenderer.enabled = false;
        edgeCollider.enabled = false;
    }

    void MoveCar()
    {
        if (linePositions.Count > 1)
        {
            Vector2 targetPosition = linePositions[currentLineIndex];
            float step = carSpeed * Time.deltaTime;

            car.transform.position = Vector2.MoveTowards(car.transform.position, targetPosition, step);

            // Check if the car has reached the current target position
            if (Vector2.Distance(car.transform.position, targetPosition) < 0.01f)
            {
                currentLineIndex++;

                // Check if the car has reached the end of the line
                if (currentLineIndex >= linePositions.Count)
                {
                    StopDrawing();
                }
            }
        }
    }

}
