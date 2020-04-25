using System;
using System.Collections.Generic;
using System.Text;

namespace GeneratorService.Models
{
    class PyEquationJson
    {
        public string LeftSide { get; set; }
        public string RightSide { get; set; }
        public string EquationLatex { get; set; }
        public string Solution { get; set; }
        public string SolutionLatex { get; set; }
    }
}
