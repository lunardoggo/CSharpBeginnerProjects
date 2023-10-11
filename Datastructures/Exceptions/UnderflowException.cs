namespace LunarDoggo.Datastructures.Exceptions
{
    /// <summary>
    /// An <see cref="Exception"/> signifying that there are no items to be removed in a collection
    /// </summary>
    public class UnderflowException : Exception
    {
        public UnderflowException(string message) : base(message)
        { }
    }
}
