using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab1
{
    public class MagazineCollection
    {
        private readonly List<Magazine> _magazines
            = new List<Magazine>();

        public double MaxRating => _magazines.DefaultIfEmpty(new Magazine()).Max(m => m.MediumRating);

        public IEnumerable<Magazine> MonthlyMagazines => _magazines
            .DefaultIfEmpty(new Magazine { Frequency = Frequency.Yearly })
            .Where(m => m.Frequency == Frequency.Yearly);

        public List<Magazine> GetMagazinesWithMinRating(double rating)
        {
            if (_magazines.Count == 0)
            {
                return null;
            }
            return _magazines.GroupBy(m => m.MediumRating >= rating).Where(g => g.Key == true)
                .SelectMany(g => g).ToList();
        }

        public void AddDefaults()
        {
            for (var i = 0; i < 5; i++)
            {
                _magazines.Add(new Magazine());
            }
        }

        public void AddMagazines(params Magazine[] magazines)
        {
            _magazines.AddRange(magazines);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(var article in _magazines)
            {
                sb.Append(article.ToString());
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public string ToShortString()
        {
            var sb = new StringBuilder();
            foreach (var article in _magazines)
            {
                sb.Append(article.ToShortString());
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public void SortByName()
        {
            _magazines.Sort();
        }

        public void SortByDate()
        {
            _magazines.Sort(new Edition());
        }

        public void SortByCirculation()
        {
            _magazines.Sort(new EditionCirculationComparer());
        }
    }
}
