
using System;
using UiTest.Config;
using UiTest.Model.Cell;
using UiTest.ModelView;

namespace UiTest.Service.Cell
{
    public class CellTest
    {
        public readonly CellTimer Timer;
        public readonly CellData CellData;
        public readonly BaseSubModelView ViewModel;
        public CellTest(string name, BaseSubModelView viewModel)
        {
            Timer = new CellTimer();
            CellData = new CellData(name);
            ViewModel = viewModel;
            ViewModel.Cell = this;
            ViewModel.Name = name;
            Timer.AddTimeTick((ts) =>
            {
                ViewModel.TestTime = Timer.StringTestTime;
            });
            Timer.Start();
        }
        public TestMode TestMode { get; set; }
        public string Name => CellData.Name;
        public string StringTestTime => Timer.StringTestTime;
        public long TestTime => Timer.TestTime;
        public string TestStatus => "Standby";
        public bool IsFree => true;

        public string ModeName => TestMode?.Name;

        public void UpdateMode(TestMode mode)
        {
            if (mode == null) return;
            TestMode = mode;
            ViewModel.TestMode = mode;
            ViewModel.Update();
        }
    }
}
