
using System;
using System.Threading.Tasks;
using UiTest.Common;
using UiTest.Config;
using UiTest.Functions.TestFunctions;
using UiTest.Model.Cell;
using UiTest.ModelView;

namespace UiTest.Service.CellService
{
    public class Cell
    {
        public readonly CellTimer Timer;
        public readonly CellData CellData;
        public readonly BaseSubModelView ViewModel;
        private readonly FunctionTester functionTester;

        public Cell(string name, BaseSubModelView viewModel, int index)
        {
            Timer = new CellTimer();
            CellData = new CellData(name, index);
            ViewModel = viewModel;
            functionTester = new FunctionTester(Timer, CellData);
            ViewModel.Name = name;
            Timer.AddTimeTick((ts) =>
            {
                ViewModel.TestTime = Timer.StringTestTime;
            });
            CellData.DataChaned += (data) =>
            {
                ViewModel.Update();
            };
            ViewModel.Cell = this;
        }
        public string Name => CellData.Name;
        public string StringTestTime => Timer.StringTestTime;
        public long TestTime => Timer.TestTime;
        public TestStatus TestStatus => CellData.TestStatus;
        public bool IsFree => functionTester.IsFree;
        public string ModeName => CellData.TestMode?.Name;
        public string Message => CellData.Message;
        public string Input => CellData.Input;

        public void StartTest(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                Task.Factory.StartNew(() =>
                {
                    UpdateMode(Core.Instance.ModelManagement.SelectedMode);
                    CellData.Input = input;
                    functionTester.Run();
                });
            }
        }
        public void UpdateMode(TestMode mode)
        {
            if (mode == null) return;
            CellData.TestMode = mode;
            ViewModel.Update();
        }
    }
}
