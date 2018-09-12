using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var magazine = new Magazine();
            Console.WriteLine(magazine.ToShortString());
            Console.WriteLine($"Weekly:{magazine[Frequency.Weekly]}");
            Console.WriteLine($"Monthly:{magazine[Frequency.Monthly]}");
            Console.WriteLine($"Yearly:{magazine[Frequency.Yearly]}");
            var articles = new[]
            {
                new Article(),
                new Article()
            };
            magazine.Articles = articles;
            magazine.Name = "qwe";
            magazine.PublicationDate = DateTime.Now;
            magazine.Frequency = Frequency.Weekly;
            Console.WriteLine(magazine);
            magazine.AddArticles(new Article(), new Article());
            Console.WriteLine(magazine);
            int rows, columns;
            var input = Console.ReadLine().Split();
            rows = int.Parse(input[0]);
            columns = int.Parse(input[1]);
            var one = new Article[rows * columns];
            var two = new Article[rows, columns];
            var jagged = new Article[rows][];
            for (var i = 0; i < rows; i++)
            {
                jagged[i] = new Article[columns];
                for (var j = 0; j < columns; j++)
                {
                    one[i * rows + j] = new Article();
                    two[i, j] = new Article();
                    jagged[i][j] = new Article();
                }
            }
            var start = Environment.TickCount;

            for (var i = 0; i < rows*columns; i++)
            {
                one[i].Name = "qwe";
            }
            var ticks = Environment.TickCount - start;

            Console.WriteLine($"1d:{ticks}");
            start = Environment.TickCount;

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    two[i, j].Name = "qwe";
                }
            }

            ticks = Environment.TickCount - start;
            Console.WriteLine($"2d:{ticks}");

            start = Environment.TickCount;

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    jagged[i][j].Name = "qwe";
                }
            }

            ticks = Environment.TickCount - start;
            Console.WriteLine($"jagged:{ticks}");
            Console.ReadKey();
        }
    }
}
