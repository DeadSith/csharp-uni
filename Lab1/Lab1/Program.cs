using System;
using System.IO;

namespace Lab1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var magazine = new Magazine
            {
                Name = "test"
            };

            var copy = magazine.DeepCopy();
            copy.Name = "Copy";

            Console.WriteLine($"Original:{Environment.NewLine}{magazine}"
                + $"{Environment.NewLine}Copy:{Environment.NewLine}{copy}");

            Console.Write("Enter file name: ");
            var filename = Console.ReadLine();

            var directory = "WrongPath";

            try
            {
                directory = Path.GetDirectoryName(filename);
            }
            catch
            {
                Console.WriteLine("Input is not correct path");
            }

            while (directory == "WrongPath" || !Directory.Exists(directory))
            {
                Console.WriteLine("Directory does not exist");
                Console.Write("Enter file name: ");
                filename = Console.ReadLine();
                try
                {
                    directory = Path.GetDirectoryName(filename);
                }
                catch
                {
                    Console.WriteLine("Input is not correct path");
                }
            }

            if (File.Exists(filename))
            {
                magazine.Load(filename);
            }
            else
            {
                Console.WriteLine("New file will be created");
            }

            Console.WriteLine(magazine);

            magazine.AddFromConsole();

            magazine.Save(filename);

            Magazine.Load(filename, magazine);
            magazine.AddFromConsole();
            Magazine.Save(filename, magazine);
            Console.WriteLine(magazine);

            Console.ReadKey();
        }
    }
}