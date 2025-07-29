using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.Functions.Config
{
    public abstract class BasefunctionConfig
    {
        public bool IsCancelInDebugMode { get; set; } = false;
        public bool IsSkipFailure { get; set; } = false;
        public bool IsMultiTask { get; set; } = false;
        public bool IsDontWaitForMe { get; internal set; } = false;
        public int Retry { get; set; } = 0;
        public int TimeOut { get; set; } = int.MaxValue;
        public string Spec { get; set; } = string.Empty;
        public string UpperLimit { get; set; } = string.Empty;
        public string LowerLimit { get; set; } = string.Empty;
    }
}
