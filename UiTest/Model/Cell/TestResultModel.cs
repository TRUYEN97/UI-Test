using System;
using System.Collections.Generic;
using UiTest.Common;
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
        public string INPUT { get; set; }
        public string ErrorCode { get; set; }
        public string Mode { get; set; }
        public string Product { get; set; }
        public string Station { get; set; }
        public string PcName { get; set; }
        public TestStatus Result { get; set; }
        public TestStatus FinalResult { get; set; }
        public double CycleTime { get; set; }
        public double FinalCycleTime { get; set; }

    }
}
