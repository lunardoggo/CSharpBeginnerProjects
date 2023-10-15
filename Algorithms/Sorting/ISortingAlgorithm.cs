namespace LunarDoggo.Algorithms.Sorting
{
    //This interface is used to keep the public interface of all sorting algorithms implemented here clean and uniform.
    //The type to be sorted must implement IComparable<T> so that there is a way to tell which value is equal to, greater
    //or less than another one
    public interface ISortingAlgorithm<T> where T : IComparable<T>
    {
        /// <summary>
        /// Sorts the provided <paramref name="values"/> in ascending order
        /// </summary>
        void Sort(T[] values);
    }
}