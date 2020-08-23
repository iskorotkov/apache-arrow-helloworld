using Apache.Arrow;
using Core.Actions;
using Core.Processors;

namespace Benchmarks.Physics.Actions
{
    public class AppendMass : IAction
    {
        public void Execute(RecordBatch batch, RecordBatch.Builder batchBuilder)
        {
            batchBuilder.Append("Mass", false, batch.Column("Mass"));
        }

        public void Execute(ProcessingData data)
        {
        }
    }
}