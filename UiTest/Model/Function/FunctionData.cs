using System;
using System.Globalization;
using Newtonsoft.Json.Linq;
using UiTest.Common;
using UiTest.Model.Cell;
using UiTest.Service.Logger;

namespace UiTest.Model.Function
{
    public class FunctionData
    {
        public readonly string name;
        public readonly string functionName;
        public readonly MyLogger logger;
        public readonly FunctionResultModel resultModel;
        public readonly CellData cellData;
        private DateTime startTime;
        private DateTime stopTime;
        private string tempErrorCode;

        public FunctionData(string name, string functionName, CellData cellData)
        {
            this.name = name;
            this.functionName = functionName;
            this.cellData = cellData;
            logger = new MyLogger();
            resultModel = new FunctionResultModel();
        }
        public bool IsTested => stopTime != default;
        public bool IsPassed => Result == TestStatus.PASSED;
        public bool IsCancel => Result == TestStatus.CANCEL;
        public bool IsFailed => Result == TestStatus.FAILED;
        public string ErrorCode  => resultModel.ErrorCode;
        public double CycleTime => startTime == null ? 0 : (DateTime.Now - startTime).TotalSeconds;
        public (TestStatus status, string value) TestResult { get; internal set; }
        public TestStatus Result { get; private set; }

        public void SetTempErrorCode(string errorCode)
        {
            tempErrorCode = errorCode;
        }
        public override string ToString()
        {
            return $"{name}-{functionName}";
        }
        public void TurnInit(int times)
        {
            stopTime = default;
            resultModel.StopTime = string.Empty;
            resultModel.LowerLimit = string.Empty;
            resultModel.UpperLimit = string.Empty;
            resultModel.Spec = string.Empty;
            resultModel.Value = string.Empty;
            resultModel.ErrorCode = string.Empty;
            resultModel.Result = string.Empty;
            tempErrorCode = string.Empty;
            TestResult = (TestStatus.FAILED, "");
            if (times == 0)
            {
                logger.AddLog("----------------------[Begin]----------------------");
            }
            else
            {
                logger.AddLog($"---------------------[Retry-{times}]---------------------");
            }
        }
        public void Start()
        {
            startTime = DateTime.Now;
            stopTime = default;
            resultModel.StartTime = startTime.ToString("o", CultureInfo.InvariantCulture);
            resultModel.StopTime = string.Empty;
            resultModel.Result = TestStatus.FAILED.ToString();
            resultModel.CycleTime = 0;
            resultModel.StopTime = string.Empty;
            resultModel.LowerLimit = string.Empty;
            resultModel.UpperLimit = string.Empty;
            resultModel.Spec = string.Empty;
            resultModel.Value = string.Empty;
            tempErrorCode = string.Empty;
            TestResult = (TestStatus.FAILED, "");
            cellData.AddFuntionData(this);
        }

        public void End()
        {
            stopTime = DateTime.Now;
            resultModel.StopTime = stopTime.ToString("o", CultureInfo.InvariantCulture);
            resultModel.CycleTime = (stopTime - startTime).TotalSeconds;
        }

        public void SetResult(TestStatus status)
        {
            Result = status;
            if (status == TestStatus.FAILED)
            {
                var errorCodeMapper = cellData.errorCodeMapper;
                if (!string.IsNullOrWhiteSpace(tempErrorCode))
                {
                    resultModel.ErrorCode = errorCodeMapper.MakeUp(tempErrorCode);
                }
                else
                if (errorCodeMapper.TryGetErrorcode(name, out var errorcode))
                {
                    resultModel.ErrorCode = errorcode;
                }
                else
                {
                    resultModel.ErrorCode = "UNKNFF";
                }
            }
            resultModel.Result = status.ToString();
            cellData.TestData.ReCheck(this, status);
        }
    }
}
