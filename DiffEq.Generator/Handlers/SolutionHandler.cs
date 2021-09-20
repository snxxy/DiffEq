using DiffEq.Generator.Models.JSON;
using DiffEq.Generator.Models.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffEq.Generator.Handlers
{
    public class SolutionHandler
    {
        public async Task<IEnumerable<bool>> GetSolutionsChecked(IEnumerable<Solution> solutions)
        {
            try
            {
                if (solutions == null || solutions.Count() == 0)
                {
                    throw new ArgumentException("Solutions is empty");
                }
                foreach (var item in solutions)
                {
                    if (item.UserSolution == null || item.RealSolution == null)
                    {
                        throw new ArgumentException("Solution items contain null fields");
                    }
                }
            }
            catch (ArgumentException e)
            {
                //log e
                return null;
            }

            IEnumerable<Solution> resultSols = solutions;
            foreach (var item in resultSols)
            {
                item.RealSolution = TrimJaxJunkFromString(item.RealSolution);
            }
            resultSols = await CheckSolution(resultSols);
            var result = new List<bool>();
            foreach (var item in resultSols)
            {
                result.Add(item.IsUserSolutionTrue);
            }
            return result;
        }

        private string TrimJaxJunkFromString(string inputString)
        {
            var result = inputString.Substring(inputString.IndexOf('_') + 5);
            result = result.Remove(result.Length - 1);
            return result;
        }

        private async Task<IEnumerable<Solution>> CheckSolution(IEnumerable<Solution> inputSolutions)
        {
            var pyHandler = new PyRestApiHandler();
            var result = new List<Solution>();
            foreach (var item in inputSolutions)
            {
                result.Add(await pyHandler.CheckExpressions(item));
            }           
            return result;
        }
    }
}
