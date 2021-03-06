﻿using Core.Actions;
using Core.DataGenerators;

namespace Core.Processors
{
    public class SimpleProcessor : IProcessor
    {
        private IDataSetGenerator Generator { get; }

        public SimpleProcessor(IDataSetGenerator generator)
        {
            Generator = generator;
        }

        public void Process(int entities, int iterations, IAction[] actions)
        {
            var data = Generator.Generate(entities);
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