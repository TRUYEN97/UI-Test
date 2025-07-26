using System;
using System.Globalization;
using UiTest.Common;
using UiTest.Service.Logger;

namespace UiTest.Model.Function
{
    public class FunctionData
    {
        public readonly string name;
        public readonly string functionName;
        public readonly MyLogger logger;
        public readonly FunctionResultModel resultModel;
        private DateTime startTime;
        private DateTime stopTime;
        public FunctionData(string name, string functionName)
        {
            this.name = name;
            this.functionName = functionName;
            logger = new MyLogger();
            resultModel = new FunctionResultModel();
        }
        public bool IsPass => resultModel.Result == TestStatus.PASSED.ToString();

        public double CycleTime => startTime == null ? 0 : (DateTime.Now - startTime).TotalSeconds;

        public (ItemStatus status, string value) TestResult { get; internal set; }

        public override string ToString()
        {
            return $"{name}-{functionName}";
        }

        public void TurnInit()
        {
            resultModel.StopTime = string.Empty;
            resultModel.LowerLimit = string.Empty;
            resultModel.UpperLimit = string.Empty;
            resultModel.Spec = string.Empty;
            resultModel.Value = string.Empty;
            TestResult = (ItemStatus.FAILED, "");
        }

        public void Start()
        {
            startTime = DateTime.Now;
            resultModel.StartTime = startTime.ToString("o", CultureInfo.InvariantCulture);
            resultModel.Result = ItemStatus.FAILED.ToString();
            resultModel.CycleTime = 0;
            resultModel.StopTime = string.Empty;
            resultModel.LowerLimit = string.Empty;
            resultModel.UpperLimit = string.Empty;
            resultModel.Spec = string.Empty;
            resultModel.Value = string.Empty;
            TestResult = (ItemStatus.FAILED, "");
        }

        public void End()
        {
            stopTime = DateTime.Now;
            resultModel.StopTime = stopTime.ToString("o", CultureInfo.InvariantCulture);
            resultModel.CycleTime = (stopTime - startTime).TotalSeconds;
        }
    }
}
