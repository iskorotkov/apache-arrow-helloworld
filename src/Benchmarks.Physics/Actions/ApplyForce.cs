using Apache.Arrow;
using Core.Actions;
using Core.Processors;

namespace Benchmarks.Physics.Actions
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

            batchBuilder.Append("Velocity", false,
                arrayBuilder => arrayBuilder.Float(builder => builder.AppendRange(results)));
        }

        public void Execute(ProcessingData data)
        {
            var length = data.Length;
            for (var i = 0; i < length; i++)
            {
                data.GetAs<float>("Velocity")[i] +=
                    data.GetAs<float>("Force")[i] / data.GetAs<float>("Mass")[i];
            }
        }
    }
}