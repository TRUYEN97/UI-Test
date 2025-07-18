using System;
using System.Collections.Generic;
using UiTest.Model.Function;
using UiTest.Model.Interface;

namespace UiTest.Model.Cell
{
    public class TestResultModel : ITestResult
    {
        public TestResultModel()
        {
            FunctionDatas = new List<FunctionData>();
        }

        public List<FunctionData> FunctionDatas { get; private set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public string FinalStopTime { get; set; }
        public string CellName { get; set; }
        public string MAC { get; set; }
        public string ErrorCode { get; set; }
        public string FinalErrorCode { get; set; }
        public string Mode { get; set; }
        public string Product { get; set; }
        public string Station { get; set; }
        public string PcName { get; set; }
        public string Result { get; set; }
        public string FinalResult { get; set; }
        public string CycleTime { get; set; }
        public string FinalCycleTime { get; set; }

    }
}
