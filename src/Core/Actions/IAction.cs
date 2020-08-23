using Apache.Arrow;
using Core.Processors;

namespace Core.Actions
{
    public interface IAction
    {
        void Execute(RecordBatch batch, RecordBatch.Builder batchBuilder);
        void Execute(ProcessingData data);
    }
}