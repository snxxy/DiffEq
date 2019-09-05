using System;
using System.Collections.Generic;
using System.Text;

namespace Generator
{
    public interface IEquation
    {
        string Eq { get; set; }
        string LatexEq { get; set; }
        int Type { get; }
        string Solution { get; set; }
        string SolutionLatex { get; set; }
    }
}
