using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using UiTest.Common;
using UiTest.Config;
using UiTest.Functions.Config;
using UiTest.Functions.Interface;
using UiTest.Model.Cell;
using UiTest.Model.Function;
using UiTest.Service.Factory;
using UiTest.Service.Logger;

namespace UiTest.Functions
{
    public class FunctionCover
    {
        public readonly FunctionData FunctionData;
        public readonly ITestFunction functionBody;
        public readonly BasefunctionConfig Config;
        private readonly FunctionAnalysis functionAnalysis;
        private readonly FucntionCoverManagement functionCoverManagement;
        private Thread thread;
        private bool isRunning;
        public FunctionCover(ItemConfig itemConfig, CellData cellData, FucntionCoverManagement functionCoverManagement)
        {
            FunctionData = new FunctionData(itemConfig.Name, itemConfig.FunctionType, cellData);
            functionBody = FunctionFactory.Instance.CreateFunctionWithTypeName(itemConfig.FunctionType, FunctionData, itemConfig.Config);
            Config = functionBody.BaseConfig;
            functionAnalysis = new FunctionAnalysis(FunctionData, Config);
            functionBody.Cts = new CancellationTokenSource();
            this.functionCoverManagement = functionCoverManagement;
        }
        public bool IsRunning => thread != null && thread.IsAlive || isRunning;

        public bool IsAcceptable => !FunctionData.IsFailed || Config.IsSkipFailure;

        public Task Start()
        {
            return Task.Run(() =>
            {
                try
                {
                    if (IsRunning || !functionCoverManagement.TryAdd(FunctionData.ToString(), this)) return;
                    isRunning = true;
                    int runTimes = Config.Retry + 1;
                    FunctionData.Start();
                    for (int times = 0; times < runTimes; times++)
                    {
                        thread?.Abort();
                        thread = new Thread(() =>
                        {
                            FunctionData.TurnInit(times);
                            FunctionData.TestResult = functionBody.Run(times);
                        });
                        thread.Start();
                        if (!thread.Join(Config.TimeOut))
                        {
                            FunctionData.logger.AddErrorText($"Time out:{FunctionData.CycleTime} >= {Config.TimeOut}");
                            Stop();
                            FunctionData.TestResult = (TestStatus.FAILED, "");
                        }
                        functionAnalysis.CheckResultTest();
                        if (functionAnalysis.IsAcceptable)
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProgramLogger.AddError(FunctionData.ToString(), ex.Message);
                    FunctionData.logger.AddErrorText(ex.Message);
                    FunctionData.TestResult = (TestStatus.FAILED, "");
                    functionAnalysis.CheckResultTest();
                }
                finally
                {
                    FunctionData.End();
                    functionBody.Cancel();
                    isRunning = false;
                    functionCoverManagement.SetTestDone(FunctionData.ToString());
                }
            });
        }
        public void Stop()
        {
            functionBody.Cancel();
            thread.Abort();
        }
    }
}
