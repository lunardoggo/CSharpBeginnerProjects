# Graphs
In the field of graph theory, a _**graph**_ is an abstract structure that represents connections between objects. A graph at least consists of a Tuple _G=(V,E)_ of Vertices (V) and Edges (E).

Graphs have different properties you should know before trying to understand the algorithms that use this datastructure, as every algorithm only works with special kinds of graphs. This project will mainly focus on simple graphs:
  * A graph is either a **simple graph** or a **multi graph**. In simple graphs there may only be one edge between a given pair of vertices and three cannot be an edge from a vertex to itself (loop), whereas multi graphs allow for both an arbitrary number of edges between a given pair of vertices and loops.
  * A graph is either **directed** or **undirected**. Whereas edges in undirected graphs can be traversed in any direction, in directed graphs there may be edges that only allow for one-way traversal
  * A graph is either **weighted** or **unweighted**. A weighted graph uses a function _c(e)_ to assign a number/weight to every edge of the graph, the notation usually changes to _G=(V,E,c(e))_.
  * A graph is either **cyclic** or **acyclic**. Depending on if the graph is _directed_ or _undirected_, this property has different meanings:
    * An _undirected_ graph is acyclic if there is no pair of vertices _u,v_ in _V_, so that there is more than one distinct path from _u_ to _v_ or vice versa
    * A _directed_ graph is acyclic if there is vertex _v_ in _V_ for which there is a directed path of length greater than one that starts in _v_ and ends in _v_
  * An _undirected_ graph is called **connected** if there is a path from every Vertex _u_ in _V_ to every other Vertex _v_ in _V_. If a graph isn't connected, it's called **disconnected**
  * An _undirected_ graph is called **complete** if there is an edge _{u,v}_ between every pair of Vertices _u,v_ in _V_
  * A **bipartite** graph is a graph where the set of vertices _V_ can be divided into two non-empty, disjunct subsets _S_ and _T_ of _V_ where there is no edge connecting any two vertices in _S_ and there is no edge connecting any two vertices in _T_.
  * A **planar** graph is a graph that can be drawn _in a plane_ without crossing any edges except at their start/end vertex

For this project specifically you should also keep in mind that, as the focus lies on simple graphs, most graphs will at most have O(|V|^2) edges which facilitates determine the time complexity of graph algorithms.