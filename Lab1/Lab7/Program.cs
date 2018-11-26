using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab7
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Enter directory to calculate size: ");
            var directory = Console.ReadLine();
            var size = await GetDirectorySize(directory);
            Console.WriteLine($"Total size: {size} bytes");
            Console.ReadKey();
        }

        private static async Task<long> GetDirectorySize(string path)
        {
            try
            {
                Console.WriteLine($"Reading directory: {path}");
                var files = Directory.GetFiles(path);
                var fileSize = files.Sum(f => new FileInfo(f).Length);
                var directories = Directory.GetDirectories(path);
                var results = await Task.WhenAll(directories.Select(GetDirectorySize));
                var size = results.Sum() + fileSize;
                return size;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }
    }
}