namespace LunarDoggo.Algorithms.Sorting
{
    public class InsertionSort<T> : ISortingAlgorithm<T> where T : IComparable<T>
    {
        //Insertion sort is one of the most basic sorting algorithms out there. The basic principle is
        //to iterate over the provided array from left to right and in each iteration step move all
        //items to the left that are greater than the current item one cell to the right, while
        //inserting the current item back into the array at the correct index.
        //InsertionSort has a worst-case and average-case time complexity of O(n^2), whereas, in the
        //best-case, the algorithm terminates after O(n) operations (for example, if the array is already
        //sorted in ascending order)
        //Additionally InsertionSort is an in situ algorithm that's stable, meaning that the sorting needs
        //only O(1) additional storage for the algorithm (in situ) and all groups of items that are equal
        //are kept in the same order
        public void Sort(T[] values)
        {
            //i starts at 1, as sorting only makes sense if there at least two values to be sorted
            for (int i = 1; i < values.Length; i++)
            {
                T key = values[i];
                int index = i - 1;

                //Move values[index] to the right if it's greater than key. This results in moving all
                //items that are to the left of key and are greater than key one cell to the right.
                while(index >= 0 && values[index].CompareTo(key) > 0)
                {
                    values[index + 1] = values[index];
                    index--;
                }
                //Lastly the value contained in "key" has to be inserted back into the array, so that
                //the array contains the same items after every step, but now the first i cells are sorted
                values[index + 1] = key;
            }
        }
    }
}