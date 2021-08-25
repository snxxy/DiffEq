using System;

namespace DiffEq.Generator.Misc
{
    class RandomNumberGenerator
    {
        private static Random rnd = new Random();

        public double Generate(double from, double to, int accuracy)
        {
            double random = rnd.NextDouble();
            return Math.Round((from + (random * (to - from))), accuracy);
        }
    }
}
