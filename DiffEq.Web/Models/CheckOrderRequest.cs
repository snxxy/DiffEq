using DiffEq.Generator.Models.Solution;
using System.Collections.Generic;

namespace DiffEq.Web.Models
{
    public class CheckOrderRequest
    {
        public IEnumerable<Solution> solutions { get; set; }
    }
}
