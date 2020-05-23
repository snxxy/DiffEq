namespace EquationDB
{
    public partial class Equation
    {
        public int? Id { get; set; }
        public string Eq { get; set; }
        public string Latex { get; set; }
        public string Solution { get; set; }
        public string SolutionLatex { get; set; }
        public int Type { get; set; }
    }
}
