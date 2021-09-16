using DiffEq.Generator.Handlers;
using DiffEq.Generator.Misc;
using DiffEq.Generator.Models;
using DiffEq.Generator.Strats;
using System;
using System.Threading.Tasks;

namespace DiffEq.Generator.Managers
{
    class EquationDispatcher
    {
        public async Task<IEquation> Dispatch(IEquation equation)
        {
            dynamic _equation = equation;
            dynamic genResult = await Generate(_equation);
            return genResult;
        }

        private async Task<IEquation> Generate(SeparableVariables sv)
        {
            var ran = new Random();
            var generator = new RandomFunctionGenerator(new SeparableTreeStrategy());
            var equationHandler = new SolverHandler();
            sv.Equation = "(" + generator.Generate("x", ran.Next(4, 10)) + ")" + "*" + "(" + generator.Generate("y", ran.Next(4, 10)) + ")" + "=" + "dydx";
            sv = (SeparableVariables)await equationHandler.SolveAndScramble(sv);
            return sv;
        }

        private async Task<IEquation> Generate(Homogeneous hg)
        {
            var ran = new Random();
            var generator = new RandomFunctionGenerator(new HomogeneousTreeStrategy());
            var equationHandler = new SolverHandler();
            hg.Equation = "(" + generator.Generate("y/x", (1 + 2 * ran.Next(1, 2))) + ")" + "/" + "(" + generator.Generate("y/x", (1 + 2 * ran.Next(1, 2))) + ")" + "=" + "dydx";
            hg = (Homogeneous)await equationHandler.SolveAndScramble(hg);
            return hg;
        }
    }
}
