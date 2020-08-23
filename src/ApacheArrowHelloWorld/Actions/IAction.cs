using Apache.Arrow;
using ApacheArrowCs.Processors.Physics;

namespace ApacheArrowCs.Actions
{
    public interface IAction
    {
        void Execute(RecordBatch batch, RecordBatch.Builder batchBuilder);
        void Execute(SimpleProcessor.ProcessingData data);
    }
}