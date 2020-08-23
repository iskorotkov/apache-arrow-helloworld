using System.Collections.Generic;

namespace ApacheArrowCs.DataGenerators
{
    public interface IDataGenerator
    {
        IEnumerable<float> GetFloat(int count);
    }
}