using Day7.Models;
using System.Data;

namespace Day7
{
    internal class Program
    {
        static long sum = 0L;
        static long totalSum = 0L;
        static long spaceToBeDeleted = 0L;
        static List<long> bigEnoughDirs = new();

        static void Main(string[] args)
        {
            var rootDir = new Node(null, "");
            var currentDir = new Node(null, "");

            try
            {
                using var fs = new FileStream(Path.GetFullPath("../../../Input/FileSystem.txt"), FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);

                var line = sr.ReadLine();

                while (line != null)
                {
                    var fileSize = 0;
                    var commands = line.Split(' ');

                    if (commands[0] == "$")
                    {
                        if (commands[1] == "cd")
                        {
                            switch (commands[2])
                            {
                                case "/":
                                    rootDir = new Node(null, "/");
                                    currentDir = rootDir;
                                    break;
                                case "..":
                                    currentDir = currentDir.Parent;
                                    break;
                                default:
                                    currentDir = currentDir.DirContent.FirstOrDefault(x => x.Name == commands[2]);
                                    break;
                            }
                        }
                        else if (commands[1] == "ls")
                        {
                            currentDir.DirContent = new List<Node>();
                        }
                    }
                    else if (int.TryParse(commands[0], out fileSize))
                    {
                        var file = new Node(currentDir, commands[1]) { Size = fileSize };
                        currentDir.DirContent.Add(file);
                    }
                    else if (commands[0] == "dir")
                    {
                        currentDir.DirContent.Add(new Node(currentDir, commands[1]) { IsDirectory = true });
                    }

                    line = sr.ReadLine();
                }

                _ = TraverseDirs(rootDir);
                _ = GetTotalUsedSpace(rootDir);
                long unused = 70000000L - totalSum;
                spaceToBeDeleted = 30000000L - unused;
                _ = FindBigEnoughDirs(rootDir);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"Total size of dirs with size <= 100000: {sum}");
                Console.WriteLine($"Dir with size {bigEnoughDirs.OrderByDescending(x => x).ToList().Last()} can get deleted");
                Console.ReadLine();
            }
        }

        static long TraverseDirs(Node node)
        {
            long size = 0L;

            foreach (var d in node.DirContent)
            {
                if (d.IsDirectory)
                {
                    long totalSize = TraverseDirs(d);

                    if (totalSize <= 100000L)
                    {
                        sum += totalSize;
                    }

                    size += totalSize;
                }
                else
                {
                    size += d.Size;
                }
            }

            return size;
        }

        static long FindBigEnoughDirs(Node node)
        {
            long size = 0L;

            foreach (var d in node.DirContent)
            {
                if (d.IsDirectory)
                {
                    long totalSize = FindBigEnoughDirs(d);

                    if (totalSize >= spaceToBeDeleted)
                    {
                        bigEnoughDirs.Add(totalSize);
                    }

                    size += totalSize;
                }
                else
                {
                    size += d.Size;
                }
            }

            return size;
        }

        static long GetTotalUsedSpace(Node node)
        {
            long size = 0L;

            foreach (var d in node.DirContent)
            {
                if (d.IsDirectory)
                {
                    long totalSize = TraverseDirs(d);
                    totalSum += totalSize;
                    size += totalSize;
                }
                else
                {
                    size += d.Size;
                }
            }

            return size;
        }
    }
}
