
using UiTest.Common;
using UiTest.Functions.TestFunctions.Config;
using UiTest.Model.Function;

namespace UiTest.Functions.TestFunctions
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

        public bool CheckResultTest()
        {
            try
            {
                var (status, value) = functionData.TestResult;
                switch (status)
                {
                    case TestStatus.PASSED:
                        return CheckPassStatus(value);
                    case TestStatus.FAILED:
                        return SetFail();
                    case TestStatus.CANCEL:
                        return SetCancel();
                    default:
                        return SetFail();
                }
            }
            finally
            {
                SetResultValue();
                AddResultLog();
            }
        }

        private bool CheckPassStatus(string result)
        {
            if (!string.IsNullOrEmpty(config.Spec))
            {
                if (result == config.Spec)
                {
                    return SetPass();
                }
                else
                {
                    return SetFail();
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
                        return SetFail();
                    }
                }
                return SetPass();
            }
        }

        private bool SetCancel()
        {
            IsAcceptable = true;
            functionData.SetResult(TestStatus.CANCEL);
            return true;
        }

        private bool SetFail()
        {
            IsAcceptable = false;
            functionData.SetResult(TestStatus.FAILED);
            return false;
        }
        private bool SetPass()
        {
            IsAcceptable = true;
            functionData.SetResult(TestStatus.PASSED);
            return true;
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
            logger.AddLog("RESULT", $"Item name: {resultModel.Name}");
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
