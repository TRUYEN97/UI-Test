using System;
using System.Threading;
using System.Threading.Tasks;
using UiTest.Functions.Interface;
using UiTest.Model.Function;
using UiTest.Service.Logger;
using UiTest.Config.Items;
using UiTest.Functions.TestFunctions.Config;

namespace UiTest.Functions.TestFunctions
{
    public class FunctionCover : BaseCover<BasefunctionConfig>
    {
        private readonly FunctionAnalysis functionAnalysis;
        private readonly FunctionData functionData;
        private Thread thread;
        public FunctionCover(ITestFunction function, CoverManagement<BasefunctionConfig> functionCoverManagement) : base(function, functionCoverManagement)
        {
            ItemSetting = function.ItemSetting;
            functionData = function.FunctionData;
            functionAnalysis = new FunctionAnalysis(functionData, function.BaseConfig);
        }
        public ItemSetting ItemSetting { get; private set; }

        public override CancellationTokenSource Cts => functionBody.Cts;

        public override bool IsRunning => thread?.IsAlive == true || isRunning;
        public override void Run()
        {
            if (ItemSetting.IsMultiTask)
            {
                Task.Run(Attack);
            }
            else
            {
                Attack();
            }
        }
        public override void Cancel()
        {
            Cancel($"*Attempt to cancel the process from the user*");
        }

        private void Attack()
        {
            if (IsRunning || !coverManagement.TryAdd(this)) return;
            isRunning = true;
            try
            {
                int runTimes = ItemSetting.Retry + 1;
                functionData.Start();
                for (functionData.RetryTimes = 0; functionData.RetryTimes < runTimes && functionBody.Cts?.IsCancellationRequested == false; functionData.RetryTimes++)
                {
                    try
                    {
                        thread = new Thread(functionBody.Run);
                        thread.Start();
                        if (!thread.Join(ItemSetting.TimeOut))
                        {
                            Stop($"Timeout: {functionData.CycleTime} >= {ItemSetting.TimeOut / 1000.0}");
                        }
                    }
                    catch (Exception ex)
                    {
                        ProgramLogger.AddError(functionData.ToString(), ex.Message);
                        Stop(ex.Message);
                    }
                    if (functionAnalysis.CheckResultTest())
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ProgramLogger.AddError(functionData.ToString(), ex.Message);
                Cancel(ex.Message);
            }
            finally
            {
                functionData.End();
                coverManagement.SetTestDone(this);
                isRunning = false;
            }
        }

        private void Cancel(string mess)
        {
            functionBody.Cancel();
            functionData.logger.AddWarningText(mess);
            thread?.Abort();
        }
        private void Stop(string mess)
        {
            ((ITestFunction)functionBody).Stop();
            functionData.logger.AddErrorText(mess);
            thread?.Abort();
        }
    }
}
