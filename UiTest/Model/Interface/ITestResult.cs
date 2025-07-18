using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        string MAC { get; set; }
        string ErrorCode { get; set; }
        string FinalErrorCode { get; set; }
        string Mode { get; set; }
        string Result { get; set; }
        string FinalResult { get; set; }
        string CycleTime { get; set; }
        string FinalCycleTime { get; set; }
    }
}
