using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueEdgeList
{
    public List<Edge> edgeList = new List<Edge>();

    public UniqueEdgeList()
    {
    }

    public UniqueEdgeList(UniqueEdgeList other)
    {
        foreach (Edge edge in other.edgeList)
        {
            this.Add(edge);
        }
    }

    public bool Add(Edge new_edge)
    {
        bool found = false;
        foreach (Edge edge in edgeList)
        {
            if (Edge.AreOverlappingEdges(edge, new_edge))
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            edgeList.Add(new_edge);
        }
        return !found;
    }

    public void Print()
    {
        foreach (Edge edge in edgeList)
        {
            Debug.Log(
                "\nEdge: " + (edge.x2 - edge.x1) + ", " + (edge.y2 - edge.y1) + ", " + (edge.z2 - edge.z1) + "\n" +
                "Edge Start: " + edge.x1 + ", " + edge.y1 + ", " + edge.z1 + "\n" +
                "Edge End: " + edge.x2 + ", " + edge.y2 + ", " + edge.z2 + "\n");
        }
    }

    /// <summary>
    /// This is a - b, not b - a
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static UniqueEdgeList SetDifference(UniqueEdgeList a, UniqueEdgeList b)
    {
        UniqueEdgeList result = new UniqueEdgeList();
        foreach (Edge edge_a in a.edgeList)
        {
            bool found = false;
            foreach (Edge edge_b in b.edgeList)
            {
                if (Edge.SameEdge(edge_a, edge_b))
                {
                    found = true;
                }
            }
            if (!found)
            {
                result.Add(edge_a);
            }
        }
        return result;
    }
}
