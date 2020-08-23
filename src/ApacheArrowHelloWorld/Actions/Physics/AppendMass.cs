using Apache.Arrow;
using ApacheArrowCs.Processors.Physics;

namespace ApacheArrowCs.Actions.Physics
{
    public class AppendMass : IAction
    {
        public void Execute(RecordBatch batch, RecordBatch.Builder batchBuilder)
        {
            batchBuilder.Append("Mass", false, batch.Column("Mass"));
        }

        public void Execute(SimpleProcessor.ProcessingData data)
        {
        }
    }
}