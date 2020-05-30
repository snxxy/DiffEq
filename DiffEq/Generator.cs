using System;
using System.Collections.Generic;
using EquationDB;
using DiffEq.Models;
using DiffEq.Strats;
using System.Linq;
using System.Threading.Tasks;

namespace DiffEq
{
    public class Generator
    {
        private Random ran = new Random();
        private List<IEquation> Cache = new List<IEquation>();

        public async Task<IEnumerable<IEquation>> GetEquations(Dictionary<int, int> pairs)
        {
            DBManager manager = new DBManager();
            var type1res = await manager.GetEquationList(2, 1);
            var type2res = await manager.GetEquationList(2, 2);
            var result = MapDalToDto((type1res).Union(type2res));
            return result;
        }
        public async Task GenerateOrder(Dictionary<int, int> pairs, bool useDb = true)
        {
            foreach (var order in pairs)
            {
                for (int i = 0; i <= order.Value; i++)
                {
                    if (order.Key == 1)
                    {
                        Dispatch(new SeparableVariables());
                    }
                    else if (order.Key == 2)
                    {
                        Dispatch(new Homogeneous());
                    }
                    else
                    {
                        throw new NotImplementedException($"Equations of type {order.Key} are not supported");
                    }
                }
            }
            if (useDb)
            {
                var forDB = MapDtoToDal(Cache);
                var manager = new DBManager();
                await manager.AddToDB(forDB);
                Cache.Clear();
            }
        }
        public async Task<IEnumerable<int>> GetEquationCounts()
        {
            //TODO: get types from global config/remove hardcode
            var result = new List<int>();
            result.Add(await GetEquationCountByType(1));
            result.Add(await GetEquationCountByType(2));
            return result;
        }
        private IEnumerable<Equation> MapDtoToDal(IEnumerable<IEquation> eqs)
        {
            var result = new List<Equation>();
            foreach (var item in eqs)
            {
                var eqDal = new Equation();
                eqDal.Eq = item.Equation;
                eqDal.Latex = item.EquationLatex;
                eqDal.Solution = item.Solution;
                eqDal.SolutionLatex = item.SolutionLatex;
                eqDal.Type = item.Type;
                result.Add(eqDal);
            }
            return result;
        }
        private async Task<int> GetEquationCountByType(int type)
        {
            var manager = new DBManager();
            var result = await manager.CountByType(type);
            return result;
        }
        private void Dispatch(IEquation equation)
        {
            dynamic _equation = equation;
            dynamic genResult = Generate(_equation);
            Cache.Add(genResult);
        }
        private async Task<IEquation> Generate(SeparableVariables sv)
        {
            RandomFunctionGenerator generator = new RandomFunctionGenerator(new SeparableTreeStrategy());
            EquationManager equationManager = new EquationManager();
            sv.Equation = "(" + generator.Generate("x", ran.Next(4, 10)) + ")" + "*" + "(" + generator.Generate("y", ran.Next(4, 10)) + ")" + "=" + "dydx";
            sv =  (SeparableVariables)await equationManager.SolveAndScramble(sv);
            return sv;
        }
        private async Task<IEquation> Generate(Homogeneous hg)
        {
            RandomFunctionGenerator generator = new RandomFunctionGenerator(new HomogeneousTreeStrategy());
            EquationManager equationManager = new EquationManager();
            hg.Equation = "(" + generator.Generate("y/x", (1 + 2 * ran.Next(1, 2))) + ")" + "/" + "(" + generator.Generate("y/x", (1 + 2 * ran.Next(1, 2))) + ")" + "=" + "dydx";
            hg = (Homogeneous)await equationManager.SolveAndScramble(hg);
            return hg;
        }
        //needs less hardcode
        private IEnumerable<IEquation> MapDalToDto(IEnumerable<Equation> equationDal)
        {
            var result = new List<IEquation>();
            foreach (var item in equationDal)
            {
                if (item.Type == 1)
                {
                    var eqDto = new SeparableVariables();
                    eqDto.Equation = item.Eq;
                    eqDto.EquationLatex = item.Latex;
                    eqDto.Solution = item.Solution;
                    eqDto.SolutionLatex = item.SolutionLatex;
                    result.Add(eqDto);
                }
                else if (item.Type == 2)
                {
                    var eqDto = new Homogeneous();
                    eqDto.Equation = item.Eq;
                    eqDto.EquationLatex = item.Latex;
                    eqDto.Solution = item.Solution;
                    eqDto.SolutionLatex = item.SolutionLatex;
                    result.Add(eqDto);
                }
                else
                {
                    throw new NotImplementedException("No mapper for this equation type");
                }
            }
            return result;
        }
    }
}
