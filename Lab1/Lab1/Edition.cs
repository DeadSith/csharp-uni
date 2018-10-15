using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public class Edition
    {
        protected string _name;

        protected DateTime _publicationDate;

        protected int _circulation;

        public string Name { get => _name; set => _name = value; }
        public DateTime PublicationDate { get => _publicationDate; set => _publicationDate = value; }
        public int Circulation { get => _circulation;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Cirulation should be 0 or greater");
                }
                _circulation = value;
            }
        }

        public Edition()
        {
            _name = "";
            _publicationDate = DateTime.Now;
            _circulation = 0;
        }

        public Edition(string name, DateTime publicationDate, int circulation)
        {
            _name = name;
            _publicationDate = publicationDate;
            _circulation = circulation;
        }

        public override bool Equals(object obj)
        {
            var edition = obj as Edition;
            if (ReferenceEquals(edition, null))
            {
                return false;
            }
            return _name == edition._name &&
                   _publicationDate == edition._publicationDate &&
                   _circulation == edition._circulation;
        }

        public override int GetHashCode() => HashCode.Combine(_name, _publicationDate, _circulation);
        public override string ToString() => $"Name:{_name}{Environment.NewLine}"
            + $"Publication date: {_publicationDate}{Environment.NewLine}"
            + $"Circulation:{_circulation}{Environment.NewLine}";

        public static bool operator ==(Edition edition1, Edition edition2)
        {
            return edition1.Equals(edition2);
        }

        public static bool operator !=(Edition edition1, Edition edition2)
        {
            return !edition1.Equals(edition2);
        }

        public virtual object DeepCopy()
        {
            var res = new Edition(Name, PublicationDate, Circulation);
            return res;
        }
    }
}
