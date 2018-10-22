using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public interface IRateAndCopy<T>
    {
        double Rating { get; }

        T DeepCopy();
    }
}
