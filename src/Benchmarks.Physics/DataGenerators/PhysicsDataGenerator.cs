using System.Linq;
using Core.DataGenerators;
using Core.Processors;

namespace Benchmarks.Physics.DataGenerators
{
    public class PhysicsDataGenerator : IDataSetGenerator
    {
        private ISequenceGenerator Generator { get; }

        public PhysicsDataGenerator(ISequenceGenerator generator)
        {
            Generator = generator;
        }

        public ProcessingData New(int size)
        {
            return new ProcessingData
            {
                Arrays =
                {
                    ["Velocity"] = Generator.Float(size).ToArray(),
                    ["Force"] = Generator.Float(size).ToArray(),
                    ["Mass"] = Generator.Float(size).ToArray()
                }
            };
        }
    }
}