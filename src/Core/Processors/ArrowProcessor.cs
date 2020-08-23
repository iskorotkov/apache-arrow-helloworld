using System.Collections.Generic;
using System.Linq;
using Apache.Arrow;
using Apache.Arrow.Memory;
using Core.Actions;
using Core.DataGenerators;

namespace Core.Processors
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
            var batch = Generator.CreateBatch(allocator, entities);
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
    }
}