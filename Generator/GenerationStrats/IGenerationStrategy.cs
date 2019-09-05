using System;
using System.Collections.Generic;
using System.Text;

namespace Generator
{
    public interface IGenerationStrategy
    {
        string ExecuteTreeAlgorithm(string variable, int difficulty);
    }
}
