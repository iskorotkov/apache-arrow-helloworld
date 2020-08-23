using Core.Actions;

namespace Core.Processors
{
    public interface IProcessor
    {
        void Process(int entities, int iterations, IAction[] actions);
    }
}