using DiffEq.Generator.Models;
using DiffEq.Generator.Managers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace DiffEq.Generator.Services
{
    public class EquationGeneratorService
    {
        public async Task<int> GenerationOrder(Dictionary<int, int> pairs, bool useDb = true)
        {
            var Cache = new List<IEquation>();
            var dispatcher = new EquationDispatcher();
            try
            {
                foreach (var order in pairs)
                {
                    for (int i = 0; i <= order.Value; i++)
                    {
                        if (order.Key == 1)
                        {
                            var result = await dispatcher.Dispatch(new SeparableVariables());
                            Cache.Add(result);
                        }
                        else if (order.Key == 2)
                        {
                            var result = await dispatcher.Dispatch(new Homogeneous());
                            Cache.Add(result);
                        }
                        else
                        {
                            throw new NotImplementedException($"Equations of type {order.Key} are not supported");
                        }
                    }
                }
                if (useDb)
                {
                    var dbService = new EquationDBService();
                    await dbService.AddEquationsToDB(Cache);
                }
                return Cache.Count();
            }
            catch (NotImplementedException e)
            {
                //log e
                //refactor returns/null objects
                return -1;
            }
        }
    }
}
