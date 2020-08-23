using System;
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
            var builder = new RecordBatch.Builder(allocator);
            for (var i = 0; i < iterations; i++)
            {
                foreach (var action in actions)
                {
                    action.Execute(batch, builder);
                }

                try
                {
                    batch = builder.Build();
                    builder = new RecordBatch.Builder(allocator);
                }
                catch (InvalidOperationException)
                {
                }
            }
        }
    }
}