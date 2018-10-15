using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public class Listener
    {
        public Listener()
        {
            Changes = new List<ListEntry>();
        }

        public List<ListEntry> Changes { get; }

        public void Report(object source, MagazineListHandlerEventArgs args)
        {
            Changes.Add(new ListEntry(args.CollectionName, args.ChangeType, args.ElementIndex));
        }

        public override string ToString()
        {
            var sb = new StringBuilder($"Total changes: {Changes.Count}{Environment.NewLine}");
            for (var i = 0; i < Changes.Count; i++)
            {
                sb.Append($"{i + 1}: {Changes[i]}{Environment.NewLine}");
            }
            return sb.ToString();
        }
    }
}
