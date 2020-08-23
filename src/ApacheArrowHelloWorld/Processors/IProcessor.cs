using ApacheArrowCs.Actions;

namespace ApacheArrowCs.Processors
{
    public interface IProcessor
    {
        void Process(int entities, int iterations, IAction[] actions);
    }
}