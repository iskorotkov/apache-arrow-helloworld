using System.Linq;
using Apache.Arrow;
using ApacheArrowCs.DataGenerators;
using ApacheArrowCs.Processors.Physics;

namespace ApacheArrowCs.Actions.Physics
{
    public class RandomizeForce : IAction
    {
        private IDataGenerator Generator { get; }

        public RandomizeForce(IDataGenerator generator)
        {
            Generator = generator;
        }

        public void Execute(RecordBatch batch, RecordBatch.Builder batchBuilder)
        {
            var length = batch.Arrays.First().Length;
            var values = Generator.GetFloat(length);
            batchBuilder.Append("Force", false, arrayBuilder => arrayBuilder.Float(builder =>
                builder.AppendRange(values)));
        }

        public void Execute(SimpleProcessor.ProcessingData data)
        {
            var length = data.Length;
            data.Velocity = Generator.GetFloat(length).ToArray();
        }
    }
}