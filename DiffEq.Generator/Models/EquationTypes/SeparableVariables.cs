namespace DiffEq.Generator.Models
{
    class SeparableVariables : IEquation
    {
        public string Equation { get; set; }
        public string EquationLatex { get; set; }
        public int Type { get; }
        public string Solution { get; set; }
        public string SolutionLatex { get; set; }

        public SeparableVariables()
        {
            Type = 1;
        }
    }
}
