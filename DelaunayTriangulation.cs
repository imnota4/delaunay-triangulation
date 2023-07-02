using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Geometry;
using Unity.VisualScripting.FullSerializer;
using System.Collections.ObjectModel;
using System.Linq;
using System;


namespace Algorithms.Delaunay
{
    public class DelaunayTriangulation
    {
        private List<Triangle> triangles;
        private Dictionary<Triangle, List<Triangle>> hashmap;

        public List<Triangle> getTriangles()
        {
            return triangles;
        }

        public Dictionary<Triangle, List<Triangle>> getAdjacencyMap()
        {
            return hashmap;
        }

        private Dictionary<Triangle, List<Triangle>> generateTriangleMap(List<Triangle> triangles)
        {
            var adjacencyMap = new Dictionary<Triangle, List<Triangle>>();

            // Iterate over each triangle
            foreach (var triangle in triangles)
            {
                // Initialize the adjacency list for the current triangle
                adjacencyMap[triangle] = new List<Triangle>();

                // Check each triangle against the current triangle for edge sharing
                foreach (var otherTriangle in triangles)
                {
                    if (otherTriangle != triangle && triangle.sharesEdge(otherTriangle))
                    {
                        // Add the triangle to the adjacency list
                        adjacencyMap[triangle].Add(otherTriangle);
                    }
                }
            }

            return adjacencyMap;
        }

        public DelaunayTriangulation(float width, float height, List<Vector2> points)
        {

            Vector2 vertex1 = new Vector2(0 - Mathf.Tan(30 * (Mathf.PI / 180)) * height, 0);
            Vector2 vertex2 = new Vector2(width + Mathf.Tan(30 * (Mathf.PI / 180)) * height, 0);
            Vector2 vertex3 = new Vector2(width / 2, height + Mathf.Tan(60 * (Mathf.PI / 180)) * (width / 2));


            triangles = new List<Triangle>
        {
            new Triangle(vertex1, vertex2, vertex3)
        };

            foreach (Vector2 point in points)
            {
                List<Triangle> badTriangles = new List<Triangle>();
                List<(Vector2, Vector2)> edgeList = new List<(Vector2, Vector2)>();

                foreach (Triangle triangle in triangles)
                {
                    if (triangle.getCircumcircle().isPointWithin(point))
                    {
                        badTriangles.Add(triangle);
                        edgeList.Add((triangle.getVertices()[0], triangle.getVertices()[1]));
                        edgeList.Add((triangle.getVertices()[1], triangle.getVertices()[2]));
                        edgeList.Add((triangle.getVertices()[2], triangle.getVertices()[0]));
                    }
                }

                triangles.RemoveAll(triangle => badTriangles.Contains(triangle));

                List<(Vector2, Vector2)> edgesToRemove = new List<(Vector2, Vector2)>();

                foreach ((Vector2, Vector2) edge1 in edgeList)
                {
                    foreach ((Vector2, Vector2) edge2 in edgeList)
                    {
                        if (edge1.Item1.Equals(edge2.Item2) && edge1.Item2.Equals(edge2.Item1))
                        {
                            edgesToRemove.Add(edge1);
                            edgesToRemove.Add(edge2);
                        }
                    }
                }

                foreach ((Vector2, Vector2) edgeToRemove in edgesToRemove)
                {
                    edgeList.Remove(edgeToRemove);
                }

                foreach ((Vector2, Vector2) edge in edgeList)
                {
                    triangles.Add(new Triangle(edge.Item1, edge.Item2, point));
                }

               
            }

            triangles.RemoveAll(triangle => triangle.containsVertice(vertex1) || triangle.containsVertice(vertex2) || triangle.containsVertice(vertex3));
            hashmap = generateTriangleMap(triangles);
        }
    }
}
