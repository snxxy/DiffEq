using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DiffEq.Generator.Models;
using DiffEq.Generator.Services;
using DiffEq.Web.Models;
using System.Threading.Tasks;
using System;

namespace DiffEq.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        // GET: api/<controller>
        [Route("[action]/")]
        [HttpGet]
        public async Task<IEnumerable<int>> GetEquationCount()
        {
            var dbService = new EquationDBService();
            var result = await dbService.GetEquationCounts();
            return result;
        }

        [HttpPost]
        public async Task GenerateEquationOrder([FromBody] GenerateOrderRequest order)
        {
            var generator = new EquationGeneratorService();
            await generator.GenerationOrder(new Dictionary<int, int>() { { 1, Convert.ToInt32(order.Sveq) }, { 2, Convert.ToInt32(order.Hgeq) } });
        }

        [Route("[action]/")]
        [HttpGet]
        public async Task<IEnumerable<IEquation>> GetEquationsToSolve()
        {
            var generator = new EquationDBService();
            var result = await generator.GetEquations(new Dictionary<int, int>() { { 1, 2 }, { 2, 2 } });
            return result;
        }
    }

}