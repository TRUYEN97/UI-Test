
using System;
using UiTest.Config;
using UiTest.Model.Cell;
using UiTest.ModelView;

namespace UiTest.Service.CellService
{
    public class Cell
    {
        public readonly CellTimer Timer;
        public readonly CellData CellData;
        public readonly BaseSubModelView ViewModel;
        private readonly CellTester tester;
        public Cell(string name, BaseSubModelView viewModel)
        {
            Timer = new CellTimer();
            CellData = new CellData(name);
            ViewModel = viewModel;
            tester = new CellTester(this);
            ViewModel.Cell = this;
            ViewModel.Name = name;
            Timer.AddTimeTick((ts) =>
            {
                ViewModel.TestTime = Timer.StringTestTime;
            });
            CellData.DataChaned += (data) =>
            {
                ViewModel.Update();
            };
        }
        public TestMode TestMode { get; set; }
        public string Name => CellData.Name;
        public string StringTestTime => Timer.StringTestTime;
        public long TestTime => Timer.TestTime;
        public string TestStatus => CellData.ProcessStatus.ToString();
        public bool IsFree => tester.IsFree;
        public string ModeName => TestMode?.Name;
        public string Message => CellData.Message;

        public void StartTest(string input)
        {
            UpdateMode(Core.Instance.ModelManagement.SelectedMode);
            tester.StartTest(input);
        }
        public void UpdateMode(TestMode mode)
        {
            if (mode == null) return;
            TestMode = mode;
            ViewModel.TestMode = mode;
        }
    }
}
