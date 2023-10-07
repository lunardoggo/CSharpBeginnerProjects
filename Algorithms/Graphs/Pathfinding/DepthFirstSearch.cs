using LunarDoggo.Datastructures.Graphs;

namespace LunarDoggo.Algorithms.Graphs.Pathfinding
{
    //DFS is a basic pathfinding algorithm that works on unweighted graphs. It doesn't matter if the graph is directed
    //or undirected, but a DFS run on a directed graph will yield a s-rooted spanning tree that isn't necessarily minimal
    //in weight if the graph contains at least one
    //The time complexity of DFS is O(|V| + |E|), as the algorithm processes every vertex and iterates over every edge exactly once.
    //In simple graphs |E| is bounded by O(|V|^2), so in such cases one could also use O(|V|^2) as the time complexity
    public class DepthFirstSearch
    {
        public void Run(IGraph<DFSVertex> graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException("The graph the DFS should be run on cannot be null");
            }

            this.Initialize(graph);
            //Current processing time
            int time = 0;

            //DFS is usually also implemented in a way that works on disconnected graphs by iterating over all vertices.
            //If a vertex hasn't been processed yet, it must be in a component of the graph that's unreachable from the
            //Previously processed part and therefor the recursion is started once again.
            foreach (Vertex<DFSVertex> vertex in graph.Vertices)
            {
                if (!vertex.Value!.Processed)
                {
                    time = this.RunRecursively(graph, vertex, time);
                }
            }
        }

        private int RunRecursively(IGraph<DFSVertex> graph, Vertex<DFSVertex> start, int startTime)
        {
            //The difference between ++startTime and startTime++ is, that ++startTime will execute the increment before the assignment
            //and startTime++ afterwards. This shorthand assignment will be used here to shorten the code
            start.Value!.StartTime = startTime++;

            //DFS works iterative. One could picture it as an algorithm that walks as far as it can before turning back and processing
            //the adjacencies of previously processed vertices
            foreach (Vertex<DFSVertex> adjacent in start.Adjacent)
            {
                //Only vertices that haven't been processed yet need to be regarded further
                if (!adjacent.Value!.Processed)
                {
                    adjacent.Value.Predecessor = start;
                    this.RunRecursively(graph, adjacent, startTime);
                }
            }

            //After setting the end time the processing of the vertex is finished
            start.Value!.EndTime = startTime++;
            return startTime;
        }

        private void Initialize(IGraph<DFSVertex> graph)
        {
            foreach (Vertex<DFSVertex> vertex in graph.Vertices)
            {
                //Make sure that there is no vertex that doesn't have any data attached to it; this is not part of
                //the original algorithm
                if (vertex.Value is null)
                {
                    vertex.Value = new DFSVertex();
                }

                //The original algorithm inizializes every vertex with invalid start and end times, as well as a null predecessor
                //Instead of the processed flag, some implementations use colors to facilitate determining the time complexity of
                vertex.Value.StartTime = -1;
                vertex.Value.EndTime = -1;
                vertex.Value.Predecessor = null;
            }
        }
    }

    public class DFSVertex
    {
        /// <summary>
        /// Returns whether this vertex has been processed yet. It is considered processed if the start time is valid
        /// </summary>
        public bool Processed { get => this.StartTime != -1; }
        /// <summary>
        /// Returns the predecessor of this vertex on the path from the previous vertex
        /// </summary>
        public Vertex<DFSVertex>? Predecessor { get; set; }
        /// <summary>
        /// Returns the start time of the processing of this vertex
        /// </summary>
        public int StartTime { get; set; }
        /// <summary>
        /// Returns the end time of the processing of this vertex
        /// </summary>
        public int EndTime { get; set; }
    }
}