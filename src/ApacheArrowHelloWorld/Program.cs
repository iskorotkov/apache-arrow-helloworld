using Benchmarks.Physics.Actions;
using Benchmarks.Physics.DataGenerators;
using Benchmarks.Physics.Processors;
using Core.Actions;
using Core.Benchmarks;
using Core.Processors;
using DataGenerators.Sequential;

namespace ApacheArrowCs
{
    internal static class Program
    {
        private static void Main()
        {
            var sequenceGenerator = new OrderedSequenceGenerator();
            var dataGenerator = new PhysicsDataGenerator(sequenceGenerator);
            var processors = new IProcessor[]
            {
                new SimpleProcessor(dataGenerator),
                new ArrowProcessor(dataGenerator),
            };

            var actions = new IAction[]
            {
                new ApplyForce(),
                new RandomizeForce(sequenceGenerator),
                new AppendMass()
            };

            var benchmark = new Benchmark();
            benchmark.RunForAll(entities: 1000000, iterations: 1, processors, actions);
        }
    }
}