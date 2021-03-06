﻿using System;
using System.Threading.Tasks;
using DiffEq.Models;

namespace DiffEq
{
    sealed class EquationManager
    {
        private int maxVar = 0;

        //много ненужных созданий объектов
        public async Task<IEquation> SolveAndScramble(IEquation equation)
        {
            maxVar = 0;
            PyRestApiHelper pyRestApiHelper = new PyRestApiHelper();

            if (equation.Type == 1)
            {
                var jsonEquation = await pyRestApiHelper.SolveEquation(equation.Equation.Split('=')[0], equation.Equation.Split('=')[1]);
                equation = MapJsonToDto(jsonEquation, equation.Type);

                return equation;
            }
            else if (equation.Type == 2)
            {
                var jsonEquation = await pyRestApiHelper.SolveEquation(equation.Equation.Split('=')[0], equation.Equation.Split('=')[1]);
                //мб не возвращать новый equation?
                equation = MapJsonToDto(jsonEquation, equation.Type);
                //fix scramble
                //equation = Scramble(equation);

                return equation;
            }
            return null;
        }
        private async Task<IEquation> Scramble(IEquation equation)
        {
            PyRestApiHelper pyRestApiHelper = new PyRestApiHelper();

            for (int i = 0; i < equation.Equation.Length - 1; i++)
            {
                if (equation.Equation[i].Equals('*') && equation.Equation[i + 1].Equals('*'))
                {
                    if (((int)(char.GetNumericValue(equation.Equation[i + 2])) > maxVar))
                    {
                        maxVar = (int)(char.GetNumericValue(equation.Equation[i + 2]));
                    }
                }
            }

            var equals = equation.Equation.Split("=");
            var a = equals[0].Split(")/(");
            a[0] = a[0] + ")";
            a[1] = "(" + a[1];
            var jsonExpression = await pyRestApiHelper.ScrambleExpression(a[0], maxVar);
            a[0] = jsonExpression.Expression;
            jsonExpression = await pyRestApiHelper.ScrambleExpression(a[1], maxVar);
            a[1] = jsonExpression.Expression;
            equation.Equation = "dydx * (" + a[1].Split("==")[0] + ")" + ")" + "=" + a[0].Split("==")[0] + ")";
            return equation;
        }
        private IEquation MapJsonToDto(PyEquationJson equationJson, int equationType)
        {
            if (equationType == 1)
            {
                SeparableVariables sv = new SeparableVariables();
                sv.Equation = equationJson.LeftSide + " = " + equationJson.RightSide;
                sv.EquationLatex = equationJson.EquationLatex;
                sv.Solution = equationJson.Solution;
                sv.SolutionLatex = equationJson.SolutionLatex;
                return sv;
            }
            else if (equationType == 2)
            {
                Homogeneous hg = new Homogeneous();
                hg.Equation = equationJson.LeftSide + " = " + equationJson.RightSide;
                hg.EquationLatex = equationJson.EquationLatex;
                hg.Solution = equationJson.Solution;
                hg.SolutionLatex = equationJson.SolutionLatex;
                return hg;
            }
            return null;
        }
    }
}
