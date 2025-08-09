using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiTest.Common;
using UiTest.Model.Function;

namespace UiTest.Model.Interface
{
    interface ITestResult
    {
        List<FunctionData> FunctionDatas { get; }
        string StartTime { get; }
        string StopTime { get; }
        string FinalStopTime { get; }
        string CellName { get; }
        string Product { get; }
        string Station { get; }
        string PcName { get; }
        string MAC { get; }
        string Input { get; }
        string ErrorCode { get; }
        string Mode { get; }
        TestResult Result { get; }
        TestResult FinalResult { get; }
        double CycleTime { get; }
        double FinalCycleTime { get; }
    }
}
