namespace DiffEq.Generator.Models
{
    public interface IEquation
    {
        string Equation { get; set; }
        string EquationLatex { get; set; }
        int Type { get; }
        string Solution { get; set; }
        string SolutionLatex { get; set; }
    }
}
