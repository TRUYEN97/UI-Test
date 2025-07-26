using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.Functions.Config
{
    public abstract class BasefunctionConfig
    {
        public int Retry { get; set; }
        public int TimeOut { get; set; } = int.MaxValue;
        public bool IsSkipFailure { get; set; } = false;
        public string Spec { get; set; }
        public string UpperLimit { get; set; }
        public string LowerLimit { get; set; }
    }
}
