﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public interface IRateAndCopy
    {
        double Rating { get; }

        object DeepCopy();
    }
}
