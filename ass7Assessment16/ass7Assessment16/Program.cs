using System;
using System.IO;
using System.Linq;

public class FileAnalyzer
{
    // Function to get all files in a directory
    public static string[] GetFilesInDirectory(string directory)
    {
        return Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
    }

    // Function to count the number of text files (*.txt)
    public static int CountTextFiles(string directory)
    {
        string[] files = GetFilesInDirectory(directory);
        int textFileCount = files.Count(file => Path.GetExtension(file) == ".txt");
        return textFileCount;
    }

    // Function to count the number of files per extension type
    public static int[] CountFilesPerExtension(string directory)
    {
        string[] files = GetFilesInDirectory(directory);
        var fileCounts = files.GroupBy(file => Path.GetExtension(file))
                              .Select(group => new { Extension = group.Key, Count = group.Count() })
                              .ToArray();
        int[] counts = new int[fileCounts.Length];
        for (int i = 0; i < fileCounts.Length; i++)
        {
            counts[i] = fileCounts[i].Count;
        }
        return counts;
    }

    // Function to get the top 5 largest files
    public static (string, long)[] GetTop5LargestFiles(string directory)
    {
        string[] files = GetFilesInDirectory(directory);
        var fileSizes = files.Select(file => new { Path = file, Size = new FileInfo(file).Length })
                             .OrderByDescending(file => file.Size)
                             .Take(5)
                             .Select(file => (file.Path, file.Size))
                             .ToArray();
        return fileSizes;
    }

    // Function to get the file with maximum length
    public static string GetFileWithMaxLength(string directory)
    {
        string[] files = GetFilesInDirectory(directory);
        string maxFile = "";
        long maxLength = 0;
        foreach (string file in files)
        {
            long length = new FileInfo(file).Length;
            if (length > maxLength)
            {
                maxLength = length;
                maxFile = file;
            }
        }
        return maxFile;
    }

    // Example usage
    public static void Main(string[] args)
    {
        string directoryPath = "C:\\Users\\bhekanimasinga\\3D Objects";
        int numTextFiles = CountTextFiles(directoryPath);
        int[] fileCountPerExtension = CountFilesPerExtension(directoryPath);
        var top5LargestFiles = GetTop5LargestFiles(directoryPath);
        string fileWithMaxLength = GetFileWithMaxLength(directoryPath);

        Console.WriteLine("Number of text files (*.txt): " + numTextFiles);
        Console.WriteLine("Number of files per extension type: ");
        string[] extensions = Directory.GetDirectories(directoryPath);
        for (int i = 0; i < extensions.Length; i++)
        {
            Console.WriteLine(extensions[i] + ": " + fileCountPerExtension[i]);
        }
        Console.WriteLine("Top 5 largest files: ");
        foreach (var file in top5LargestFiles)
        {
            Console.WriteLine($"File: {file.Item1}, Size: {file.Item2}");
        }
        Console.WriteLine("File with maximum length: " + fileWithMaxLength);
    }
}
