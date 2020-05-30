using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiffEq;
using DiffEq.Models;
using DiffEqWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiffEqWeb.Controllers
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
            var generator = new Generator();
            var result = await generator.GetEquationCounts();
            return result;
        }

        [HttpPost]
        public async Task GenerateEquationOrder([FromBody]GenerateOrderRequest order)
        {
            var generator = new Generator();
            await generator.GenerateOrder(new Dictionary<int, int>() { { 1, Convert.ToInt32(order.Sveq) }, { 2, Convert.ToInt32(order.Hgeq)}});
        }

        [Route("[action]/")]
        [HttpGet]
        public async Task<IEnumerable<IEquation>> GetEquationsToSolve()
        {
            var generator = new Generator();
            var result = await generator.GetEquations(new Dictionary<int, int>() { { 1, 2 }, { 2, 2 } });
            return result;
        }
    }
}
