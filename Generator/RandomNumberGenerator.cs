using System;

namespace Generator
{
    public sealed class RandomNumberGenerator
    {
        private static readonly object singleLock = new object();

        private static RandomNumberGenerator instance = null;

        private static Random rnd = new Random();

        public static RandomNumberGenerator Instance
        {
            get
            {
                lock (singleLock)
                {
                    if (instance == null)
                    {
                        instance = new RandomNumberGenerator();
                    }
                    return instance;
                }
            }
        }

        public double Generate(double from, double to, int accuracy)
        {
            double random = rnd.NextDouble();
            return Math.Round((from + (random * (to - from))), accuracy);
        }

    }
}
