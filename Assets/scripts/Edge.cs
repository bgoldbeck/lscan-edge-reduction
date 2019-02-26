using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{

    // (Start 1) *----------->* (End 2)
    // Start
    public float x1;
    public float y1;
    public float z1;

    // End
    public float x2;
    public float y2;
    public float z2;

    public Edge(float x1, float y1, float z1, float x2, float y2, float z2)
    {
        this.x1 = x1;
        this.x2 = x2;
        this.y1 = y1;
        this.y2 = y2;
        this.z1 = z1;
        this.z2 = z2;
    }

    public void Print()
    {
        float[] e = new float[] { this.x2 - this.x1, this.y2 - this.y1, this.z2 - this.z1 };
        Debug.Log("Edge Slope: " + e[0] + ", " + e[1] + ", " + e[2]);
        return;
    }


    public static bool SameEdge(Edge a, Edge b)
    {
        return Mathf.Approximately(a.x1, b.x1) &&
               Mathf.Approximately(a.y1, b.y1) &&
               Mathf.Approximately(a.z1, b.z1) &&
               Mathf.Approximately(a.x2, b.x2) &&
               Mathf.Approximately(a.y2, b.y2) &&
               Mathf.Approximately(a.z2, b.z2);
    }

    public static bool AreOverlappingEdges(Edge a, Edge b)
    {

        // Case 1. Exactly the same edges.
        if (SameEdge(a, b))
        {
            return true;
        }
        // Case 2. Edges in opposite directions.
        if (
            // 'a' start vs 'b' end
            Mathf.Approximately(a.x1, b.x2) && 
            Mathf.Approximately(a.y1, b.y2) &&
            Mathf.Approximately(a.z1, b.z2) &&
            // 'b' start vs 'a' end
            Mathf.Approximately(b.x1, a.x2) && 
            Mathf.Approximately(b.y1, a.y2) &&
            Mathf.Approximately(b.z1, a.z2))
        {
            return true;
        }
        return false;
    }

    public static float[] HasSharedVertex(Edge a, Edge b)
    {
        float[] result = null;
        // Case 1. End of a == Start of b
        if (Mathf.Approximately(a.x2, b.x1) && 
            Mathf.Approximately(a.y2, b.y1) &&
            Mathf.Approximately(a.z2, b.z1))
        {
            result = new float[] {a.x2, a.y2, a.z2};
        }

        // Case 2. Start of a == Start of b
        if (Mathf.Approximately(a.x1, b.x1) && 
            Mathf.Approximately(a.y1, b.y1) &&
            Mathf.Approximately(a.z1, b.z1))
        {
            result = new float[] {a.x1, a.y1, a.z1};
        }

        // Case 3. Start of a == End of b
        if (Mathf.Approximately(a.x1, b.x2) && 
            Mathf.Approximately(a.y1, b.y2) &&
            Mathf.Approximately(a.z1, b.z2))
        {
            result = new float[] {a.x1, a.y1, a.z1};
        }

        // Case 4. End of a == End of b
        if (Mathf.Approximately(a.x2, b.x2) && 
            Mathf.Approximately(a.y2, b.y2) &&
            Mathf.Approximately(a.z2, b.z2))
        {
            result = new float[] {a.x2, a.y2, a.z2};
        }
        return result;
    }

    public static bool AreParallelOrAntiParallel(Edge a, Edge b)
    {
        float[] e1 = new float[] { a.x2 - a.x1, a.y2 - a.y1, a.z2 - a.z1 };
        float[] e2 = new float[] { b.x2 - b.x1, b.y2 - b.y1, b.z2 - b.z1 };

        float e1_mag = Mathf.Sqrt(
            Mathf.Pow(e1[0], 2.0f) + 
            Mathf.Pow(e1[1], 2.0f) + 
            Mathf.Pow(e1[2], 2.0f));

        float e2_mag = Mathf.Sqrt(
               Mathf.Pow(e2[0], 2.0f) +
               Mathf.Pow(e2[1], 2.0f) +
               Mathf.Pow(e2[2], 2.0f));
        float dot = (e1[0] * e2[0]) + (e1[1] * e2[1]) + (e1[2] * e2[2]);
        
        float angle = Mathf.Acos((dot) / (e1_mag * e2_mag)) * Mathf.Rad2Deg;

        return (angle == 0.0 || angle == 180.0);
    }
}
