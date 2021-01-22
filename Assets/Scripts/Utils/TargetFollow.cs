using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    public Transform Target;
    public float speed;
    private void LateUpdate()
    {
        var transform1 = transform;
        var pos = transform1.position;
        var position = Target.position;
        pos.z = position.z;
        transform1.position = Vector3.Lerp(transform1.position, pos, Time.deltaTime * speed);
    }
}