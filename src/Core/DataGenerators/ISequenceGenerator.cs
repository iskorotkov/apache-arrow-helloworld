using System.Collections.Generic;

namespace Core.DataGenerators
{
    public interface ISequenceGenerator
    {
        IEnumerable<float> Float(int count);
    }
}