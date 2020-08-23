using System.Collections.Generic;
using System.Linq;
using Core.DataGenerators;

namespace DataGenerators.Sequential
{
    public class SequentialGenerator : IDataGenerator
    {
        public IEnumerable<float> GetFloat(int count)
        {
            return Enumerable.Range(1, count).Select(x => (float) x);
        }
    }
}