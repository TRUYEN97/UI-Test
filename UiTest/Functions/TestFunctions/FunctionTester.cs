using System;
using System.Collections.Generic;
using System.Threading;
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
        }

        public bool IsFree => !IsRunning;

        public void Cancel()
        {
            if (IsRunning)
            {
                if (coverManagement.Cts?.IsCancellationRequested == false)
                {
                    coverManagement.Cts?.Cancel();
                }
                coverManagement.CancelAll();
            }
        }
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
                if (inputEventRunner.IsAcceptable)
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
            coverManagement.IsAcceptable = true;
            for (int i = 0; i < modeFlow.Loop && coverManagement.IsAcceptable && modeFlow.Reset(); i++)
            {
                try
                {
                    timer.Start();
                    cellData.Start(modeFlow.Name);
                    List<FunctionConfig> items;
                    while (coverManagement.IsAcceptable && (items = modeFlow.GetListItem()) != null)
                    {
                        cellData.FailColor = modeFlow.FailColor;
                        if (modeFlow.IsFinalGroup)
                        {
                            cellData.End();
                        }
                        if (RunFunctions(items))
                        {
                            modeFlow.NextToPassFlow();
                        }
                        else
                        {
                            modeFlow.NextToFailedFlow();
                        }
                    }
                    coverManagement.WaitForTaskDone();
                }
                catch (Exception ex)
                {
                    ProgramLogger.AddError(cellData.Name, ex.Message);
                    cellData.SetExecption(ex.Message);
                }
                finally
                {
                    coverManagement.StopAll();
                    cellData.EndProcess();
                }
            }
        }

        private bool RunFunctions(List<FunctionConfig> functionConfigs)
        {
            foreach (var functionConfig in functionConfigs)
            {
                if (!coverManagement.IsAcceptable)
                {
                    break;
                }
                var functionData = new FunctionData(functionConfig.Name, functionConfig.FunctionType, cellData);
                var functionBody = FunctionFactory.Instance.CreateFunctionWith(functionConfig, functionData);
                var functionCover = new FunctionCover(functionBody, coverManagement);
                functionCover.Run();
            }
            return coverManagement.IsAcceptable;
        }
    }
}
