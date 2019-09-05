﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Generator
{
    class Homogeneous : IEquation
    {
        public string Eq { get; set; }
        public string LatexEq { get; set; }
        public int Type { get; }
        public string Solution { get; set; }
        public string SolutionLatex { get; set; }

        public Homogeneous()
        {
            Type = 2;
        }
    }
}
