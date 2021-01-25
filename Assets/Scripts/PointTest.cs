using System;
using UnityEditor;
using UnityEngine;

public class PointTest : MonoBehaviour
{
    public Vector2[] points;
    public float dist;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
    public int[] triangles;
    public Vector3[] meshPoints;
    public Mesh currMesh;
    private PolyExtruder polyExtruder;
    private GameObject polyExtruderGO;
    private void Awake()
    {
        polyExtruderGO = new GameObject();
        polyExtruderGO.transform.SetParent(this.transform);

// add PolyExtruder script to newly created GameObject,
// keep track of its reference, and name it
        polyExtruder = polyExtruderGO.AddComponent<PolyExtruder>();
        polyExtruderGO.name = "CarMesh";

// run poly extruder according to input data
        polyExtruder.createPrism(polyExtruderGO.name, .5f, points, Color.grey, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateMesh();
        }
    }

    private void UpdateMesh()
    {
        Debug.Log("Update mesh");
       //currMesh = MeshUtils.CreateMeshFromPoints(points,  0.5f, 2);
       polyExtruder.createPrism(polyExtruderGO.name, .5f, points, Color.grey, true);
       meshFilter.mesh = currMesh;
       triangles = currMesh.triangles;
       meshPoints = currMesh.vertices;
    }

    private void OnDrawGizmosSelected()
    {
        if(points == null ||currMesh == null || triangles == null) return;
        
       /* foreach (var i in meshPoints)
        {
            Handles.Label(i + Vector3.up *.15f, i.ToString());
            Gizmos.DrawSphere(i, .025f);
        }*/
      
    }
}
