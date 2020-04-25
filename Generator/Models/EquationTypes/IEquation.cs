using System;
using System.Collections.Generic;
using System.Text;

namespace Generator
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
