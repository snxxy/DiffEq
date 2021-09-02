namespace DiffEq.Generator.GenerationConfigs
{
    class HomogeneousTreeConfig : IGenerationConfig
    {
        public List<string> Operators { get; }
        public List<string> Trigonometry { get; }
        public int MaxPow { get; }
        public bool IsTrigonometryEnabled { get; }
        public bool IsVariablePowEnabled { get; }

        public HomogeneousTreeConfig()
        {
            Operators = new List<string>() {"+", "-"};
            Trigonometry = null;
            MaxPow = 2;
            IsTrigonometryEnabled = false;
            IsVariablePowEnabled = true;
        }
    }
}
