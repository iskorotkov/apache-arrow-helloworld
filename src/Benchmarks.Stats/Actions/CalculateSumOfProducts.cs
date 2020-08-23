using System;
using Apache.Arrow;
using Core.Actions;
using Core.Data;

namespace Benchmarks.Stats.Actions
{
    public class CalculateSumOfProducts : IAction
    {
        public void Execute(RecordBatch batch, RecordBatch.Builder batchBuilder)
        {
            var array = (FloatArray) batch.Column("Values");
            var array2 = (FloatArray) batch.Column("Values2");
            var values = array.Values;
            var values2 = array2.Values;
            FindSum(values, values2);
        }

        private static void FindSum(ReadOnlySpan<float> values, ReadOnlySpan<float> values2)
        {
            var sum = 0f;
            var length = values.Length;
            for (var i = 0; i < length; i++)
            {
                sum += values[i] * values2[i];
            }
        }

        public void Execute(ProcessingData data)
        {
            var values = data.GetAs<float>("Values");
            var values2 = data.GetAs<float>("Values2");
            FindSum(values, values2);
        }
    }
}