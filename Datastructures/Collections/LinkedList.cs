namespace LunarDoggo.Datastructures.Collections
{
    //A doubly linked list ist a data structure that is composed of single items that have a value and a reference to
    //the previous item and the next item in the list. The list itself only stores a reference to the the current head
    //(i.e. the first item) of the list.
    //If an item has to be inserted, a new item is created, linked and set as the new head. If one would want to insert
    //items at the end of the list, one would have to keep a reference to the tail of the list and add the new item after
    //that.
    //In practice array-based lists are preferred over linked lists in most cases, as following so many references is less
    //efficient than reading values from an array.
    //Linked lists don't need a delete operation, as the links only consist of references between items, if one would want
    //to remove an item from the list, one must only reassign the the next reference of the item's previous item and the
    //previous reference of the item's next item in O(1) time
    public class LinkedList<T> where T : class //T must be a class because otherwise the "!=" operator isn't defined for generic T's
    {
        //The only thing to be done on initialization is to assign a new field of type LinkedListItem<T> that represents the
        //head and is initialized with null
        private LinkedListItem<T> head;

        /// <summary>
        /// Searches for the first item in the linked list that contains the provided <paramref name="value"/> and returns the
        /// list item if it exists
        /// </summary>
        public LinkedListItem<T> Search(T value)
        {
            //The search operation has a time complexity of O(n) where n is the number of items in the list, as in the worst case
            //every item in the list has to be checked if it contains the searched value

            //If head is null, the loop won't run and null is returned. If the value isn't contained in the list, Search also returns
            //null after checking every single item. Otherwise there must be an item containing the searched value, that's found after
            //at most O(n) checks
            LinkedListItem<T> current = head;
            while (current != null && current.Value != value)
            {
                current = current.Next;
            }
            return current;
        }

        /// <summary>
        /// Inserts a new list item at the beginning of the linked list
        /// </summary>
        public LinkedListItem<T> Insert(T value)
        {
            //New items are inserted at the front of the list, therefor the previous reference of this.head has to be updated and
            //the new item has to be set as the new head
            LinkedListItem<T> item = new LinkedListItem<T>(value);
            item.Next = this.head;
            if (this.head != null)
            {
                this.head.Previous = item;
            }
            this.head = item;
            return item;
        }
    }

    public class LinkedListItem<T>
    {
        public LinkedListItem(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the reference to the item that's preceeding the current item in the linked list
        /// </summary>
        public LinkedListItem<T> Previous { get; set; }
        /// <summary>
        /// Gets or sets the reference to item that's following the current item in the linked list
        /// </summary>
        public LinkedListItem<T> Next { get; set; }
        /// <summary>
        /// Returns the value contained in the current list item
        /// </summary>
        public T Value { get; }
    }
}
