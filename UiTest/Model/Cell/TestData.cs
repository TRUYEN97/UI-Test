
using System;
using System.Collections.Generic;
using UiTest.Model.Function;
using UiTest.Model.Interface;

namespace UiTest.Model.Cell
{
    public class TestData : ITestResult
    {
        private readonly List<FunctionData> _functionFailedDatas;
        private readonly TestResultModel _testResultModel;
        public TestData(string name)
        {
            _functionFailedDatas = new List<FunctionData>();
            _testResultModel = new TestResultModel();
            CellName = name;
        }
        public void AddFuntionData(FunctionData functionData)
        {
            if (string.IsNullOrWhiteSpace(functionData?.Name)) return;
            _testResultModel.FunctionDatas.Add(functionData);
            if (!functionData.IsPass)
            {
                _functionFailedDatas.Add(functionData);
            }
        }
        public List<FunctionData> FunctionDatas => new List<FunctionData>(_testResultModel.FunctionDatas);
        public List<FunctionData> FunctionFailedDatas => new List<FunctionData>(_functionFailedDatas);

        public bool IsTested { get; set; }
        public string StartTime { get => _testResultModel.StartTime; private set => _testResultModel.StartTime = value; }
        public string StopTime { get => _testResultModel.StopTime; private set => _testResultModel.StopTime = value; }
        public string FinalStopTime { get => _testResultModel.FinalStopTime; private set => _testResultModel.FinalStopTime = value; }
        public string CellName { get => _testResultModel.CellName; private set => _testResultModel.CellName = value; }
        public string Product { get => _testResultModel.Product; set => _testResultModel.Product = value; }
        public string Station { get => _testResultModel.Station; set => _testResultModel.Station = value; }
        public string PcName { get => _testResultModel.PcName; set => _testResultModel.PcName = value; }
        public string MAC { get => _testResultModel.MAC; set => _testResultModel.MAC = value; }
        public string Mode { get => _testResultModel.Mode; set => _testResultModel.Mode = value; }
        public string ErrorCode { get => _testResultModel.ErrorCode; set => _testResultModel.ErrorCode = value; }
        public string FinalErrorCode { get => _testResultModel.FinalErrorCode; set => _testResultModel.FinalErrorCode = value; }
        public string Result { get => _testResultModel.Result; set => _testResultModel.Result = value; }
        public string FinalResult { get => _testResultModel.FinalResult; set => _testResultModel.FinalResult = value; }
        public string CycleTime { get => _testResultModel.CycleTime; set => _testResultModel.CycleTime = value; }
        public string FinalCycleTime { get => _testResultModel.FinalCycleTime; set => _testResultModel.FinalCycleTime = value; }

        public void Reset()
        {
            Result = null; MAC = null; ErrorCode = null; IsTested = false; Mode = null; FinalCycleTime = null;
        }
    }
}
