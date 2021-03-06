﻿using System.Collections.Generic;
using System.Linq;

namespace Core.Data
{
    public class ProcessingData
    {
        public Dictionary<string, object> Arrays { get; } = new Dictionary<string, object>();

        public T[] GetAs<T>(string name)
        {
            return (T[]) Arrays[name];
        }

        public int Length => Arrays.First().Key.Length;
    }
}