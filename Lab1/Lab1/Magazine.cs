using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace Lab1
{
    public class Magazine: Edition, IRateAndCopy
    {

        private Frequency _frequency;

        private List<Article> _articles;

        public Magazine(string name, Frequency frequency, DateTime publicationDate,
            int circulation, List<Article> articles): base(name, publicationDate, circulation)
        {
            _articles = articles;
        }

        public Magazine(): base()
        {
            _articles = new List<Article>();
        }

        public Frequency Frequency
        {
            get => _frequency;
            set => _frequency = value;
        }

        public List<Article> Articles
        {
            get => _articles;
            set => _articles = value;
        }

        public List<Person> Editors { get; set; }

        public double MediumRating
        {
            get
            {
                if (_articles.Count == 0)
                    return 0;
                var sum = .0;
                foreach (var article in _articles)
                {
                    sum += ((Article)article).Rating;
                }
                return sum / _articles.Count;
            }
        }

        public double Rating => MediumRating;

        public bool this[Frequency frequency] => _frequency == frequency;

        public void AddArticles(params Article[] articles)
        {
            _articles.AddRange(articles);
        }

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
            foreach (var article in _articles)
            {
                sb.Append($"Article:{article}{Environment.NewLine}{Environment.NewLine}");
            }

            return $"Name:{_name}{Environment.NewLine}Frequency:{_frequency}{Environment.NewLine}"
                + $"Publication date: {_publicationDate}{Environment.NewLine}{Environment.NewLine}"
                + $"Circulation:{_circulation}{Environment.NewLine}Articles:{Environment.NewLine}{sb}";
        }

        public virtual string ToShortString() => $"Name:{_name}{Environment.NewLine}"
            + $"Frequency:{_frequency}{Environment.NewLine}"
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


        public override object DeepCopy()
        {
            var res = new Magazine(Name, _frequency, PublicationDate, Circulation, new List<Article>(Articles.Count));
            foreach(var o in Articles)
            {
                var a = o as Article;
                res.Articles.Add(a.DeepCopy() as Article);
            }
            var editors = new List<Person>(Editors.Count);
            foreach (var o in Editors)
            {
                var e = o as Person;
                editors.Add(e.DeepCopy() as Person);
            }
            res.Editors = editors;
            return res;
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
            foreach(var o in Articles)
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
    }
}
