using System.Collections.Generic;

namespace DiffEq.Generator.GenerationConfigs
{
    class SeparableTreeConfig : IGenerationConfig
    {
        public List<string> Operators { get; }
        public List<string> Trigonometry { get; }
        public int MaxPow { get; }
        public bool IsTrigonometryEnabled { get; }
        public bool IsVariablePowEnabled { get; }

        public SeparableTreeConfig()
        {
            Operators = new List<string>() {"+", "-", "/", "*"};
            Trigonometry = new List<string>() {"sin", "cos", "tan", "cot"};
            MaxPow = 2;
            IsTrigonometryEnabled = true;
            IsVariablePowEnabled = true;
        }
    }
}
