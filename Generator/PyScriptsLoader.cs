using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using System.Collections.Generic;

namespace Generator
{
    public sealed class PyScriptsLoader
    {
        private ScriptEngine pythonEngine = null;
        private ScriptScope pythonScope = null;
        private ICollection<string> paths = null;

        public PyScriptsLoader()
        {
            pythonEngine = Python.CreateEngine();
            pythonScope = pythonEngine.CreateScope();
            paths = pythonEngine.GetSearchPaths();
            paths.Add(@"..\..\..\"); //for bin/debug/netcoreapp21/
            pythonEngine.SetSearchPaths(paths);
        }

        public string Solve(string equation, out string solution, out string solutionLatex)
        {
            string l = equation.Split("=")[0];
            string r = equation.Split("=")[1];
            var script = pythonEngine.CreateScriptSourceFromString(string.Format(@"
import sympy as sym
from sympy import *


x = Symbol('x')
y = Function('y')(x)
dydx = y.diff(x)
expr = Eq({0}, {1})
#sol = sym.dsolve(expr, simplify=False)

toLatex = latex(expr)

import clr
from System import String

result = clr.Convert(toLatex, String)
#solu = clr.Convert(sol, String)
#solToLatex = latex(sol)
#soluToLatex = clr.Convert(solToLatex, String)
", l,r));
            script.Execute(pythonScope);
            //solution = pythonScope.GetVariable("solu");
            //solutionLatex = pythonScope.GetVariable("soluToLatex");
            solution = null;
            solutionLatex = null;
            return pythonScope.GetVariable("result");
        }

        public string DoScrambling(string equation, int maxVar)
        {
            var script = pythonEngine.CreateScriptSourceFromString(string.Format(@"
import sympy
from sympy import *
from sympy.parsing.sympy_parser import parse_expr

x = Symbol('x')
y = Function('y')(x)
dydx = y.diff(x)
expr = Eq({0})
expr = expr * (x**{1})

import clr
from System import String

result = clr.Convert(expr, String)", equation, maxVar));

            script.Execute(pythonScope);
            
            return pythonScope.GetVariable("result");
        }

    }
}
