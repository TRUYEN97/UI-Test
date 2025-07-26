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
                    case ItemStatus.PASSED:
                        CheckPassStatus(value);
                        break;
                    case ItemStatus.FAILED:
                        SetFail();
                        break;
                    case ItemStatus.CANCEL:
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
            if (!IsLimitAvailable)
            {
                SetPass();
            }
            else
            {
                var resultModel = functionData.resultModel;

                if (!string.IsNullOrEmpty(config.Spec))
                {
                    if (result != null)
                    {
                        if (config.Spec == $"{result}")
                        {
                            IsAcceptable = true;
                            resultModel.Result = ItemStatus.PASSED.ToString();
                            return;
                        }
                    }
                    SetFail();
                }
                else if (double.TryParse($"{result}", out var value))
                {

                }
                else
                {
                    SetFail();
                }
            }
        }

        private bool IsLimitAvailable => config != null &&
                (!string.IsNullOrWhiteSpace(config.Spec)
                || !string.IsNullOrWhiteSpace(config.UpperLimit)
                || !string.IsNullOrWhiteSpace(config.LowerLimit));

        private void SetCancel()
        {
            IsAcceptable = true;
            functionData.resultModel.Result = ItemStatus.CANCEL.ToString();
        }

        private void SetFail()
        {
            IsAcceptable = false;
            functionData.resultModel.Result = ItemStatus.FAILED.ToString();
        }
        private void SetPass()
        {
            IsAcceptable = true;
            functionData.resultModel.Result = ItemStatus.PASSED.ToString();
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
            logger.AddLog("RESULT", $"Upper limit: {resultModel.UpperLimit}");
            logger.AddLog("RESULT", $"Lower limit: {resultModel.UpperLimit}");
            logger.AddLog("RESULT", $"Value: {resultModel.Value}");
            logger.AddLog("RESULT", $"Result: {resultModel.Result}");
            logger.AddLog("RESULT", $"Errorcode: {resultModel.ErrorCode}");
            logger.AddLog("***************************************************");
        }
    }
}
