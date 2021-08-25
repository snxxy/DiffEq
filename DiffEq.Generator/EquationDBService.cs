using DiffEq.Generator.Handlers;
using DiffEq.Generator.Models;

namespace DiffEq.Generator.Services
{
    public class EquationDBService
    {
        public async Task<IEnumerable<int>> GetEquationCount()
        {
            //TODO: get types from global config/remove hardcode
            var result = new List<int>();
            result.Add(await GetEquationCountByType(1));
            result.Add(await GetEquationCountByType(2));
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
