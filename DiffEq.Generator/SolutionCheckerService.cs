using DiffEq.Generator.Handlers;
using DiffEq.Generator.Models.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffEq.Generator.Services
{
    public class SolutionCheckerService
    {
        public async Task<IEnumerable<bool>> GetSolutionsChecked(IEnumerable<Solution> solutions)
        {
            var solutionHandler = new SolutionHandler();
            var result = await solutionHandler.GetSolutionsChecked(solutions);
            return result;
        }
    }
}
