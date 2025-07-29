using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiTest.Common;
using UiTest.Functions.Config;
using UiTest.Model.Function;
using UiTest.Model.Interface;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace UiTest.Functions
{
    public class FunctionAnalysis
    {
        private readonly FunctionData functionData;
        private readonly BasefunctionConfig config;
        public FunctionAnalysis(FunctionData functionData, BasefunctionConfig config)
        {
            this.functionData = functionData;
            this.config = config;
            IsAcceptable = false;
        }

        public bool IsAcceptable { get; private set; }

        public void CheckResultTest()
        {
            try
            {
                var (status, value) = functionData.TestResult;
                switch (status)
                {
                    case TestStatus.PASSED:
                        CheckPassStatus(value);
                        break;
                    case TestStatus.FAILED:
                        SetFail();
                        break;
                    case TestStatus.CANCEL:
                        SetCancel();
                        break;
                    default:
                        SetFail();
                        break;
                }
            }
            finally
            {
                SetResultValue();
                AddResultLog();
            }
        }

        private void CheckPassStatus(string result)
        {
            if (!string.IsNullOrEmpty(config.Spec))
            {
                if (result == config.Spec)
                {
                    SetPass();
                }
                else
                {
                    SetFail();
                }
            }
            else
            {
                bool isHasUpper = double.TryParse(config.UpperLimit, out var upperLimit);
                bool isHasLower = double.TryParse(config.LowerLimit, out var lowerLimit);
                if (isHasUpper || isHasLower)
                {
                    if (!double.TryParse(result, out var value)
                        || (isHasUpper && value > upperLimit)
                        || (isHasLower && value < lowerLimit))
                    {
                        SetFail();
                    }
                }
                SetPass();
            }
        }

        private void SetCancel()
        {
            IsAcceptable = true;
            functionData.SetResult(TestStatus.CANCEL);
        }

        private void SetFail()
        {
            IsAcceptable = false;
            functionData.SetResult(TestStatus.FAILED);
        }
        private void SetPass()
        {
            IsAcceptable = true;
            functionData.SetResult(TestStatus.PASSED);
        }

        private void SetResultValue()
        {
            var resultModel = functionData.resultModel;
            resultModel.UpperLimit = config.UpperLimit;
            resultModel.LowerLimit = config.LowerLimit;
            resultModel.Spec = config.Spec;
        }
        private void AddResultLog()
        {
            var logger = functionData.logger;
            var resultModel = functionData.resultModel;
            logger.AddLog("***************************************************");
            logger.AddLog("RESULT", $"Item name: {functionData}");
            logger.AddLog("RESULT", $"Value: {resultModel.Value}");
            logger.AddLog("RESULT", $"Result: {resultModel.Result}");
            logger.AddLog("RESULT", $"Errorcode: {resultModel.ErrorCode}");
            logger.AddLog("RESULT", $"Upper limit: {resultModel.UpperLimit}");
            logger.AddLog("RESULT", $"Lower limit: {resultModel.LowerLimit}");
            logger.AddLog("RESULT", $"Spec: {resultModel.Spec}");
            logger.AddLog("***************************************************");
        }
    }
}
