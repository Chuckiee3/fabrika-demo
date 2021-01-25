using System;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;

    public GameObject startWheel;
    public GameObject endWheel;
    private Vector3 startWheelPos;
    private Vector3 endWheelPos;
    public Rigidbody rb;
    private WheelBehaviour startWheelBehaviour;
    private WheelBehaviour endWheelBehaviour;
    private void Awake()
    {
        startWheelBehaviour = startWheel.GetComponent<WheelBehaviour>();
        endWheelBehaviour = endWheel.GetComponent<WheelBehaviour>();
        startWheel.SetActive(false);
        endWheel.SetActive(false);
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void MeshUpdated()
    {
        transform.rotation = Quaternion.identity;
        PlaceWheels();
        meshCollider.sharedMesh = meshFilter.mesh;
        startWheelBehaviour.MeshUpdated();
        endWheelBehaviour.MeshUpdated();
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    private void PlaceWheels()
    {
        var verts = meshFilter.mesh.vertices;
        startWheelPos = verts[0];
        endWheelPos = verts[verts.Length-1];
        startWheel.transform.localPosition = startWheelPos + Vector3.down *.2f;
        endWheel.transform.localPosition = endWheelPos+ Vector3.down *.2f;
        
        startWheel.SetActive(true);
        endWheel.SetActive(true);
    }
}
