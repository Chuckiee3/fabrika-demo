using System;
using _Reusable.Actions;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LayerMask drawingCanvasLayer;
    public float distThreshold;


    private int pointCount;
    
    
    private bool isDrawing;
    
    private Camera cam;
    
    
    private Vector3 lastSavedPos;
    private Vector3 currentPos;
    private Vector3[] points;
    private void Awake()
    {
        cam = Camera.main;
        points = new Vector3[5000];
    }

    private void OnEnable()
    {
        TouchActions.touchDown += StartDrawingIfOnCanvas;
        TouchActions.touchUp += StopDrawing;
    }

    private void OnDisable()
    {
        TouchActions.touchDown -= StartDrawingIfOnCanvas;
        TouchActions.touchUp -= StopDrawing;
        
    }

    private void StartDrawingIfOnCanvas(Vector2 pos)
    {
        isDrawing = false;
        Vector3 pos2 = pos;
        Ray r = cam.ScreenPointToRay(pos);
        if (Physics.Raycast(r, 5, drawingCanvasLayer))
        {
            pointCount = 0;
            lastSavedPos = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward );
            AddPoint(lastSavedPos);
            isDrawing = true;   
        }
    }

    private void AddPoint(Vector2 point)
    {
        if (pointCount >= 5000) return;
        points[pointCount] = point;
        lastSavedPos = point;
        pointCount++;
        UpdateLineRenderer();

    }

    private void UpdateLineRenderer()
    {
        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPositions(points);
    }

    private void StopDrawing(Vector2 pos)
    {
        isDrawing = false;
        ResetLine();
        //Update Car
    }

    private void ResetLine()
    {
        
        lineRenderer.positionCount = 0;
    }


    void Update()
    {
        if(!isDrawing) return;
        currentPos = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward );
        if ((lastSavedPos - currentPos).sqrMagnitude >= distThreshold)
        {
            AddPoint(currentPos);
        }
    }
}
