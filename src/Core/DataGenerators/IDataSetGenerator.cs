using Apache.Arrow;
using Apache.Arrow.Memory;
using Core.Processors;

namespace Core.DataGenerators
{
    public interface IDataSetGenerator
    {
        ProcessingData New(int size);
        RecordBatch CreateBatch(MemoryAllocator allocator, int entities);
    }
}