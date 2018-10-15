using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var time = DateTime.Now;
            var ed1 = new Edition
            {
                PublicationDate = time
            };
            var ed2 = new Edition
            {
                PublicationDate = time
            };
            Console.WriteLine($"Equals: {ed1.Equals(ed2)}");
            Console.WriteLine($"Reference: {ReferenceEquals(ed1, ed2)}");
            Console.WriteLine($"HashCode: {ed1.GetHashCode() == ed2.GetHashCode()}");

            try
            {
                ed1.Circulation = -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var mag = new Magazine();

            var articles = new Article[]
            {
                new Article
                {
                    Name = "1"
                },
                new Article
                {
                    Rating = 2
                }
            };

            var editors = new Person[]
            {
                new Person(),
                new Person()
            };

            mag.AddArticles(articles);
            mag.AddEditors(editors);

            Console.WriteLine(mag);

            Console.WriteLine(mag.Edition.ToString());

            var copy = mag.DeepCopy() as Magazine;

            mag.Name = "test";
            copy.Name = "copy";

            Console.WriteLine($"Original:{mag.Name}\nCopy:{copy.Name}");


            Console.WriteLine("String enumerator: ");
            foreach(var a in mag.GetArticlesByName("1"))
            {
                Console.WriteLine(a);
            }


            Console.WriteLine("Double enumerator: ");
            foreach (var a in mag.GetRatings(1))
            {
                Console.WriteLine(a);
            }

            Console.ReadKey();
        }
    }
}
