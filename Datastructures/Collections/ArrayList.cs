namespace LunarDoggo.Datastructures.Collections
{
    //An array list ist a list that uses an array for data storage with one twist: array lists are usually implemented to have
    //variable length in contrast to arrays which have a fixed length. To achieve that goal, array lists keep track of the number
    //of items stored and allocate a new array with a bigger or smaller size, as soon as a item count threshold is met. Most
    //commonly array lists double in size if the array is half filled. Most basic implementations don't shrink the array if most
    //items are removed from it, but this implementation showcases how shrinking the underlying array could work, allocating a new
    //smaller array with half the size of the original one if a item count threshold of 1/8th the length of the array is met.
    //To ensure that the array always stays viable, the minimum array size is set to 8
    //In contrast to linked lists, the time complexity of deleting an item is O(n) instead of O(1) with n being the number of items
    //that is currently stored, but array lists often are preferred over linked lists, as they are generally easier to work with,
    //because you don't have to keep track of LinkedListItems and therefor you cannot break the list by accident that easily
    public class ArrayList<T> where T : class
    {
        /// <summary>
        /// Keeps track of the index of the last item in the list
        /// </summary>
        private int currentIndex = -1;
        /// <summary>
        /// Stores all items contained in the list
        /// </summary>
        private T[] items;

        //Array lists don't require a initial capacity, as they are variable in size. In this implementation a initial capacity
        //8 items is chosen by default
        public ArrayList() : this(8)
        { }

        //But optionally, a initial capacity can be provided, if it is known that right after initialization, a lot of new items will
        //be aded to the storage array
        public ArrayList(int initialCapacity)
        {
            this.items = new T[Math.Max(initialCapacity, 8)];
        }

        /// <summary>
        /// Adds a new item to the array list, resizing the storage array as needed. Afterwards the index of the added item is returned
        /// </summary>
        public int Add(T item)
        {
            //The add operation has a time complexity of O(n), as sometimes the array has to be copied. But for this example the amortized
            //time complexity (i.e. the average time complexity over many operations) is O(1), as the array is rarely copied for large n's
            this.currentIndex++;
            this.items[this.currentIndex] = item;

            //if the array is halfway full, it will be resized to twice its length to ensure that there's enough room for more items
            //to be added
            if (this.currentIndex > this.items.Length / 2)
            {
                this.CopyToNewArray(this.items.Length * 2);
            }

            return this.currentIndex;
        }

        /// <summary>
        /// Removes the item at the provided index if it exists
        /// </summary>
        public void RemoveAt(int index)
        {
            //The remove operation has a time complexity of O(n), as, in the worst case, every item in the array has
            //to be shifted one cell to the left. The amortized time complexity therefor also is O(n), even though
            //copying the array also rarely occurs for large n's
            //As just setting the default value to the removed item would leave gaps and replacing the removed item
            //with the last item in the list would shuffle the list over time, every item after the removed index
            //has to be shifted one cell to the left. The last index of the array has to be set to the default value
            //of type T
            for (int i = index; index < this.items.Length - 1; index++)
            {
                this.items[i] = this.items[i + 1];
            }
            this.items[this.items.Length - 1] = default;
            this.currentIndex--;

            //In this implementation the array will be shrunk if the array is less than 1/8th filled after removing an item
            if (this.items.Length > 0.125 * this.currentIndex)
            {
                this.CopyToNewArray(this.items.Length / 2);
            }
        }

        /// <summary>
        /// Returns the index of the first occurence of the provided item is stored at
        /// </summary>
        public int IndexOf(T item)
        {
            //The IndexOf operation does the same job, as the Search operation of a LinkedList, but instead of returning
            //a LinkedListITem, it returns the index an item is stored at. Therefor both have the same time complexity
            //of O(n)

            //A return value of -1 signifies that the item isn't present in the list
            if (this.IsEmpty)
            {
                return -1;
            }

            //this particular search algorithm terminates after the first ocurrence of the searched item is found
            int itemIndex = -1;
            int index = 0;
            while (index <= this.currentIndex && itemIndex == -1)
            {
                if (item == this.items[index])
                {
                    itemIndex = index;
                }
                itemIndex++;
            }

            return itemIndex;
        }

        /// <summary>
        /// Returns the item that is stored at the provided index
        /// </summary>
        public T Get(int index)
        {
            //Note: in C# you would idiomatically use the indexer, but this implementation uses a get method
            //      to keep it more generalized

            //The get operation has a time complexity of O(1), as the memory address of the storage cell can
            //be directly calculated and accessed
            return this.items[index];
        }

        /// <summary>
        /// Returns whether this array list is empty
        /// </summary>
        public bool IsEmpty
        {
            //if currentIndex is -1, no cell of the array has been written to yet
            get => this.currentIndex == -1;
        }

        /// <summary>
        /// Returns the amount of items currently stored in the array list
        /// </summary>
        public int Count
        {
            //As arrays use 0-based indexing in C#, you must add one to the current storage index to convert it to an item count
            get => this.currentIndex + 1;
        }

        /// <summary>
        /// This method is responsible for copying the current storage array to a new one with a different size
        /// </summary>
        private void CopyToNewArray(int newLength)
        {
            //This operation has a time complexity of O(n), but it is executed so rarely for large n's, that it
            //doesn't affect the amortized time complexity of the add and remove operations

            //To prevent unnecessary copy operations, the minimum array length is set to 8 and copying only
            //occurs if the new array length is different from the current one
            newLength = Math.Max(newLength, 8);
            if (newLength != this.items.Length)
            {
                //Note: C# also provides the method Array.Resize(ref T[] array, int newSize) that achieves the same goal
                //      but a) it sometimes has to copy the array content to a new array if there isn't enough continous
                //             free memory after the array
                //      and b) it's a C# specific method that may not be available in other languages (keep it generalized)
                T[] copy = new T[newLength];
                for (int i = 0; i < Math.Min(newLength, this.items.Length); i++)
                {
                    copy[i] = this.items[i];
                }
                this.items = copy;
            }
        }
    }
}