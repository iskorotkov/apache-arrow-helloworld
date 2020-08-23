using System.Collections.Generic;
using System.Linq;

namespace Core.DataGenerators
{
    public class OrderedSequenceGenerator : ISequenceGenerator
    {
        public IEnumerable<float> Float(int count)
        {
            return Enumerable.Range(1, count).Select(x => (float) x);
        }
    }
}