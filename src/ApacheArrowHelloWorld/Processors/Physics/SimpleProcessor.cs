using System.Linq;
using ApacheArrowCs.Actions;
using ApacheArrowCs.DataGenerators;

namespace ApacheArrowCs.Processors.Physics
{
    public class SimpleProcessor : IProcessor
    {
        private IDataGenerator Generator { get; }

        public SimpleProcessor(IDataGenerator generator)
        {
            Generator = generator;
        }

        public class ProcessingData
        {
            public float[] Velocity { get; set; }
            public float[] Force { get; set; }
            public float[] Mass { get; set; }

            public int Length => Velocity.Length;
        }

        public void Process(int entities, int iterations, IAction[] actions)
        {
            var data = new ProcessingData
            {
                Velocity = Generator.GetFloat(entities).ToArray(),
                Force = Generator.GetFloat(entities).ToArray(),
                Mass = Generator.GetFloat(entities).ToArray()
            };

            for (var i = 0; i < iterations; i++)
            {
                foreach (var action in actions)
                {
                    action.Execute(data);
                }
            }
        }
    }
}