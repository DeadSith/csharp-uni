using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Lab1
{
    [Serializable]
    public class Magazine : Edition, IRateAndCopy<Magazine>
    {
        public Magazine(string name, Frequency frequency, DateTime publicationDate,
            int circulation, List<Article> articles) : base(name, publicationDate, circulation)
        {
            Articles = articles;
        }

        public Magazine() : base()
        {
            Articles = new List<Article>();
        }

        public Frequency Frequency { get; set; }

        public List<Article> Articles { get; set; }

        public List<Person> Editors { get; set; }

        public double MediumRating
        {
            get
            {
                if (Articles.Count == 0)
                    return 0;
                var sum = .0;
                foreach (var article in Articles)
                {
                    sum += article.Rating;
                }
                return sum / Articles.Count;
            }
        }

        public double Rating => MediumRating;

        public bool this[Frequency frequency] => Frequency == frequency;

        public void AddArticles(params Article[] articles) => Articles.AddRange(articles);

        public Edition Edition
        {
            get => this;
            set
            {
                Name = value.Name;
                Circulation = value.Circulation;
                PublicationDate = value.PublicationDate;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var article in Articles)
            {
                sb.Append($"Article:{article}{Environment.NewLine}{Environment.NewLine}");
            }

            return $"Name:{_name}{Environment.NewLine}Frequency:{Frequency}{Environment.NewLine}"
                + $"Publication date: {_publicationDate}{Environment.NewLine}{Environment.NewLine}"
                + $"Circulation:{_circulation}{Environment.NewLine}Articles:{Environment.NewLine}{sb}";
        }

        public virtual string ToShortString() => $"Name:{_name}{Environment.NewLine}"
            + $"Frequency:{Frequency}{Environment.NewLine}"
            + $"Publication date: {_publicationDate}{Environment.NewLine}"
            + $"Circulation:{_circulation}{Environment.NewLine}Rating:{MediumRating}";

        public override bool Equals(object obj)
        {
            var magazine = obj as Magazine;
            if (ReferenceEquals(magazine, null))
            {
                return false;
            }
            return Name == magazine.Name &&
                   Frequency == magazine.Frequency &&
                   Circulation == magazine.Circulation &&
                   PublicationDate == magazine.PublicationDate;
        }

        public Magazine DeepCopy()
        {
            Magazine result;
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                result = formatter.Deserialize(stream) as Magazine;
            }
            return result;
        }

        public void AddEditors(params Person[] editors)
        {
            if (Editors == null)
            {
                Editors = new List<Person>(editors);
            }
            else
            {
                Editors.AddRange(editors);
            }
        }

        public IEnumerable<Article> GetRatings(double minRating)
        {
            foreach (var o in Articles)
            {
                var article = o as Article;
                if (article == null || article.Rating < minRating)
                {
                    continue;
                }
                yield return article;
            }
        }

        public IEnumerable<Article> GetArticlesByName(string name)
        {
            foreach (var o in Articles)
            {
                var article = o as Article;
                if (article == null || !article.Name.Contains(name))
                {
                    continue;
                }
                yield return article;
            }
        }

        public override int GetHashCode() => HashCode.Combine(Frequency, Name, PublicationDate, Circulation);

        public static bool operator ==(Magazine magazine1, Magazine magazine2)
        {
            return EqualityComparer<Magazine>.Default.Equals(magazine1, magazine2);
        }

        public static bool operator !=(Magazine magazine1, Magazine magazine2)
        {
            return !(magazine1 == magazine2);
        }

        public bool Save(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Create))
            {
                try
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool Load(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                try
                {
                    var formatter = new BinaryFormatter();
                    var result = formatter.Deserialize(stream) as Magazine;
                    if (result == null)
                    {
                        return false;
                    }
                    Edition = result.Edition;
                    Editors = result.Editors;
                    Frequency = result.Frequency;
                    Articles = result.Articles;
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool AddFromConsole()
        {
            try
            {
                Console.WriteLine($"String format:{Environment.NewLine}" +
                    "<article_name>;<author_name> <author_surname> <dd.mm.yyyy date>;<rating.rating>");
                var input = Console.ReadLine().Split(';');
                if (input.Length != 3)
                {
                    return false;
                }
                var personData = input[1].Split(' ');
                if (personData.Length != 3)
                {
                    return false;
                }
                var date = DateTime.ParseExact(personData[2], "dd.mm.yyyy", CultureInfo.InvariantCulture);
                var rating = double.Parse(input[2], CultureInfo.InvariantCulture);
                var author = new Person(personData[0], personData[1], date);
                var article = new Article(author, input[0], rating);
                Articles.Add(article);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Save(string fileName, Magazine magazine) => magazine.Save(fileName);

        public static bool Load(string fileName, Magazine magazine) => magazine.Load(fileName);
    }
}