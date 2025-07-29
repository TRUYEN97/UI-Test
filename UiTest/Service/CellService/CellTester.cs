using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UiTest.Config;
using UiTest.Functions;
using UiTest.Model.Cell;
using UiTest.Service.Logger;
using UiTest.Service.Tester;

namespace UiTest.Service.CellService
{
    public class CellTester : BaseTester
    {
        private readonly CellTimer timer;
        private readonly CellData cellData;
        private readonly FucntionCoverManagement functionCoverManagement;

        public CellTester(CellTimer timer, CellData cellData)
        {
            this.timer = timer;
            this.cellData = cellData;
            functionCoverManagement = new FucntionCoverManagement();
        }

        public bool IsFree => !IsRunning;

        public void StartTest(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || IsRunning) return;
            functionCoverManagement.Cts = new CancellationTokenSource();
            functionCoverManagement.IsAcceptable = true;
            currentTask = Task.Run(async () =>
            {
                try
                {
                    input = input.Trim();
                    ModeFlow modeFlow = cellData.TestMode.ModeFlow;
                    for (int i = 0; i < modeFlow.Loop && functionCoverManagement.IsAcceptable && modeFlow.Reset(); i++)
                    {
                        try
                        {
                            timer.Start();
                            cellData.Start(input, modeFlow.Name);
                            List<ItemConfig> items;
                            while (functionCoverManagement.IsAcceptable && (items = modeFlow.GetListItem()) != null )
                            {
                                if (modeFlow.IsFinalGroup)
                                {
                                    cellData.End();
                                }
                                cellData.TestColor = modeFlow.TestColor;
                                cellData.FailColor = modeFlow.FailColor;
                                if (await Run(items))
                                {
                                    modeFlow.NextToPassFlow();
                                }
                                else
                                {
                                    modeFlow.NextToFailedFlow();
                                }
                            }
                            await functionCoverManagement.WaitForAllTaskDone();
                        }
                        catch (Exception ex)
                        {
                            ProgramLogger.AddError(cellData.Name, ex.Message);
                            cellData.SetExecption(ex.Message);
                        }
                        finally
                        {
                            await functionCoverManagement.StopAll();
                            cellData.EndProcess();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProgramLogger.AddError(cellData.Name, ex.Message);
                }
                finally
                {
                    timer.Stop();
                }
            }, functionCoverManagement.Cts.Token);
        }

        private async Task<bool> Run(List<ItemConfig> itemConfigs)
        {
            foreach (var itemConfig in itemConfigs)
            {
                if (!functionCoverManagement.IsAcceptable)
                {
                    break;
                }
                var functionCover = new FunctionCover(itemConfig, cellData, functionCoverManagement);
                if (functionCover.Config.IsMultiTask)
                {
                    _ = functionCover.Start();
                }
                else
                {
                    await functionCover.Start();
                }
            }
            return functionCoverManagement.IsAcceptable;
        }
    }
}
