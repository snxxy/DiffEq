using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffEq.Generator.GenerationConfigs
{
    interface IGenerationConfig
    {
        public List<string> Operators { get; }
        public List<string> Trigonometry { get; }
        public int MaxPow { get; }
        public bool IsTrigonometryEnabled { get; }
        public bool IsVariablePowEnabled { get; }
    }
}
