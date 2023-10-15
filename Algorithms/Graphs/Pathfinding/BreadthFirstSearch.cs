using LunarDoggo.Datastructures.Graphs;

namespace LunarDoggo.Algorithms.Graphs.Pathfinding
{
    //BFS is a basic pathfinding algorithm that works on unweighted graphs. It doesn't matter if the graph is directed
    //or undirected. The time complexity of BFS is O(|V| + |E|), as the algorithm processes every vertex and iterates over every edge in the graph exactly once
    //In simple graphs |E| is bounded by O(|V|^2), so in such cases, one could use O(|V|^2) as the time complexity 
    public class BreadthFirstSearch
    {
        public void Run(IGraph<BFSVertex> graph, Vertex<BFSVertex> start)
        {
            if (graph is null)
            {
                throw new ArgumentNullException("The graph the DFS should be run on cannot be null");
            }

            this.Initialize(graph, start);

            //BFS is usually implemented in a way that doesn't work on disconnected graphs. BFS can therefore be
            //used to find all vertices contained in a connected component of the graph.
            //BFS additionally is implemented iteratively using a queue. One could imagine the execution as a wave that propagates from the starting vertex to all reachable vertices of the graph
            Queue<Vertex<BFSVertex>> queue = new Queue<Vertex<BFSVertex>>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                Vertex<BFSVertex> current = queue.Dequeue();
                foreach (Vertex<BFSVertex> vertex in current.Adjacent)
                {
                    //If a processed adjacent vertex is found, it doesn't have to be processed again, as the distance from the start cannot be lower
                    //than the currently set distance
                    if (!vertex.Value.Processed)
                    {
                        vertex.Value.Distance = current.Value.Distance + 1;
                        vertex.Value.Predecessor = current;
                        vertex.Value.Processed = true;
                    }
                }
            }
        }

        private void Initialize(IGraph<BFSVertex> graph, Vertex<BFSVertex> start)
        {
            //Every vertex is initialized with the maximum possible distance to signify that the distance is invalid.
            foreach (Vertex<BFSVertex> vertex in graph.Vertices)
            {
                vertex.Value = new BFSVertex()
                {
                    Distance = Int32.MaxValue,
                    Predecessor = null,
                    Processed = false
                };
            }

            //The starting vertex, is guaranteed to be reachable from the start with a distance of 0 edges. The start also
            //doesn't have a predecessor, as it is an end vertex of every found start-vertex-path
            start.Value.Processed = true;
            start.Value.Distance = 0;
        }
    }

    public class BFSVertex
    {
        /// <summary>
        /// Returns whether this vertex has already been processed. The processed flag has to be set by the algorithm
        /// </summary>
        public bool Processed { get; set; }
        /// <summary>
        /// Returns the predecessor of this vertex on the path from the starting vertex
        /// </summary>
        public Vertex<BFSVertex> Predecessor { get; set; }
        /// <summary>
        /// Returns the distance in edges of this vertex to the start vertex
        /// </summary>
        public int Distance { get; set; }
    }
}