﻿using System.Collections.Generic;
using System.Linq;

namespace ApacheArrowCs.DataGenerators
{
    public class OrderedValuesGenerator : IDataGenerator
    {
        public IEnumerable<float> GetFloat(int count)
        {
            return Enumerable.Range(1, count).Select(x => (float) x);
        }
    }
}