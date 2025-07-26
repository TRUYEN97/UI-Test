using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UiTest.Config;
using UiTest.Functions;
using UiTest.Model.Cell;
using UiTest.ModelView;
using UiTest.Service.Logger;
using UiTest.Service.Tester;

namespace UiTest.Service.CellService
{
    public class CellTester : BaseTester
    {
        private readonly Cell cell;
        private readonly CellTimer timer;
        private readonly CellData cellData;
        private readonly BaseSubModelView viewModel;
        private CancellationTokenSource cts;

        public CellTester(Cell cell)
        {
            this.cell = cell;
            this.timer = cell.Timer;
            this.cellData = cell.CellData;
            this.viewModel = cell.ViewModel;
        }
        public bool IsFree => !IsRunning;

        public void StartTest(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || IsRunning) return;
            currentTask = Task.Run(async () =>
            {
                try
                {
                    TestMode testMode = cell.TestMode;
                    input = input.ToUpper().Trim();
                    ModeFlow modeFlow = testMode.ModeFlow;
                    cts = new CancellationTokenSource();
                    for (int i = 0; !cts.IsCancellationRequested && i < modeFlow.Loop && modeFlow.Reset(); i++)
                    {
                        try
                        {
                            timer.Start();
                            cellData.Start(input, testMode.Name);
                            List<ItemConfig> items;
                            while ((items = modeFlow.GetListItem()) != null && !cts.IsCancellationRequested)
                            {
                                if (modeFlow.IsFinalGroup)
                                {
                                    cellData.End();
                                }
                                viewModel.Color = modeFlow.TestColor;
                                if (await Run(items))
                                {
                                    modeFlow.NextToPassFlow();
                                }
                                else
                                {
                                    viewModel.Color = modeFlow.FailColor;
                                    modeFlow.NextToFailedFlow();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ProgramLogger.AddError(cellData.Name, ex.Message);
                            cellData.SetExecption(ex.Message);
                        }
                        finally
                        {
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
            });
        }

        private async Task<bool> Run(List<ItemConfig> itemConfigs)
        {
            foreach (var itemConfig in itemConfigs)
            {
                if (cts.IsCancellationRequested)
                {
                    break;
                }
                var functionCover = new FunctionCover(itemConfig, cellData);
                functionCover.functionBody.Cts = cts;
                await functionCover.Start();
                if (!functionCover.IsAcceptable)
                {
                    break;
                }
            }
            return !cellData.HasFailedFunctions;
        }
    }
}
