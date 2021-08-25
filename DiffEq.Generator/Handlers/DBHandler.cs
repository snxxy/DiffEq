using DiffEq.DB;
using DiffEq.Generator.Models;

namespace DiffEq.Generator.Handlers
{
    class DBHandler
    {
        public async Task<IEnumerable<int>> GetEquationCounts()
        {
            var db = new DiffEqDB();
            var result = await db.GetEquationCounts();
            return result;
        }

        public async Task<int> GetEquationCountByType(int type)
        {
            var db = new DiffEqDB();
            var result = await db.CountByType(type);
            return result;
        }

        public async Task AddEquationsToDB(IEnumerable<IEquation> eqs)
        {
            var db = new DiffEqDB();
            var forDb = MapDtoToDao(eqs);
            await db.AddToDB(forDb);
        }

        public async Task<IEnumerable<IEquation>> GetEquations(Dictionary<int, int> pairs)
        {
            var db = new DiffEqDB();
            var type1res = await db.GetEquationList(2, 1);
            var type2res = await db.GetEquationList(2, 2);
            var result = MapDaoToDto((type1res).Union(type2res));
            return result;
        }

        private IEnumerable<IEquation> MapDaoToDto(IEnumerable<Equation> equationDal)
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

        private IEnumerable<Equation> MapDtoToDao(IEnumerable<IEquation> eqs)
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
    }
}
