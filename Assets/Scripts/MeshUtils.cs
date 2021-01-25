using System.Collections.Generic;
using UnityEngine;

public static class MeshUtils
{
    public static Mesh CreateMeshFromPoints(Vector3[] points, float drawingThickness, float meshDepth)
    {
        
        var newPoints = CreatePointsFromCenter(points, drawingThickness / 2);
        //TODO fix this part
        /*int i = 0;
        for (int j = 0; j <triangles.Length; j+=6)
        {
            
            triangles[j] = i; 
            triangles[j + 1] = i + 1;
            triangles[j + 2] = i + 2;
            
            triangles[j + 3] = i + 2;
            triangles[j + 4] = i + 3;
            triangles[j + 5] = i ;
            i += 2;
            Debug.Log(i);
        }*/
        
        Triangulator tr = new Triangulator(newPoints);
        int[] indices = tr.Triangulate();

        Vector3[] vertices = new Vector3[newPoints.Length];
        for (int i=0; i<vertices.Length; i++) {
            vertices[i] = new Vector3(newPoints[i].x, newPoints[i].y, 0);
        }
        Mesh m = new Mesh();
        m.vertices = vertices;
        m.triangles = indices;
        m.RecalculateNormals();
        m.RecalculateBounds();
        //m.Optimize();
        return m;
    }

    public static Vector2[] CreatePointsFromCenter(Vector3[] points, float dist)
    {
        Vector2[] newPoints = new Vector2[points.Length * 2];
        int k = 0;
        Vector3 lastV = Vector3.zero;
        for (int i = 0; i < points.Length - 1; i++)
        {
            var diff = points[i] - points[i + 1];
            var n = diff / diff.magnitude;
            var v = n;
            v.x = -n.y;
            v.y = n.x;
            v *= dist;
            lastV = v;
            var p3 = points[i] + v;
            var p5 = points[i] - v;
            newPoints[k] = p3;
            newPoints[k + 1] = p5;
            
            k += 2;
        }
        var p4 = points[points.Length - 1] + lastV;
        var p6 = points[points.Length - 1] - lastV;
        newPoints[k ] = p6;
        newPoints[k + 1] = p4;


        return newPoints;
    }

    
}
