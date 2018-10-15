using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var time = DateTime.Now;
            var c1 = new MagazineCollection
            {
                Name = "Test1"
            };
            var c2 = new MagazineCollection
            {
                Name = "Test2"
            };
            var l1 = new Listener();
            var l2 = new Listener();
            c1.AddDefaults();
            c2.AddDefaults();

            c1.MagazineAdded += l1.Report;
            c1.MagazineReplaced += l1.Report;
            c2.MagazineAdded += l2.Report;
            c2.MagazineReplaced += l2.Report;

            c1.AddMagazines(new Magazine());
            c1[3] = new Magazine();
            c1.Replace(4, new Magazine());
            c1.AddMagazines(new Magazine());

            c2[1] = new Magazine();
            c2.Replace(0, new Magazine());
            c2.AddMagazines(new Magazine());
            c2[2] = new Magazine();

            Console.WriteLine(l1);
            Console.WriteLine();
            Console.WriteLine(l2);

            Console.ReadKey();
        }
    }
}
