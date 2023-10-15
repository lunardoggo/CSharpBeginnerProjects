using LunarDoggo.Datastructures.Exceptions;

namespace LunarDoggo.Datastructures.Collections
{
    //A queue is a so called FIFO (first in, first out) data structure that works exactly as the name suggests:
    //items are added to queue and have to wait in line to be served. Items that are enqueued the earliest will
    //be removed the earliest. "headIndex" points to the element in the array that should be removed from the queue
    //on the next dequeue-call and "tailIndex" points to the index of the cell that should be filled by the next
    //enqueue-call. If "headIndex" and "tailIndex" are the same, the queue is considered empty.
    //The most primitive implementation uses an array that's not resized over the queue's lifetime. If the queue
    //runs full and another object is enqueued, an exception is thrown. Additionally, as the oldest items are
    //dequeued first, one must keep track of the current head and tail of the queue, as it will no longer be 
    //guaranteed that the item at index 0 is the next item to be dequeued
    public class Queue<T>
    {
        private readonly T[] cache;
        private int headIndex = 0;
        private int tailIndex = 0;

        public Queue(int capacity)
        {
            //The initialization of the queue has a time complexity of O(capacity), as C# not only allocates a certain memory
            //range, but also sets all cells of the array to the default value of type T

            //As the array will never be resized or reallocated, the capacity of the queue has to be provided on initialization
            this.cache = new T[capacity];
        }

        /// <summary>
        /// Adds a new item to the queue in last place
        /// </summary>
        /// <exception cref="OverflowException"></exception>
        public void Enqueue(T item)
        {
            //Enqueue has a time complexity of O(1), as only constant time operations are used sequentially

            //head points to the oldest element and tail to the next place to be used; if head and tail have the same
            //value after this operation, it cannot be distinguished between a full and an empty queue, therefore an
            //Exception must be thrown before adding the element to avoid this situation
            if ((this.tailIndex + 1) % this.cache.Length == this.headIndex)
            {
                throw new OverflowException("The queue is full");
            }

            this.cache[tailIndex] = item;
            //As head and tail can wrap around the end of the array, it must be ensured that the tail index never goes out of bounds
            this.tailIndex = (this.tailIndex + 1) % this.cache.Length;
        }

        /// <summary>
        /// Removes the first item of the queue and returns its value
        /// </summary>
        /// <exception cref="UnderflowException"></exception>
        public T Dequeue()
        {
            //Dequeue has a time complexity of O(1), as only constant time operations are used sequentially
            if (this.IsEmpty)
            {
                throw new UnderflowException("The queue is empty");
            }

            T item = this.cache[this.headIndex];
            //As head and tail can wrap around the end of the array, it must be ensured that the head index never goes out of bounds
            this.headIndex = (this.headIndex + 1) % this.cache.Length;
            return item;
        }

        /// <summary>
        /// Returns the value of the first item in the queue
        /// </summary>
        public T Peek()
        {
            //Peek has a time complexity of O(1), as only constant time operations are used sequentially

            if (this.IsEmpty)
            {
                return default;
            }
            return this.cache[this.headIndex];
        }

        /// <summary>
        /// Returns whether the queue is empty
        /// </summary>
        public bool IsEmpty
        {
            //IsEmpty has a time complexity of O(1), as only constant time operations are used sequentially
            get => this.headIndex == this.tailIndex;
        }
    }
}
