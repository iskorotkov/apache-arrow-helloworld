using Apache.Arrow;
using Core.Data;

namespace Core.Actions
{
    public interface IAction
    {
        void Execute(RecordBatch batch, RecordBatch.Builder batchBuilder);
        void Execute(ProcessingData data);
    }
}