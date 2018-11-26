using System.Collections.Generic;

namespace Lab1
{
    public class TestCollection
    {
        private readonly List<Edition> _editions;

        private readonly List<string> _keys;

        private readonly Dictionary<string, Magazine> _stringDictionary;

        private readonly Dictionary<Edition, Magazine> _typeDictionary;

        public TestCollection(int count)
        {
            _editions = new List<Edition>(count);
            _keys = new List<string>(count);
            _stringDictionary = new Dictionary<string, Magazine>(count);
            _typeDictionary = new Dictionary<Edition, Magazine>(count);
        }

        public static Magazine[] GenerateMagazines(int count)
        {
            var res = new Magazine[count];
            for (var i = 0; i < count; i++)
            {
                res[i] = new Magazine();
            }
            return res;
        }
    }
}