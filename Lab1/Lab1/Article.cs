using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public class Article
    {
        public Person Author { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public Article(Person author, string name, double rating)
        {
            Author = author;
            Name = name;
            Rating = rating;
        }

        public Article()
        {
            Author = new Person();
            Name = "";
            Rating = 0;
        }

        public override string ToString() => $"Author:{Author}{Environment.NewLine}Name:{Name}"+
            $"{Environment.NewLine}Rating:{Rating}";
    }
}
