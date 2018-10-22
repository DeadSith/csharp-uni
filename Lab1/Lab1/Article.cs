using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    [Serializable]
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

        public override bool Equals(object obj)
        {
            var article = obj as Article;
            if (ReferenceEquals(article, null))
            {
                return false;
            }
            return Author.Equals(article.Author) &&
                   Name == article.Name &&
                   Rating == article.Rating;
        }

        public override int GetHashCode() => HashCode.Combine(Author, Name, Rating);

        public static bool operator ==(Article article1, Article article2)
        {
            return EqualityComparer<Article>.Default.Equals(article1, article2);
        }

        public static bool operator !=(Article article1, Article article2)
        {
            return !(article1 == article2);
        }

        public virtual object DeepCopy()
        {
            var res = new Article(Author.DeepCopy() as Person, Name, Rating);
            return res;
        }
    }
}
