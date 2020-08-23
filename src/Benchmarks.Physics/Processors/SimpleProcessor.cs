using System.Linq;
using Core.Actions;
using Core.DataGenerators;
using Core.Processors;

namespace Benchmarks.Physics.Processors
{
    public class SimpleProcessor : IProcessor
    {
        private IDataGenerator Generator { get; }

        public SimpleProcessor(IDataGenerator generator)
        {
            Generator = generator;
        }

        public void Process(int entities, int iterations, IAction[] actions)
        {
            var data = new ProcessingData
            {
                Arrays =
                {
                    ["Velocity"] = Generator.GetFloat(entities).ToArray(),
                    ["Force"] = Generator.GetFloat(entities).ToArray(),
                    ["Mass"] = Generator.GetFloat(entities).ToArray()
                }
            };

            for (var i = 0; i < iterations; i++)
            {
                foreach (var action in actions)
                {
                    action.Execute(data);
                }
            }
        }
    }
}