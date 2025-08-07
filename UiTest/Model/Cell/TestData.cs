
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UiTest.Common;
using UiTest.Config;
using UiTest.Model.Function;
using UiTest.Model.Interface;

namespace UiTest.Model.Cell
{
    public class TestData : ITestResult
    {
        private readonly List<FunctionData> _functionFailedDatas;
        private readonly TestResultModel _testResultModel;
        private readonly ProgramSetting programSetting;
        public DateTime StartDateTime { get; private set; }
        public DateTime StopDateTime { get; private set; }
        public TestData(string name)
        {
            _functionFailedDatas = new List<FunctionData>();
            _testResultModel = new TestResultModel();
            programSetting = ConfigLoader.ProgramConfig.ProgramSetting;
            CellName = name;
        }
        public void AddFuntionData(FunctionData functionData)
        {
            if (string.IsNullOrWhiteSpace(functionData?.name)) return;
            _testResultModel.FunctionDatas.Add(functionData);
        }
        public void AddFailedFuntionData(FunctionData functionData)
        {
            if (string.IsNullOrWhiteSpace(functionData?.name)) return;
            if (!functionData.IsPassed)
            {
                _functionFailedDatas.Add(functionData);
            }
        }

        public void Reset()
        {
            MAC = string.Empty;
            Input = string.Empty;
            Mode = string.Empty;
            StartTime = string.Empty;
            StopTime = string.Empty;
            FinalStopTime = string.Empty;
            ErrorCode = string.Empty;
            Result = TestStatus.STANDBY;
            FinalResult = TestStatus.STANDBY;
            CycleTime = 0;
            FinalCycleTime = 0;
            _functionFailedDatas.Clear();
            _testResultModel.FunctionDatas.Clear();
        }
        public void Start(string input, string modeName)
        {
            Product = programSetting.Product;
            Station = programSetting.Station;
            PcName = PcInfo.PcName;
            MAC = input;
            Input = input;
            Mode = modeName;
            StartDateTime = DateTime.Now;
            StartTime = StartDateTime.ToString("o", CultureInfo.InvariantCulture);
            StopTime = string.Empty;
            FinalStopTime = string.Empty;
            ErrorCode = string.Empty;
            Result = TestStatus.TESTING;
            FinalResult = TestStatus.TESTING;
            CycleTime = 0;
            FinalCycleTime = 0;
        }

        public void End()
        {
            StopDateTime = DateTime.Now;
            StopTime = StopDateTime.ToString("o", CultureInfo.InvariantCulture);
            Result = GetResult();
            FinalResult = Result;
            ErrorCode = FunctionFailedDatas.Count > 0 ? FunctionFailedDatas[0].ErrorCode : string.Empty;
            CycleTime = (StopDateTime - StartDateTime).TotalSeconds;
        }

        private TestStatus GetResult()
        {
            if (FunctionFailedDatas.Count > 0)
            {
                return TestStatus.FAILED;
            }
            else if (FunctionDatas.Count > 0 && FunctionDatas.Any(f => !f.IsCancel))
            {
                return TestStatus.PASSED;
            }
            else
            {
                return TestStatus.CANCEL;
            }
        }

        public void EndProcess()
        {
            var dtNow = DateTime.Now;
            FinalStopTime = dtNow.ToString("o", CultureInfo.InvariantCulture);
            FinalResult = GetResult();
            FinalCycleTime = (dtNow - StartDateTime).TotalSeconds;
        }

        public void ReCheck(FunctionData functionData, TestStatus status)
        {
            if (functionData == null) return;
            switch (status)
            {
                case TestStatus.FAILED:
                    if (!_functionFailedDatas.Contains(functionData))
                    {
                        _functionFailedDatas.Add(functionData);
                    }
                    break;
                case TestStatus.CANCEL:
                case TestStatus.PASSED:
                    if (_functionFailedDatas.Contains(functionData))
                    {
                        _functionFailedDatas.Remove(functionData);
                    }
                    break;
            }
        }

        public List<FunctionData> FunctionDatas => new List<FunctionData>(_testResultModel.FunctionDatas);
        public List<FunctionData> FunctionFailedDatas => new List<FunctionData>(_functionFailedDatas);
        public string StartTime { get => _testResultModel.StartTime; private set => _testResultModel.StartTime = value; }
        public string StopTime { get => _testResultModel.StopTime; private set => _testResultModel.StopTime = value; }
        public string FinalStopTime { get => _testResultModel.FinalStopTime; private set => _testResultModel.FinalStopTime = value; }
        public string CellName { get => _testResultModel.CellName; private set => _testResultModel.CellName = value; }
        public string Product { get => _testResultModel.Product; private set => _testResultModel.Product = value; }
        public string Station { get => _testResultModel.Station; private set => _testResultModel.Station = value; }
        public string PcName { get => _testResultModel.PcName; private set => _testResultModel.PcName = value; }
        public string MAC { get => _testResultModel.MAC; private set => _testResultModel.MAC = value; }
        public string Input { get => _testResultModel.Input; private set => _testResultModel.Input = value; }
        public string Mode { get => _testResultModel.Mode; private set => _testResultModel.Mode = value; }
        public string ErrorCode { get => _testResultModel.ErrorCode; private set => _testResultModel.ErrorCode = value; }
        public TestStatus Result { get => _testResultModel.Result; private set => _testResultModel.Result = value; }
        public TestStatus FinalResult { get => _testResultModel.FinalResult; private set => _testResultModel.FinalResult = value; }
        public double CycleTime { get => _testResultModel.CycleTime; private set => _testResultModel.CycleTime = value; }
        public double FinalCycleTime { get => _testResultModel.FinalCycleTime; private set => _testResultModel.FinalCycleTime = value; }
    }
}
