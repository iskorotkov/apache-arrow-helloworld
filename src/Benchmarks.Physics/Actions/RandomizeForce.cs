using System.Linq;
using Apache.Arrow;
using Core.Actions;
using Core.DataGenerators;
using Core.Processors;

namespace Benchmarks.Physics.Actions
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

        public void Execute(ProcessingData data)
        {
            var length = data.Length;
            data.Arrays["Force"] = Generator.GetFloat(length).ToArray();
        }
    }
}