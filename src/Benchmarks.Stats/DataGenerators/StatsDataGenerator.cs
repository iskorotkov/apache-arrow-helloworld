using System.Linq;
using Apache.Arrow;
using Apache.Arrow.Memory;
using Core.Data;
using Core.DataGenerators;

namespace Benchmarks.Stats.DataGenerators
{
    public class StatsDataGenerator : IDataSetGenerator
    {
        private ISequenceGenerator Generator { get; }

        public StatsDataGenerator(ISequenceGenerator generator)
        {
            Generator = generator;
        }

        public ProcessingData New(int size)
        {
            return new ProcessingData
            {
                Arrays =
                {
                    ["Values"] = Generator.Float(size).ToArray()
                }
            };
        }

        public RecordBatch CreateBatch(MemoryAllocator allocator, int entities)
        {
            var values = Generator.Float(entities);
            return new RecordBatch.Builder(allocator)
                .Append("Values", false, arrayBuilder => arrayBuilder.Float(builder => builder.AppendRange(values)))
                .Build();
        }
    }
}