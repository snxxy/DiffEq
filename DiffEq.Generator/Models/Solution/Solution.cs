using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffEq.Generator.Models.Solution
{
    public class Solution
    {
        public string UserSolution { get; set;  }
        public string RealSolution { get; set; }
        public bool IsUserSolutionTrue { get; set; }

        public Solution(string userSolution, string realSolution)
        {
            UserSolution = userSolution;
            RealSolution = realSolution;
        }

        public Solution()
        {
        }
    }
}
