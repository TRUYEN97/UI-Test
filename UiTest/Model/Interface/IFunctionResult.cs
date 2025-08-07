using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.Model.Interface
{
    public interface IFunctionResult
    {
        string Name { get;}
        string StartTime { get; }
        string StopTime { get; }
        string Value { get; }
        string Result { get; }
        double CycleTime { get; }
        string UpperLimit { get; }
        string LowerLimit { get; }
        string Spec { get; }
        string ErrorCode { get; }
    }
}
