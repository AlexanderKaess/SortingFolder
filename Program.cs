using System;
using System.IO;
using System.Text;

namespace SortingFolder
{
    class Program
    {
        static void Main(string[] args)
        {

            ResizeMyConsole(150, 40);
            Console.WriteLine("Hello World, Kaess is the best!");

            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.ExpandEnvironmentVariables("%USERPROFILE%"));
            sb.Append(@"\Downloads");
            Console.WriteLine("Folder: " + sb.ToString() + "\n");

            ShowFolderContent(sb.ToString());
            ShowExtensions(sb.ToString());
            CreateFoldersFromExtensions(sb.ToString());
            MoveItemToFolder(sb.ToString());

            Console.ReadKey();
        }

        static void ResizeMyConsole(int windowWidth, int windowHeight)
        {
            Console.SetWindowSize(windowWidth, windowHeight);
        }

        static void ShowFolderContent(string originPath)
        {
            string[] itemsInFolder = Directory.GetFileSystemEntries(originPath);

            foreach (string item in itemsInFolder)
            {
                Console.WriteLine("Item name: " + Path.GetFileName(item));
            }
            Console.WriteLine("\n");
        }

        static void ShowExtensions(string originPath)
        {
            string[] itemsInFolder = Directory.GetFileSystemEntries(originPath);
            Console.WriteLine("Extensions in Folder: ");

            foreach (string item in itemsInFolder)
            {
                if (Path.GetExtension(item) == string.Empty)
                {
                    Console.Write(" FOLDER");
                }
                else
                {
                    Console.Write(" " + Path.GetExtension(item));
                }
            }
            Console.WriteLine("\n");
        }

        static void CreateFoldersFromExtensions(string originPath)
        {
            string[] itemsInFolder = Directory.GetFileSystemEntries(originPath);
            Console.WriteLine("Create folders ...");

            foreach (string item in itemsInFolder)
            {
                if (Path.GetExtension(item) == string.Empty)
                {
                    Console.WriteLine("It's a FOLDER, do nothing...");
                }
                else
                {
                    string itemName = Directory.GetParent(item) + @"\" + Path.GetExtension(item).Trim('.');

                    if (Directory.Exists(itemName) == false)
                    {
                        Directory.CreateDirectory(itemName);
                        Console.WriteLine("Folder created: " + Path.GetExtension(item).Trim('.'));
                    }
                }
            }
            Console.WriteLine("\n");
        }

        static void MoveItemToFolder(string originPath)
        {
            string[] itemsInFolder = Directory.GetFileSystemEntries(originPath);
            Console.WriteLine("Move items to folder ...");

            try
            {
                foreach (string item in itemsInFolder)
                {
                    if (Path.GetExtension(item) == string.Empty)
                    {
                        Console.WriteLine("It's a FOLDER, do nothing...");
                    }
                    else
                    {
                        string itemTargetPath = Directory.GetParent(item) + @"\" + Path.GetExtension(item).Trim('.');

                        if (Directory.Exists(itemTargetPath))
                        {
                            itemTargetPath += @"\" + Path.GetFileName(item);
                            File.Move(item, itemTargetPath, true);
                            File.Delete(item);
                            Console.WriteLine($"moved and deleted - {Path.GetFileName(item)}");
                        }
                    }
                }
                Console.WriteLine("\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File could not be found - {ex.ToString()}");
            }

        }

    } 
}
