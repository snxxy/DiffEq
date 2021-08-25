using DiffEq.Generator.Models;
using DiffEq.Generator.Models.JSON;

namespace DiffEq.Generator.Handlers
{
    sealed class SolverHandler
    {
        private int maxVar = 0;
        public async Task<IEquation> SolveAndScramble(IEquation equation)
        {
            maxVar = 0;
            var pyRestApiHelper = new PyRestApiHandler();
            var jsonEquation = MapDtoToJson(equation);

            if (equation.Type == 1)
            {
                var solvedJsonEquation = await pyRestApiHelper.SolveEquation(jsonEquation);
                var resultEquation = MapJsonToDto(solvedJsonEquation, equation.Type);

                return resultEquation;
            }
            else if (equation.Type == 2)
            {
                var solvedJsonEquation = await pyRestApiHelper.SolveEquation(jsonEquation);
                var resultEquation = MapJsonToDto(solvedJsonEquation, equation.Type);

                //fix scramble
                //equation = Scramble(equation);

                return equation;
            }
            else
            {
                throw new NotImplementedException($"equationType {equation.Type} is not supported for mapping");
            }
        }

        private async Task<IEquation> Scramble(IEquation equation)
        {
            var pyRestApiHelper = new PyRestApiHandler();

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
                var sv = new SeparableVariables();
                sv.Equation = equationJson.LeftSide + " = " + equationJson.RightSide;
                sv.EquationLatex = equationJson.EquationLatex;
                sv.Solution = equationJson.Solution;
                sv.SolutionLatex = equationJson.SolutionLatex;
                return sv;
            }
            else if (equationType == 2)
            {
                var hg = new Homogeneous();
                hg.Equation = equationJson.LeftSide + " = " + equationJson.RightSide;
                hg.EquationLatex = equationJson.EquationLatex;
                hg.Solution = equationJson.Solution;
                hg.SolutionLatex = equationJson.SolutionLatex;
                return hg;
            }
            else
            {
                throw new NotImplementedException($"equationType {equationType} is not supported for mapping");
            }
        }

        private PyEquationJson MapDtoToJson(IEquation dtoEquation)
        {
            var jsonEquation = new PyEquationJson();
            var sides = dtoEquation.Equation.Split('=');
            jsonEquation.LeftSide = sides[0];
            jsonEquation.RightSide = sides[1];
            return jsonEquation;
        }
    }
}
