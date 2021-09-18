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

        [Route("[action]/")]
        [HttpPost]
        public async Task GenerateEquationOrder(GenerateOrderRequest order)
        {
            var generator = new EquationGeneratorService();
            await generator.GenerationOrder(new Dictionary<int, int>() { { 1, order.Sveq }, { 2, order.Hgeq } });
        }

        [Route("[action]/")]
        [HttpPost]
        public async Task<IEnumerable<IEquation>> GetEquationsToSolve(GenerateOrderRequest order)
        {
            var dBService = new EquationDBService();
            var result = await dBService.GetEquations(new Dictionary<int, int>() { { 1, order.Sveq }, { 2, order.Hgeq } });
            return result;
        }
    }

}