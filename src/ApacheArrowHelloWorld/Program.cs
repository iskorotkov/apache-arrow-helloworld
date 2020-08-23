// using Benchmarks.Physics.Actions;
// using Benchmarks.Physics.DataGenerators;

using Benchmarks.Stats.Actions;
using Benchmarks.Stats.DataGenerators;
using Core.Actions;
using Core.Benchmarks;
using Core.DataGenerators;
using Core.Processors;

namespace ApacheArrowCs
{
    internal static class Program
    {
        private static void Main()
        {
            var sequenceGenerator = new OrderedSequenceGenerator();
            // var dataGenerator = new PhysicsDataGenerator(sequenceGenerator);
            var dataGenerator = new StatsDataGenerator(sequenceGenerator);

            var processors = new IProcessor[]
            {
                new SimpleProcessor(dataGenerator),
                new ArrowProcessor(dataGenerator),
            };

            var actions = new IAction[]
            {
                // new ApplyForce(),
                // new RandomizeForce(sequenceGenerator),
                // new AppendMass()
                // new CalculateMinMax(),
                new CalculateSumOfProducts()
            };

            var benchmark = new Benchmark();
            benchmark.RunForAll(entities: 1_000_000, iterations: 1000, processors, actions);
        }
    }
}