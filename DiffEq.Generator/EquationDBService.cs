using DiffEq.Generator.Handlers;
using DiffEq.Generator.Models;

namespace DiffEq.Generator.Services
{
    public class EquationDBService
    {
        public async Task<IEnumerable<int>> GetEquationCounts()
        {
            var handler = new DBHandler();
            var result = await handler.GetEquationCounts();
            return result;
        }

        public async Task<int> GetEquationCountByType(int type)
        {
            var handler = new DBHandler();
            var result = await handler.GetEquationCountByType(type);
            return result;
        }

        public async Task AddEquationsToDB(IEnumerable<IEquation> eqs)
        {
            var handler = new DBHandler();
            await handler.AddEquationsToDB(eqs);
        }

        public async Task<IEnumerable<IEquation>> GetEquations(Dictionary<int, int> pairs)
        {
            var handler = new DBHandler();
            var result = await handler.GetEquations(pairs);
            return result;
        }
    }
}
