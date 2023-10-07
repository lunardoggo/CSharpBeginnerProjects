namespace LunarDoggo.Datastructures.Graphs
{
    public interface IGraph<T>
    {
        /// <summary>
        /// Returns all vertices contained in this graph
        /// </summary>
        IEnumerable<Vertex<T>> Vertices { get; }
        /// <summary>
        /// Returns all edges contained in this graph
        /// </summary>
        IEnumerable<Edge<T>> Edges { get; }

        /// <summary>
        /// Removes the provided <paramref name="vertex"/> from this graph
        /// </summary>
        void RemoveVertex(Vertex<T> vertex);
        /// <summary>
        /// Adds a new vertex containing the provided <paramref name="value"/> to the graph and returns the
        /// newly created vertex
        /// </summary>
        Vertex<T> AddVertex(T value);

        /// <summary>
        /// Removes an existing Edge from <paramref name="from"/> to <paramref name="to"/> from the graph
        /// </summary>
        void RemoveEdge(Vertex<T> from, Vertex<T> to);
    }

    //Unweighted graphs can also be described as graphs where every edge has the same weight of a constant c.
    //This project uses two different interfaces to represent graphs noneteless, as unweighted graphs would
    //need to implement a parameter "weight" when adding edges, that the graph doesn't need and therefor
    //shouldn't implement

    /// <summary>
    /// Represents an unweighted graph that contains vertices containing values of type T
    /// </summary>
    public interface IUnweightedGraph<T> : IGraph<T>
    {
        /// <summary>
        /// Adds a new Edge from <paramref name="from"/> to <paramref name="to"/> to the graph. If the edge is
        /// directed or undirected depends on the implementation used
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        Edge<T> AddEdge(Vertex<T> from, Vertex<T> to);
    }

    /// <summary>
    /// Represents a weighted graph that contains vertices containing values of type T
    /// </summary>
    public interface IWeightedGraph<T> : IGraph<T>
    {
        /// <summary>
        /// Adds a new Edge from <paramref name="from"/> to <paramref name="to"/> with a weight of <paramref name="weight"/>
        /// to the graph. If the edge is directed or undirected depends on the implementation used
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        Edge<T> AddEdge(Vertex<T> from, Vertex<T> to, float weight);
        /// <summary>
        /// Returns the weight of the provided <see cref="Edge{T}"/>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        float GetWeight(Edge<T> edge);
    }
}