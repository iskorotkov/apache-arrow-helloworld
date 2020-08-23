using System.Collections.Generic;
using System.Linq;
using Core.DataGenerators;

namespace DataGenerators.Sequential
{
    public class OrderedSequenceGenerator : ISequenceGenerator
    {
        public IEnumerable<float> Float(int count)
        {
            return Enumerable.Range(1, count).Select(x => (float) x);
        }
    }
}