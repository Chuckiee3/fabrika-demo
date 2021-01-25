using System;
using System.Collections.Generic;
using _Reusable.Actions;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public CarBehaviour playerCarBehaviour;
    public LineRenderer lineRenderer;
    public LayerMask drawingCanvasLayer;
    public float distThreshold;
    public float thickness = .15f;
    public Vector3[] meshPoints;
    public int n = 4;
    private int pointCount;
    public float scaleMultiplier= 5;
    
    private bool isDrawing;
    
    private Camera cam;
    
    
    private Vector3 lastSavedPos;
    private Vector3 currentPos;
    private Vector3[] points;
    private Vector2[] points2D;
    private int k;
    private GameObject polyExtruderGO;
    public PipeMeshGenerator pipeMeshGenerator;
    private void Awake()
    {
        polyExtruderGO = new GameObject();
        polyExtruderGO.transform.SetParent(this.transform);

        polyExtruderGO.name = "CarMesh";
        cam = Camera.main;
        meshPoints = new Vector3[2000];
        points2D = new Vector2[5000];
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
        points2D[pointCount] = point;
        if (pointCount % n == 0)
        {
            meshPoints[k] = point * scaleMultiplier;
            k++;
        }
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
        if (lineRenderer.positionCount > 0)
        {
            //playerCarBehaviour.UpdateMesh(MeshUtils.CreateMeshFromPoints(points,  0.5f,2));
            //polyExtruder.ResetMesh();
           // polyExtruder.createPrism(polyExtruderGO.name, .5f, GetMeshPoints(lineRenderer), Color.grey, true);
          /* var pts = GetMeshPoints(lineRenderer);
           Triangulator tr = new Triangulator(pts);
           int[] indices = tr.Triangulate();
 
           // Create the Vector3 vertices
           Vector3[] vertices = new Vector3[pts.Length];
           for (int i=0; i<vertices.Length; i++) {
               vertices[i] = new Vector3(pts[i].x, pts[i].y, 0);
           }
 
 
           Mesh msh = new Mesh();
           msh.vertices = vertices;
           msh.triangles = indices;
           msh.RecalculateNormals();
           msh.RecalculateBounds();
           playerCarBehaviour.UpdateMesh(msh);*/
          pipeMeshGenerator.SetPoints(meshPoints, k);
          pipeMeshGenerator.RenderPipe();
          playerCarBehaviour.MeshUpdated(); }
        ResetLine();
        //Update Car
    }

    private void ResetLine()
    {
        
        lineRenderer.positionCount = 0;
        pointCount = 0;
        k = 0;
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
    public Vector2[] GetMeshPoints(LineRenderer line)
    {
        List<Vector2> p = new List<Vector2>();
        GameObject caret = null;
        caret = new GameObject("Lines");
 
        Vector3 left, right; // A position to the left of the current line
 
        
        for (var i = 0; i < line.positionCount - 1; i++)
        {
            caret.transform.position = line.GetPosition(i);
            caret.transform.LookAt(line.GetPosition(i + 1));
            right = caret.transform.position + transform.right * thickness;
            p.Add(right);
        }
        caret.transform.position = line.GetPosition(line.positionCount - 1);
        caret.transform.LookAt(line.GetPosition(line.positionCount - 2));
        left = caret.transform.position - transform.right * thickness;
        p.Add(left);

        // For all but the last point
        for (var i = 0; i < line.positionCount - 1; i++)
        {
            caret.transform.position = line.GetPosition(i);
            caret.transform.LookAt(line.GetPosition(i + 1));
            left = caret.transform.position - transform.right * thickness;
            p.Add(left);
        }
 
        // Last point looks backwards and reverses
        caret.transform.position = line.GetPosition(line.positionCount - 1);
        caret.transform.LookAt(line.GetPosition(line.positionCount - 2));
        right = caret.transform.position + transform.right * thickness;
        p.Add(right);
        Destroy(caret);
        return p.ToArray();
    }
}
