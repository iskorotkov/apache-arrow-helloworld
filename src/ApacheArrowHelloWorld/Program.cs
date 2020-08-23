using Benchmarks.Physics.Actions;
using Benchmarks.Physics.Processors;
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
            var processors = new IProcessor[]
            {
                new SimpleProcessor(new OrderedValuesGenerator()),
                new ArrowProcessor(new OrderedValuesGenerator()),
            };

            var actions = new IAction[]
            {
                new ApplyForce(),
                new RandomizeForce(new OrderedValuesGenerator()),
                new AppendMass()
            };

            var benchmark = new Benchmark();
            benchmark.RunForAll(entities: 1000000, iterations: 1, processors, actions);
        }
    }
}