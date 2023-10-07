namespace LunarDoggo.Datastructures.Graphs
{
    //This class is generic to allow for custom values in a vertex without needing to make a custom implementation of this class
    public class Vertex<T>
    {
        //A HashSet facilitates checking if two vertices are adjacent, as hashing allows for accessing the information if
        //another vertex is contained in adjacent in O(1) time. Keep in mind though that there is no resolution mechanism
        //for hash collisions using this method. For objects like Instances of Vertex<T> this usually doesn't do harm because
        //GetHashCode(), which is used to determine the hash is distinct between different instances of Vertex<T> unless the
        //method is overridden
        private readonly HashSet<Vertex<T>> adjacent = new HashSet<Vertex<T>>();

        public Vertex(int id, T value)
        {
            this.Value = value;
            this.Id = id;
        }

        /// <summary>
        /// Returns the Id of this vertex in the <see cref="IGraph"/> that created it
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// Returns or updates the value that's stored inside of this vertex
        /// </summary>
        public T? Value { get; set; }
        /// <summary>
        /// Returns a collection of all Vertices adjacent to this vertex
        /// </summary>
        public IEnumerable<Vertex<T>> Adjacent { get => this.adjacent.AsEnumerable(); }

        /// <summary>
        /// Returns if there is an edge from this vertex to <paramref name="other"/>
        /// </summary>
        public bool IsAdjacent(Vertex<T> other)
        {
            //this part doesn't require thread safety, as 1. the HashSet isn't modified by this check and
            //2. other threads could modify the adjacent-HashSet right after this check regardless
            return this.adjacent.Contains(other);
        }

        /// <summary>
        /// Adds a new edge from this vertex to <paramref name="other"/> to the graph; this method is
        /// marked as internal, because allowing arbitrary changes to edges from other sources than
        /// the graph itself will lead to inconsistent data
        /// </summary>
        internal void AddAdjacency(Vertex<T> other)
        {
            //ensure thread safety when modifying adjacent
            lock (this.adjacent)
            {
                //The vertex is not concerned if the graph it's contained in is directed or undirected, as a result,
                //adjacencies of undirected graphs will have to be managed by the graph isntance itself
                //Additionally, using a HashSet has the advantage that one doesn't have to check if "other" actually
                //is contained in the HashSet before adding a new entry (https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.add?view=net-6.0)
                this.adjacent.Add(other);
            }
        }

        /// <summary>
        /// Removes the edge from this vertex to <paramref name="other"/> from the graph; this method is
        /// marked as internal, because allowing arbitrary changes to edges from other sources than
        /// the graph itself will lead to inconsistent data
        /// </summary>
        internal void RemoveAdjacency(Vertex<T> other)
        {
            //ensure thread safety when modifying adjacent
            lock (this.adjacent)
            {
                //The vertex is not concerned if the graph it's contained in is directed or undirected, as a result,
                //adjacencies of undirected graphs will have to be managed by the graph isntance itself
                //Additionally, using a HashSet has the advantage that one doesn't have to check if "other" actually
                //is contained in the HashSet before adding a new entry (https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.remove?view=net-6.0)
                this.adjacent.Remove(other);
            }
        }
    }
}
