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
        private IDataGenerator Generator { get; }

        public ArrowProcessor(IDataGenerator generator)
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

        // TODO: Move CreateBatch to another class
        private RecordBatch CreateBatch(MemoryAllocator allocator, int entities)
        {
            var velocityArrayBuilder = new FloatArray.Builder().AppendRange(Generator.GetFloat(entities));
            var forceArrayBuilder = new FloatArray.Builder().AppendRange(Generator.GetFloat(entities));
            var massArrayBuilder = new FloatArray.Builder().AppendRange(Generator.GetFloat(entities));

            return new RecordBatch.Builder(allocator)
                .Append("Velocity", false, velocityArrayBuilder)
                .Append("Force", false, forceArrayBuilder)
                .Append("Mass", false, massArrayBuilder)
                .Build();
        }
    }
}