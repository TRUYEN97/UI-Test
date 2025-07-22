using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UiTest.Config;
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
                    StopTest = false;
                    for (int i = 0; i < modeFlow.Loop && !StopTest; i++, modeFlow.Reset())
                    {
                        try
                        {
                            timer.Start();
                            cellData.Start(input, testMode.Name);
                            List<ItemConfig> items;
                            while ((items = modeFlow.GetListItem()) != null && !StopTest)
                            {
                                if (modeFlow.IsFinalGroup)
                                {
                                    cellData.End();
                                }
                                viewModel.Color = modeFlow.TestColor;
                                if (Run(items))
                                {
                                    modeFlow.NextToPassFlow();
                                }
                                else
                                {
                                    viewModel.Color = modeFlow.FailColor;
                                    modeFlow.NextToFailedFlow();
                                }
                            }
                            await Task.Delay(5000);
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

        private bool Run(List<ItemConfig> items)
        {
            return items.Count > 0;
        }
    }
}
