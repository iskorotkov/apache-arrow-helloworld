using Core.Actions;
using Core.Processors;

namespace Core.Benchmarks
{
    public interface IBenchmark
    {
        void RunForAll(int entities, int iterations, IProcessor[] processors, IAction[] actions);
        void Run(int entities, int iterations, IProcessor processor, IAction[] actions);
    }
}