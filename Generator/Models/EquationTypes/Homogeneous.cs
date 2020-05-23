namespace DiffEq.Models
{
    class Homogeneous : IEquation
    {
        public string Equation { get; set; }
        public string EquationLatex { get; set; }
        public int Type { get; }
        public string Solution { get; set; }
        public string SolutionLatex { get; set; }

        public Homogeneous()
        {
            Type = 2;
        }
    }
}
