namespace LunarDoggo.Datastructures.Graphs
{
    public class UndirectedUnweightedGraph<T> : IUnweightedGraph<T>
    {
        //Using HashSets for storing the vertices and edges, again, facilitates managing the collections more easily,
        //as it makes removing and checking for entries easier while avoiding duplicate entries
        private HashSet<Vertex<T>> vertices = new HashSet<Vertex<T>>();
        private HashSet<Edge<T>> edges = new HashSet<Edge<T>>();

        //If the original HashSet was returned, it would be possible to modify the original collection of vertices/edges
        //therefor the LINQ method ToArray() is called to return a copy of the original sets
        public IEnumerable<Vertex<T>> Vertices { get => this.vertices.ToArray(); }
        public IEnumerable<Edge<T>> Edges { get => this.edges.ToArray(); }

        private int lastId = 0;

        public Edge<T> AddEdge(Vertex<T> from, Vertex<T> to)
        {
            //this lock ensures thread safety when adding edges, as multithreaded Add operations without locks can lead to null
            //entries in the collection
            lock (this.edges)
            {
                //The ArgumentNullException advertised in the definition of AddEdge in IUnweightedGraph will be thrown by the
                //constructor of Edge<T>, therefor it isn't necessary to check from and to for null values here
                Edge<T> edge = new Edge<T>(from, to, true);

                //As the edge and adjacencies are stored in HashSets, it isn't necessary to check if it already exists
                this.edges.Add(edge);
                from.AddAdjacency(to);
                to.AddAdjacency(from);

                return edge;
            }
        }

        public Vertex<T> AddVertex(T value)
        {
            //lock is used to ensure thread safety when incrementing lastId. The lock is set on vertices because
            //locks can only be set on reference types and lastId, as an Int32, is a ValueType
            lock (this.vertices)
            {
                Vertex<T> vertex = new Vertex<T>(this.lastId, value);
                //If an exception ocurrs when adding a vertex, the increment to lastId will not happen, which is intended behavior
                this.vertices.Add(vertex);
                this.lastId++;
                return vertex;
            }
        }

        public void RemoveEdge(Vertex<T> from, Vertex<T> to)
        {
            lock (this.edges)
            {
                //As edges that contain the same values are considered equal and all edges are stored in a HashSet,
                //edges can be removed by removing a new and equal edge from edges
                Edge<T> edge = new Edge<T>(from, to, true);
                this.edges.Remove(edge);
                //The adjacency lists of the vertices also have to be updated
                from.RemoveAdjacency(to);
                to.RemoveAdjacency(from);
            }
        }

        public void RemoveVertex(Vertex<T> vertex)
        {
            lock (this.vertices)
            {
                this.vertices.Remove(vertex);
                //the provided vertex is only removed from the adjacency lists of the remaining vertices without clearing
                //its own adjacency list, as it is no longer part of the graph and will likely be garbage collected soon
                foreach (Vertex<T> v in this.vertices)
                {
                    v.RemoveAdjacency(vertex);
                }
            }
        }
    }
}