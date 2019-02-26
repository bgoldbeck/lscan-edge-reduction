using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleTest : MonoBehaviour
{
    float[,,] input_triangles = new float[,,]
    {
        {
            {0.0f, 0.0f, 0.0f},  // Triangle 0
            {0.5f, 0.5f, 0.0f},
            {0.0f, 0.5f, 0.0f},
        },
        {
            {0.0f, 0.0f, 0.0f},  // Triangle 1
            {0.5f, 0.0f, 0.0f},
            {0.5f, 0.5f, 0.0f},
        },
        {
            {0.5f, 0.0f, 0.0f},  // Triangle 2
            {1.0f, 0.5f, 0.0f},
            {0.5f, 0.5f, 0.0f},
        },
        {
            {0.5f, 0.0f, 0.0f},  // Triangle 3
            {1.0f, 0.0f, 0.0f},
            {1.0f, 0.5f, 0.0f},
        },
        {
            {0.5f, 0.5f, 0.0f},  // Triangle 4
            {1.0f, 1.0f, 0.0f},
            {0.5f, 1.0f, 0.0f},
        },
        {
            {0.5f, 0.5f, 0.0f},  // Triangle 5
            {1.0f, 0.5f, 0.0f},
            {1.0f, 1.0f, 0.0f},
        },
        {
            {0.0f, 0.5f, 0.0f},  // Triangle 6
            {0.5f, 1.0f, 0.0f},
            {0.0f, 1.0f, 0.0f},
        },
        {
            {0.0f, 0.5f, 0.0f},  // Triangle 7
            {0.5f, 0.5f, 0.0f},
            {0.5f, 1.0f, 0.0f},
        },
    };


    UniqueEdgeList all_edges = new UniqueEdgeList();
    UniqueEdgeList outline_edges = new UniqueEdgeList();
    UniqueEdgeList outline_edges_refined = new UniqueEdgeList();
    UniqueEdgeList shared_edges = new UniqueEdgeList();

    // Start is called before the first frame update
    void Start()
    {
        StepTwo();
        StepThree();
        return;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Color color = Color.red;
        DrawTriangles(color);
        return;
    }

    void DrawTriangles(Color color)
    {
        for (int m = 0; m < input_triangles.GetLength(0); ++m)
        {

            int i = 0;
            int k = i + 1;

            Debug.DrawLine(
                new Vector3(input_triangles[m, i, 0], input_triangles[m, i, 1], input_triangles[m, i, 2]),  // From
                new Vector3(input_triangles[m, k, 0], input_triangles[m, k, 1], input_triangles[m, k, 2]),  // To
                color);  // The line color.
            i = 1;
            k = 2;
            Debug.DrawLine(
                 new Vector3(input_triangles[m, i, 0], input_triangles[m, i, 1], input_triangles[m, i, 2]),  // From
                 new Vector3(input_triangles[m, k, 0], input_triangles[m, k, 1], input_triangles[m, k, 2]),  // To
                 color);  // The line color.
            i = 2;
            k = 0;
            Debug.DrawLine(
                 new Vector3(input_triangles[m, i, 0], input_triangles[m, i, 1], input_triangles[m, i, 2]),  // From
                 new Vector3(input_triangles[m, k, 0], input_triangles[m, k, 1], input_triangles[m, k, 2]),  // To
                 color);  // The line color.

        }
        return;
    }

    // Step 1. Seperate faces. (Group tris by normals)
    void StepOne()
    {
        return;
    }

    // Step 2. Remove shared edges.
    // Input: List of triangles.
    // Output: List of edges that were not shared by other edges in the triangles.
    void StepTwo()
    {
        // Check every triangle against all other triangles.
        for (int m = 0; m < input_triangles.GetLength(0); ++m)
        {
            for (int n = 0; n < input_triangles.GetLength(0); ++n)
            {
                // Not the same triangle.
                if (m != n)
                {
                    for (int i = 0; i < 3; ++i)
                    {
                        int j = i + 1;
                        if (i == 2)
                        {
                            j = 0;
                        }

                        Edge tri1_edge = new Edge(
                            input_triangles[m, i, 0], input_triangles[m, i, 1], input_triangles[m, i, 2],
                            input_triangles[m, j, 0], input_triangles[m, j, 1], input_triangles[m, j, 2]);

                        all_edges.Add(tri1_edge);

                        // a --> b == (b - a)
                        Edge tri2_edge01 = new Edge(
                            input_triangles[n, 0, 0], input_triangles[n, 0, 1], input_triangles[n, 0, 2],
                            input_triangles[n, 1, 0], input_triangles[n, 1, 1], input_triangles[n, 1, 2]);

                        Edge tri2_edge12 = new Edge(
                            input_triangles[n, 1, 0], input_triangles[n, 1, 1], input_triangles[n, 1, 2],
                            input_triangles[n, 2, 0], input_triangles[n, 2, 1], input_triangles[n, 2, 2]);

                        Edge tri2_edge20 = new Edge(
                            input_triangles[n, 2, 0], input_triangles[n, 2, 1], input_triangles[n, 2, 2],
                            input_triangles[n, 0, 0], input_triangles[n, 0, 1], input_triangles[n, 0, 2]);

                        if (Edge.AreOverlappingEdges(tri1_edge, tri2_edge01))
                        {
                            shared_edges.Add(tri1_edge);
                        }
                        if (Edge.AreOverlappingEdges(tri1_edge, tri2_edge12))
                        {
                            shared_edges.Add(tri1_edge);
                        }
                        if (Edge.AreOverlappingEdges(tri1_edge, tri2_edge20))
                        {
                            shared_edges.Add(tri1_edge);
                        }
                    }

                }
            }

        }
        outline_edges = UniqueEdgeList.SetDifference(all_edges, shared_edges);

        //shared_edges.Print();
        //Debug.Log("OUTLINE EDGES");
        //outline_edges.Print();
        //all_edges.Print();
        return;
    }

    // Step 3. Simply outline. (Remove edges that are connecting and colinear)
    // Input: List of edges.
    // Output: List of edges with no colinear and connecting edges.
    void StepThree()
    {
        foreach (Edge edge_outer in outline_edges.edgeList)
        {
            foreach (Edge edge_inner in outline_edges.edgeList)
            {
                if (!Edge.SameEdge(edge_inner, edge_outer))
                {
                    float[] shared_vertex = Edge.HasSharedVertex(edge_inner, edge_outer);
                    bool is_parallel = Edge.AreParallelOrAntiParallel(edge_inner, edge_outer);

                    if (shared_vertex != null && is_parallel)
                    {
                        float[] start_vertex = new float[] {edge_inner.x1, edge_inner.y1, edge_inner.z1};
                        if (edge_inner.x1 == shared_vertex[0] &&
                            edge_inner.y1 == shared_vertex[1] &&
                            edge_inner.z1 == shared_vertex[2])
                        {

                            start_vertex = new float[] {edge_inner.x2, edge_inner.y2, edge_inner.z2};
                        }
                    
                        float[] end_vertex = new float[] { edge_outer.x1, edge_outer.y1, edge_outer.z1 };
                        if (edge_outer.x1 == shared_vertex[0] &&
                            edge_outer.y1 == shared_vertex[1] &&
                            edge_outer.z1 == shared_vertex[2])
                        {

                            end_vertex = new float[] { edge_outer.x2, edge_outer.y2, edge_outer.z2 };
                        }

                        outline_edges_refined.Add(
                            new Edge(
                                start_vertex[0], start_vertex[1], start_vertex[2], // Edge Start
                                end_vertex[0], end_vertex[1], end_vertex[2]  // Edge end
                                ));
                    }
                }
            }
        }
        outline_edges_refined.Print();
        return;
    }

    // Step 4. Triangulate (With library)

}
