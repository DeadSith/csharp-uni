using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public class MagazineListHandlerEventArgs: EventArgs
    {
        public MagazineListHandlerEventArgs(string collectionName, string changeType, int elementIndex)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ElementIndex = elementIndex;
        }

        public string CollectionName { get; set; }

        public string ChangeType { get; set; }

        public int ElementIndex { get; set; }

        public override string ToString() => $"Element {ElementIndex} was {ChangeType} in {CollectionName} collection";
    }
}
