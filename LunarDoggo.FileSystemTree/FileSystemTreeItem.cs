using System.Collections.Generic;

namespace LunarDoggo.FileSystemTree
{
    public class FileSystemTreeItem
    {
        private readonly IEnumerable<FileSystemTreeItem> children;
        private readonly FileSystemTreeItemType type;
        private readonly string name;

        public FileSystemTreeItem(string name, FileSystemTreeItemType type, IEnumerable<FileSystemTreeItem> children)
        {
            this.children = children;
            this.name = name;
            this.type = type;
        }

        public IEnumerable<FileSystemTreeItem> Children { get { return this.children; } }
        public FileSystemTreeItemType Type { get { return this.type; } }
        public string Name { get { return this.name; } }
    }

    public enum FileSystemTreeItemType
    {
        Directory,
        File
    }
}
