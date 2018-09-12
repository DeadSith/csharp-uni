using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab1
{
    public class Magazine
    {
        private string _name;

        private Frequency _frequency;

        private DateTime _publicationDate;

        private int _circulation;

        private Article[] _articles;

        public Magazine(string name, Frequency frequency, DateTime publicationDate, Article[] articles)
        {
            _name = name;
            _frequency = frequency;
            _publicationDate = publicationDate;
            _articles = articles;
        }

        public Magazine()
        {
            _name = "";
            _articles = new Article[0];
            _publicationDate = DateTime.Now;
            _frequency = Frequency.Monthly;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public Frequency Frequency
        {
            get => _frequency;
            set => _frequency = value;
        }

        public int Circulation
        {
            get => _circulation;
            set => _circulation = value;
        }

        public DateTime PublicationDate
        {
            get => _publicationDate;
            set => _publicationDate = value;
        }

        public Article[] Articles
        {
            get => _articles;
            set => _articles = value;
        }

        public double MediumRating
        {
            get
            {
                if (_articles.Length == 0)
                    return 0;
                return _articles.Average(a => a.Rating);
            }
        } 

        public bool this[Frequency frequency] => _frequency == frequency;

        public void AddArticles(params Article[] articles)
        {
            var size = _articles.Length;
            Array.Resize(ref _articles, size + articles.Length);
            Array.Copy(articles, 0, _articles, size, articles.Length);
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
    }
}
