using System.Collections.Immutable;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace LunarDoggo.FileSystemTree
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseDirectoryPath = Program.GetBaseDirectoryPath();
            DirectoryInfo baseDirectory = new DirectoryInfo(baseDirectoryPath);

            FileSystemTreeItem fileSystemTree = Program.GetFileSystemTree(baseDirectory);

            Program.OutputFileSystemTreeLevel(0, fileSystemTree);
        }

        private static void OutputFileSystemTreeLevel(int indentationLevel, FileSystemTreeItem item)
        {
            //for each indentationlevel we add two spaces
            string indentation = new string(Enumerable.Repeat(' ', indentationLevel * 2).ToArray());

            //combine the indentation with the current tree items name and type
            Console.WriteLine(indentation + item.Name + " (" + item.Type + ")");

            //if the current tree item has any children, recursively print them and
            //their children to the console with the corresponding indentatino level
            if (item.Children != null && item.Children.Count() > 0)
            {
                foreach (FileSystemTreeItem child in item.Children)
                {
                    Program.OutputFileSystemTreeLevel(indentationLevel + 1, child);
                }
            }
        }

        /// <summary>
        /// Recursive Method to get the file system structure as a tree
        /// </summary>
        private static FileSystemTreeItem GetFileSystemTree(DirectoryInfo baseDirectory)
        {
            //Read all subdirectories and files from the current baseDirectory
            DirectoryInfo[] subdirectories = baseDirectory.GetDirectories();
            FileInfo[] files = baseDirectory.GetFiles();

            List<FileSystemTreeItem> children = new List<FileSystemTreeItem>();

            //First recursively add all subdirectories with its children to the current tree item
            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                //add all tree items from 
                children.Add(Program.GetFileSystemTree(subdirectory));
            }

            //Lastly add all files of the current tree item
            foreach (FileInfo file in files)
            {
                children.Add(new FileSystemTreeItem(file.Name, FileSystemTreeItemType.File, null));
            }

            return new FileSystemTreeItem(baseDirectory.Name, FileSystemTreeItemType.Directory, children.ToImmutableArray());
        }

        /// <summary>
        /// Get the base directory path from user input
        /// </summary>
        private static string GetBaseDirectoryPath()
        {
            string path;
            do
            {
                Console.Clear(); //Clear the console window
                Console.Write("Please enter a valid directory path: ");
                path = Console.ReadLine();

                //While the user input is not a valid path and therefor doesn't exist, continue to prompt for a valid directory path
            } while (!Directory.Exists(path));
            return path;
        }
    }
}
