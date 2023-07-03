# delaunay-triangulation
## About
This is an implementation of a delaunay triangulation using the bowyer–watson algorithm. This code allows you to take a list of randomly generated points in 2D space, and generates a grid of triangles with the randomly generated points as the vertices. The delaunay triangulation is the backbone of other more complex generative processes such as making voronoi diagrams or barycentric dual meshes which is the intended purpose, but it can be used by itself for other projects that may require the creation of triangles from a 2D grid of points. 

## How to use
This project was designed to be very simple to use. It's all contained in a single class, and you only need to call one function for the list of triangles, though an additional luxury function **getAdjacencyMap()** is included if you wish to get a Dictionary that gives each triangle as a key and all triangles adjacent to it as a value, if that is something that would be helpful to you.

The first step is to create a **DelaunayTriangulation** object, and when doing so, making sure to call its constructor by passing it the width and height of the grid you're working with, followed by a List object containing all the points on the grid. All of the implementation is within the constructor and done in the background so you don't need to recall any additional functions

Once you've created the object, just call the **getTriangles()** and it will return a list of **Triangle** objects, That's it, you're all done. You know have a List of Triangles which can be rendered to the screen with a single function call, as well as have various other useful geometric operations and functionality included within it. 
*For further information on Triangle objects, visit the geometry repository for the README of that particular library.*

## Dependencies
The implementation of the DelaunayTriangulation class is heavily reliant on classes defined within a namespace called **"geometry"** in another of my repositories. This dependency can be found here: https://github.com/imnota4/geometry

## Examples

![example3](https://github.com/imnota4/delaunay-triangulation/assets/4397050/a6f88951-d313-4700-8d9e-09c57659f14b)
![example4](https://github.com/imnota4/delaunay-triangulation/assets/4397050/e0333855-b7bc-48c7-9496-0bb70f365378)

$~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~$*A 25x25 grid of triangles with different amounts of points* $~~~~~~~~$

