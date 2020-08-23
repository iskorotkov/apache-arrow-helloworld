using System.Collections.Generic;

namespace Core.DataGenerators
{
    public interface IDataGenerator
    {
        IEnumerable<float> GetFloat(int count);
    }
}