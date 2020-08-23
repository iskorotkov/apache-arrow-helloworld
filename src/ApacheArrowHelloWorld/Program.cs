using ApacheArrowCs.Actions;
using ApacheArrowCs.Actions.Physics;
using ApacheArrowCs.Benchmarks;
using ApacheArrowCs.DataGenerators;
using ApacheArrowCs.Processors;
using ApacheArrowCs.Processors.Physics;

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