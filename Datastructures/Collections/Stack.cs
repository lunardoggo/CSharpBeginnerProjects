using LunarDoggo.Datastructures.Exceptions;

namespace LunarDoggo.Datastructures.Collections
{
    //A stack is a so called LIFO (last in, first out) data structure that can be imagined as a pile of objects
    //stacked on one and another. If you want to put something on the pile you put it on the top, and if you
    //want to retrieve an object that's in the middle of the stack you have to remove all the objects that are
    //on top of the object to be retrieved.
    //The most primitive implementation uses an array that's not resized over the stack's lifetime. If the stack
    //runs full and another object is added, an exception is thrown signifying that no more items can be pushed
    //onto the stack
    public class Stack<T>
    {
        //-1 means that there are no items in the cache yet
        private int currentIndex = -1;
        private readonly T[] cache;

        public Stack(int capacity)
        {
            //The initialization of the stack has a time complexity of O(capacity), as C# not only allocates a certain memory
            //range, but also sets all cells of the array to the default value of type T

            //As the array will never be resized or reallocated, the capacity of the stack has to be provided on initialization
            this.cache = new T[capacity];
        }

        /// <summary>
        /// Pushes another item of type <see cref="T"/> onto the stack
        /// </summary>
        /// <exception cref="OverflowException"></exception>
        public void Push(T item)
        {
            //Push has a time complexity of O(1), as only constant time operations are used sequentially
            if (currentIndex >= this.cache.Length - 1)
            {
                throw new OverflowException("The stack is already full");
            }

            this.currentIndex++;
            this.cache[this.currentIndex] = item;
        }

        /// <summary>
        /// Removes the item on top of the stack from the stack and returns the previously contained value
        /// </summary>
        /// <exception cref="UnderflowException"></exception>
        public T Pop()
        {
            //Pop has a time complexity of O(1), as only constant time operations are used sequentially
            T output = this.Peek();
            //usually the cells of removed values aren't reset to the default value of T, but this could
            //result in security issues, as sensitive deleted data isn't garbage collected as soon as possible
            this.cache[this.currentIndex] = default;
            return output;
        }

        /// <summary>
        /// Returns the item that's on top of the stack
        /// </summary>
        /// <exception cref="UnderflowException"></exception>
        public T Peek()
        {
            //Peek has a time complexity of O(1), as only constant time operations are used sequentially
            if (this.IsEmpty)
            {
                throw new UnderflowException("The stack is empty");
            }

            return this.cache[this.currentIndex];
        }

        /// <summary>
        /// Returns whether the stack is empty
        /// </summary>
        public bool IsEmpty
        {
            //IsEmpty has a time complexity of O(1), as only constant time operations are used sequentially
            get => this.currentIndex < 0;
        }
    }
}
