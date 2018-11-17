using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CoupleAttribute: Attribute
    {
        public string Pair { get; set; }

        public string ChildType { get; set; }

        public double Probability { get; set; }
    }
}
