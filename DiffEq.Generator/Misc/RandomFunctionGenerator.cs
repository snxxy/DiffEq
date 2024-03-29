﻿using DiffEq.Generator.Strats;

namespace DiffEq.Generator.Misc
{
    class RandomFunctionGenerator
    {
        private IGenerationStrategy strategy { get; }
        public RandomFunctionGenerator(IGenerationStrategy _strategy)
        {
            strategy = _strategy;
        }

        public string Generate(string variable, int difficulty, bool pow = false)
        {
            var result = strategy.ExecuteTreeAlgorithm(variable, difficulty);
            return result;
        }
    }
}
