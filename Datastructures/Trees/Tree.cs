namespace LunarDoggo.Datastructures.Trees
{
    //Trees are a special kind of graph: they're connected, undirected, acyclic and unweighted. In general trees konsist of
    //Individual vertices (thereafter "TreeItems") that may have an arbitrary amount of children
    //The traversal of trees is similar to that of doubly linked lists with the difference that each item may have an arbitrary
    //amount of successors instead of a maximum of one.
    
    //Note: one could also declare a class called Tree<T>, but it would only contain a reference to the root and maybe
    //      some operations that aren't considered standard for trees, so instead one would store a reference to the root
    //      TreeItem of the tree in the code using the tree

    //A TreeItem is a single vertex in the tree. It contains a value and stores references to an arbitrary amount of child
    //TreeItems. To ensure that there cannot be a cycle in the tree, the AddChild method takes an instance of type T instead
    //of a TreeItem instance
    public class TreeItem<T>
    {
        //Using a HashSet instead of a List is advantageous for this use case, as it usually doesn't take O(n) time to find
        //a single item. As every child is an independent object, it's unlikely that two different children come into
        //conflict (a set is characterized by only containing unique objects). Add and Remove operations on HashSets have
        //a time complexity of O(n), though most operations will take constant time, if the hashing algorithm used is
        //sufficiently evenly distributed (this should be the case with the implementation of the standard library)
        private readonly HashSet<TreeItem<T>> children = new HashSet<TreeItem<T>>();

        public TreeItem(T value) : this(value, null)
        { }

        //This constructor is declared as private to prevent users from creating cycles
        private TreeItem(T value, TreeItem<T> parent)
        {
            this.Parent = parent;
            this.Value = value;
        }

        /// <summary>
        /// Adds a new TreeItem containing the provided value to the children of this TreeItem
        /// </summary>
        public void AddChild(T value)
        {
            //The AddChild operation could be implemented in O(1) time using a List, but the amortized time complexity
            //using .NET's HashSet should also be O(1)
            //again: to prevent cycles in the tree from happening, the user isn't allowed to pass
            //TreeItem instances directly
            this.children.Add(new TreeItem<T>(value));
        }

        /// <summary>
        /// Removes a child from the tree if it is present
        /// </summary>
        public void RemoveChild(TreeItem<T> child)
        {
            //The RemoveChild operation has a time complexity of O(n) regardless of if a List or HashSet is used, though
            //using .NET's HashSet it's likely to have a amortized time complexity of O(1)
            this.children.Remove(child);
        }

        /// <summary>
        /// Deletes this TreeItem from the tree
        /// </summary>
        public void Delete()
        {
            if(this.Parent != null)
            {
                this.Parent.RemoveChild(this);
            }
        }

        /// <summary>
        /// Returns a collection of all children of this TreeItem
        /// </summary>
        public IEnumerable<TreeItem<T>> Children { get => this.children.AsEnumerable(); }
        /// <summary>
        /// Returns the Value stored in this TreeItem
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// Returns the parent TreeItem of this TreeItem
        /// </summary>
        public TreeItem<T> Parent { get; private set; }
    }
}