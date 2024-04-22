using System;
using System.Collections.Generic;

namespace _Base.Scripts.Utils
{
    public class ProbabilityPicker<T>
    {
        private Random rand;
        private Dictionary<T, double> probabilities;

        public ProbabilityPicker(Dictionary<T, double> probabilities)
        {
            this.probabilities = probabilities;
            this.rand = new Random();
        }

        public T Choose()
        {
            double totalProbability = 0;
            double randomNumber = rand.NextDouble();

            foreach (var kvp in probabilities)
            {
                totalProbability += kvp.Value;
                if (randomNumber < totalProbability)
                {
                    return kvp.Key;
                }
            }

            // If probabilities do not sum up to 1, return default value
            throw new InvalidOperationException("Probabilities do not sum up to 1.");
        }
    }
}