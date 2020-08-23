using System;
using Apache.Arrow;
using Core.Actions;
using Core.Data;

namespace Benchmarks.Stats.Actions
{
    public class CalculateMinMax : IAction
    {
        public void Execute(RecordBatch batch, RecordBatch.Builder batchBuilder)
        {
            var array = (FloatArray) batch.Column("Values");
            var values = array.Values;
            FindMinMax(values);
        }

        private static void FindMinMax(ReadOnlySpan<float> values)
        {
            var min = values[0];
            var max = values[0];
            foreach (var value in values)
            {
                if (value > min) min = value;
                if (value < max) max = value;
            }
        }

        public void Execute(ProcessingData data)
        {
            var values = data.GetAs<float>("Values");
            FindMinMax(values);
        }
    }
}