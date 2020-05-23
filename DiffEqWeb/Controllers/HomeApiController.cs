using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiffEq;
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
        [HttpGet]
        public async Task<IEnumerable<int>> GetEquationCount()
        {
            var generator = new Generator();
            var result = generator.GetEquationCounts();
            return result;
        }

        [HttpPost]
        public async Task GenerateEquationOrder([FromBody]GenerateOrderRequest order)
        {
            var generator = new Generator();
            generator.GenerateOrder(new Dictionary<int, int>() { { 1, Convert.ToInt32(order.Sveq) }, { 2, Convert.ToInt32(order.Hgeq)}});
        }
    }
}
