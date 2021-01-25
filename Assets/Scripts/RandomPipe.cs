using UnityEngine;

[RequireComponent(typeof(PipeMeshGenerator))]
public class RandomPipe : MonoBehaviour {

    public int numberOfPoints;
    public float range;

    void Start() {
        PipeMeshGenerator pmg = GetComponent<PipeMeshGenerator>();
        for (int i = 0; i < numberOfPoints; i++) {
            pmg.points.Add(Vector3.right * i);
        }
        pmg.RenderPipe();
    }

}