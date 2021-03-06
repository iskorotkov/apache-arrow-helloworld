﻿using System.Linq;
using Apache.Arrow;
using Core.Actions;
using Core.Data;
using Core.DataGenerators;

namespace Benchmarks.Physics.Actions
{
    public class RandomizeForce : IAction
    {
        private ISequenceGenerator Generator { get; }

        public RandomizeForce(ISequenceGenerator generator)
        {
            Generator = generator;
        }

        public void Execute(RecordBatch batch, RecordBatch.Builder batchBuilder)
        {
            var length = batch.Arrays.First().Length;
            var values = Generator.Float(length);
            batchBuilder.Append("Force", false, arrayBuilder => arrayBuilder.Float(builder =>
                builder.AppendRange(values)));
        }

        public void Execute(ProcessingData data)
        {
            var length = data.Length;
            data.Arrays["Force"] = Generator.Float(length).ToArray();
        }
    }
}