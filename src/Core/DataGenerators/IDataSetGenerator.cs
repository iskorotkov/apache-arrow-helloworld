using Apache.Arrow;
using Apache.Arrow.Memory;
using Core.Data;

namespace Core.DataGenerators
{
    public interface IDataSetGenerator
    {
        ProcessingData Generate(int size);
        RecordBatch GenerateBatch(MemoryAllocator allocator, int size);
    }
}