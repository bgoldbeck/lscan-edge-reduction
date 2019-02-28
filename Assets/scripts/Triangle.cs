using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle
{
    public Edge edge1;
    public Edge edge2;
    public Edge edge3;

    public Triangle(Edge e1, Edge e2, Edge e3)
    {
        this.edge1 = e1;
        this.edge2 = e2;
        this.edge3 = e3;
    }

    public static bool AreNeighbors(Triangle t1, Triangle t2)
    {
        bool result = false;

        return result;
    }
}
