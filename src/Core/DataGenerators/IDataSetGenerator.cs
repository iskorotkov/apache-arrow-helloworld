using Core.Processors;

namespace Core.DataGenerators
{
    public interface IDataSetGenerator
    {
        ProcessingData New(int size);
    }
}