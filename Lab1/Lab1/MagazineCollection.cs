using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab1
{
    public delegate void MagazineListHandler(object source, MagazineListHandlerEventArgs args);

    public class MagazineCollection
    {
        public event MagazineListHandler MagazineAdded;

        public event MagazineListHandler MagazineReplaced;

        public string Name { get; set; }

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
            var handler = MagazineAdded;
            for (var i = 0; i < 5; i++)
            {
                _magazines.Add(new Magazine());
                handler?.Invoke(this, new MagazineListHandlerEventArgs(Name, "Added", _magazines.Count));
            }
        }

        public void AddMagazines(params Magazine[] magazines)
        {
            var handler = MagazineAdded;
            foreach(var magazine in magazines)
            {
                _magazines.Add(magazine);
                handler?.Invoke(this, new MagazineListHandlerEventArgs(Name, "Added", _magazines.Count));
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var article in _magazines)
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

        public Magazine this[int index]
        {
            get
            {
                if (index < 0 || index >= _magazines.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return _magazines[index];
            }
            set
            {
                if (index < 0 || index >= _magazines.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                var handler = MagazineReplaced;
                _magazines[index] = value;
                handler?.Invoke(this, new MagazineListHandlerEventArgs(Name, "Replaced", index));
            }
        }

        public bool Replace(int index, Magazine magazine)
        {
            if (index < 0 || index >= _magazines.Count)
            {
                return false;
            }
            var handler = MagazineReplaced;
            _magazines[index] = magazine;
            handler?.Invoke(this, new MagazineListHandlerEventArgs(Name, "Replaced", index));
            return true;
        }
    }
}
