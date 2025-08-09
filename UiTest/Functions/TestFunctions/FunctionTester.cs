using System;
using System.Collections.Generic;
using System.Threading;
using UiTest.Common;
using UiTest.Config.Items;
using UiTest.Functions.ActionEvents;
using UiTest.Functions.TestFunctions.Config;
using UiTest.Model.Cell;
using UiTest.Model.Function;
using UiTest.Service.CellService;
using UiTest.Service.Factory;
using UiTest.Service.Logger;

namespace UiTest.Functions.TestFunctions
{
    public class FunctionTester : BaseRunner<FunctionCoverManagement, BasefunctionConfig>
    {
        private readonly CellTimer timer;
        private readonly CellData cellData;
        private readonly InputEventRunner inputEventRunner;

        public FunctionTester(CellTimer timer, CellData cellData)
        {
            this.timer = timer;
            this.cellData = cellData;
            coverManagement = new FunctionCoverManagement();
            inputEventRunner = new InputEventRunner(cellData);
            CancelRunEvent += inputEventRunner.CancelRun;
        }

        public bool IsFree => !IsRunning;

        protected override bool IsRunnable()
        {
            return !string.IsNullOrWhiteSpace(cellData.Input) && !IsRunning && cellData?.TestMode != null ;
        }
        protected override void RunAction()
        {
            try
            {
                inputEventRunner.ActionEvents = cellData.TestMode.InputEvents;
                inputEventRunner.Run();
                if (inputEventRunner.IsPassed)
                {
                    RunFunctionsTest();
                }
            }
            catch (Exception ex)
            {
                ProgramLogger.AddError(cellData.Name, ex.Message);
            }
            finally
            {
                timer.Stop();
                cellData.Input = null;
            }
        }

        private void RunFunctionsTest()
        {
            ModeFlow modeFlow = cellData.TestMode.ModeFlow;
            for (int i = 0; i < modeFlow.Loop && !coverManagement.IsRunCancelled && modeFlow.Reset(); i++)
            {
                try
                {
                    timer.Start();
                    cellData.Start(modeFlow.Name);
                    List<FunctionConfig> items;
                    while (!coverManagement.IsRunCancelled && (items = modeFlow.GetListItem()) != null)
                    {
                        cellData.FailColor = modeFlow.FailColor;
                        if (modeFlow.IsFinalGroup)
                        {
                            cellData.End();
                        }
                        switch (RunFunctions(items))
                        {
                            case TestResult.FAILED:
                                modeFlow.NextToFailedFlow();
                                break;
                            case TestResult.PASSED:
                                modeFlow.NextToPassFlow();
                                break;
                            case TestResult.CANCEL:
                                modeFlow.NextToCanceledFlow();
                                break;
                            default:
                                break;
                        }
                    }
                    coverManagement.WaitForAllTaskDone();
                }
                catch (Exception ex)
                {
                    ProgramLogger.AddError(cellData.Name, ex.Message);
                    cellData.SetExecption(ex.Message);
                }
                finally
                {
                    coverManagement.CancelAllTask();
                    cellData.EndProcess();
                }
            }
        }

        private TestResult RunFunctions(List<FunctionConfig> functionConfigs)
        {
            foreach (var functionConfig in functionConfigs)
            {
                if (coverManagement.IsRunCancelled)
                {
                    break;
                }
                var functionData = new FunctionData(functionConfig.Name, functionConfig.FunctionType, cellData);
                var functionBody = FunctionFactory.Instance.CreateFunctionWith(functionConfig, functionData);
                var functionCover = new FunctionCover(functionBody, coverManagement);
                functionCover.Run();
            }
            coverManagement.WaitForTaskDone();
            return cellData.CurrentResult;
        }
    }
}
