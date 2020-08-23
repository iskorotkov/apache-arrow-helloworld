using System;
using ApacheArrowCs.Actions;
using ApacheArrowCs.Processors;

namespace ApacheArrowCs.Benchmarks
{
    public class Benchmark : IBenchmark
    {
        public void RunForAll(int entities, int iterations, IProcessor[] processors, IAction[] actions)
        {
            foreach (var processor in processors)
            {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                Run(entities, iterations, processor, actions);
                stopwatch.Stop();
                Console.WriteLine($"{processor.GetType()}: {stopwatch.ElapsedMilliseconds} ms elapsed");
            }
        }

        public void Run(int entities, int iterations, IProcessor processor, IAction[] actions)
        {
            processor.Process(entities, iterations, actions: actions);
        }
    }
}