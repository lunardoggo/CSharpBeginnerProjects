namespace LunarDoggo.Datastructures.Graphs
{
    //This class also has to be generic, as Vertex<T> is already generic and this class depends on Vertex<T>
    //Implementing IEquatable<Edge<T>> helps to compare different instances of Edge<T> for equal values
    public class Edge<T>
    {
        /// <exception cref="ArgumentNullException"></exception>
        public Edge(Vertex<T> from, Vertex<T> to, bool bidirectional)
        {
            //this check helps to avoid NullReferenceExceptions down the line
            if (from is null || to is null)
            {
                throw new ArgumentNullException("None of the vertices of an edge can be null!");
            }

            this.IsBidirectional = bidirectional;
            this.From = from;
            this.To = to;
        }

        /// <summary>
        /// Returns whether this edge is bidirecional (i.e. if it's undirected)
        /// </summary>
        public bool IsBidirectional { get; }
        /// <summary>
        /// Returns the <see cref="Vertex{T}"/> the edge starts at
        /// </summary>
        public Vertex<T> From { get; }
        /// <summary>
        /// Returns the <see cref="Vertex{T}"/> the edge ends at
        /// </summary>
        public Vertex<T> To { get; }

        //Equals() is overriden because HashSets are used to store edges and therefor it's easier to avoid
        //duplicate entries if edges containing the same values are considered equal instead of relying on
        //reference equality
        public override bool Equals(object? obj)
        {
            //If the provided object is an edge all edge properties are compared. As there can be multiple vertices containing the same
            //value, two vertices are considered distinct, even if they contain the same value. As a result the Equals() method of 
            //Vertex<T> isn't overriden with a custom implementation
            return obj is Edge<T> edge && edge.From == this.From && edge.To == this.To && edge.IsBidirectional == this.IsBidirectional;
        }

        //As the Equals method is overriden, GetHashCode has to be overriden as well
        public override int GetHashCode()
        {
            //Since dotnet core 2.1 there is HashCode.Combine() which facilitates creating hash codes from multiple objects
            return HashCode.Combine(this.From, this.To, this.IsBidirectional);
        }
    }
}
