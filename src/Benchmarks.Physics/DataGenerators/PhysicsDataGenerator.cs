﻿using System.Linq;
using Apache.Arrow;
using Apache.Arrow.Memory;
using Core.Data;
using Core.DataGenerators;

namespace Benchmarks.Physics.DataGenerators
{
    public class PhysicsDataGenerator : IDataSetGenerator
    {
        private ISequenceGenerator Generator { get; }

        public PhysicsDataGenerator(ISequenceGenerator generator)
        {
            Generator = generator;
        }

        public ProcessingData Generate(int size)
        {
            return new ProcessingData
            {
                Arrays =
                {
                    ["Velocity"] = Generator.Float(size).ToArray(),
                    ["Force"] = Generator.Float(size).ToArray(),
                    ["Mass"] = Generator.Float(size).ToArray()
                }
            };
        }

        public RecordBatch GenerateBatch(MemoryAllocator allocator, int entities)
        {
            var data = Generate(entities);
            var velocityArrayBuilder = new FloatArray.Builder().AppendRange(data.GetAs<float>("Velocity"));
            var forceArrayBuilder = new FloatArray.Builder().AppendRange(data.GetAs<float>("Force"));
            var massArrayBuilder = new FloatArray.Builder().AppendRange(data.GetAs<float>("Mass"));

            return new RecordBatch.Builder(allocator)
                .Append("Velocity", false, velocityArrayBuilder)
                .Append("Force", false, forceArrayBuilder)
                .Append("Mass", false, massArrayBuilder)
                .Build();
        }
    }
}