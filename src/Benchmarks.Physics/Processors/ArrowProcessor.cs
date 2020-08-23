using System.Collections.Generic;
using System.Linq;
using Apache.Arrow;
using Apache.Arrow.Memory;
using Core.Actions;
using Core.DataGenerators;
using Core.Processors;

namespace Benchmarks.Physics.Processors
{
    public class ArrowProcessor : IProcessor
    {
        private IDataSetGenerator Generator { get; }

        public ArrowProcessor(IDataSetGenerator generator)
        {
            Generator = generator;
        }

        public void Process(int entities, int iterations, IAction[] actions)
        {
            var allocator = new NativeMemoryAllocator();
            var batch = CreateBatch(allocator, entities);
            ExecuteActions(allocator, batch, actions.ToArray(), iterations);
        }

        private static void ExecuteActions(MemoryAllocator allocator, RecordBatch batch, IReadOnlyList<IAction> actions,
            int iterations)
        {
            for (var i = 0; i < iterations; i++)
            {
                var builder = new RecordBatch.Builder(allocator);
                foreach (var action in actions)
                {
                    action.Execute(batch, builder);
                }

                batch = builder.Build();
            }
        }

        private RecordBatch CreateBatch(MemoryAllocator allocator, int entities)
        {
            var data = Generator.New(entities);
            var velocityArrayBuilder = new FloatArray.Builder().AppendRange(data.GetArrayAs<float>("Velocity"));
            var forceArrayBuilder = new FloatArray.Builder().AppendRange(data.GetArrayAs<float>("Force"));
            var massArrayBuilder = new FloatArray.Builder().AppendRange(data.GetArrayAs<float>("Mass"));

            return new RecordBatch.Builder(allocator)
                .Append("Velocity", false, velocityArrayBuilder)
                .Append("Force", false, forceArrayBuilder)
                .Append("Mass", false, massArrayBuilder)
                .Build();
        }
    }
}