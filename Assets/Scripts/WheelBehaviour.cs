using System;
using UnityEngine;

public class WheelBehaviour : MonoBehaviour
{
    public WheelCollider wheelCollider;


    public float maxVelocity = 360;
    // Update is called once per frame
    public float angle = 90;    // assign correct angle in the inspector (90°)
    public float currVelocity = 0;
    public float a = 4;

    public void MeshUpdated()
    {
        currVelocity = 0;
    }
    void Update()
    {
        if (currVelocity <= maxVelocity)
        {
            currVelocity += a * Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        wheelCollider.motorTorque = currVelocity;
        wheelCollider.steerAngle = angle;
    }
}
