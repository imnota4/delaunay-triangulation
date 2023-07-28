using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Geometry;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace Algorithms.Delaunay
{
    public class DelaunayTriangulation
    {
        private List<Triangle> triangles;

        public List<Triangle> getTriangles()
        {
            return triangles;
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

        private void OptimizedEditor()
        {

        }

        public DelaunayTriangulation(float width, float height, List<Point> points)
        {

            const float BAD_ANGLE = 12;

            // Supra triangle vertices
            Point vertex1 = new Point(0 - Mathf.Tan(30 * (Mathf.PI / 180)) * height, 0);
            Point vertex2 = new Point(width + Mathf.Tan(30 * (Mathf.PI / 180)) * height, 0);
            Point vertex3 = new Point(width / 2, height + Mathf.Tan(60 * (Mathf.PI / 180)) * (width / 2));


            triangles = new List<Triangle>
        {
            new Triangle(vertex1, vertex2, vertex3)
        };

            foreach (Point point in points)
            {
                List<Triangle> badTriangles = new List<Triangle>();
                List<(Point, Point)> edgeList = new List<(Point, Point)>();

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

                List<(Point, Point)> edgesToRemove = new List<(Point, Point)>();

                foreach ((Point, Point) edge1 in edgeList)
                {
                    foreach ((Point, Point) edge2 in edgeList)
                    {
                        if (edge1.Item1.Equals(edge2.Item2) && edge1.Item2.Equals(edge2.Item1))
                        {
                            edgesToRemove.Add(edge1);
                            edgesToRemove.Add(edge2);
                        }
                    }
                }

                foreach ((Point, Point) edgeToRemove in edgesToRemove)
                {
                    edgeList.Remove(edgeToRemove);
                }

                foreach ((Point, Point) edge in edgeList)
                {
                    triangles.Add(new Triangle(edge.Item1, edge.Item2, point));
                }

               
            }

            triangles.RemoveAll(triangle => triangle.containsVertice(vertex1) || triangle.containsVertice(vertex2) || triangle.containsVertice(vertex3));
            triangles.RemoveAll(triangle => triangle.getAngle(triangle.getEdges()[0], triangle.getEdges()[1]) < BAD_ANGLE || triangle.getAngle(triangle.getEdges()[1], triangle.getEdges()[2]) < BAD_ANGLE || triangle.getAngle(triangle.getEdges()[0], triangle.getEdges()[2]) < BAD_ANGLE);
        }
    }
}
