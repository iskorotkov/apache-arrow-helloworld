using Apache.Arrow;
using ApacheArrowCs.Processors.Physics;

namespace ApacheArrowCs.Actions.Physics
{
    public class ApplyForce : IAction
    {
        public void Execute(RecordBatch batch, RecordBatch.Builder batchBuilder)
        {
            var velocity = (FloatArray) batch.Column("Velocity");
            var force = (FloatArray) batch.Column("Force");
            var mass = (FloatArray) batch.Column("Mass");

            var length = velocity.Length;
            var results = new float[length];
            for (var i = 0; i < length; i++)
            {
                results[i] = velocity.Values[i] + force.Values[i] / mass.Values[i];
            }

            batchBuilder.Append("Velocity", false, arrayBuilder => arrayBuilder.Float(builder => builder.AppendRange(results)));
        }

        public void Execute(SimpleProcessor.ProcessingData data)
        {
            var length = data.Length;
            for (var i = 0; i < length; i++)
            {
                data.Velocity[i] += data.Force[i] / data.Mass[i];
            }
        }
    }
}