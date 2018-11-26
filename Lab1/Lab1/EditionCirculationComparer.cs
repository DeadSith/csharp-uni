using System.Collections.Generic;

namespace Lab1
{
    public class EditionCirculationComparer : IComparer<Edition>
    {
        public int Compare(Edition x, Edition y)
        {
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            return x.Circulation.CompareTo(y.Circulation);
        }
    }
}