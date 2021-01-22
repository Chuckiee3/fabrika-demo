using System;
using UnityEngine;

public class TargetFollowZ : MonoBehaviour
{
    public Transform Target;
    public float speed;
    private float offset;

    private void Awake()
    {
        offset = transform.position.z - Target.position.z;
    }

    private void Update()
    {
        var transform1 = transform;
        var pos = transform1.position;
        var position = Target.position;
        pos.z = position.z + offset;
        transform1.position = Vector3.Lerp(transform1.position, pos, Time.deltaTime * speed);
    }
}