using System.Collections.ObjectModel;
using UiTest.ModelView.TabItemViewModel;
using UiTest.Service;

namespace UiTest.ModelView
{
    internal class TestUnitViewModel : BaseViewModel
    {
        public ObservableCollection<BaseTabItemViewModel> Tabs { get; }
        public TestUnitData TestUnitData { get; }
        public string Name => TestUnitData?.Name;

        public TestUnitViewModel(TestUnitData testUnitData) {
            TestUnitData = testUnitData;
            TabLoggerViewModel tabLoggerViewModel = new TabLoggerViewModel();
            testUnitData.UnitLogger.AddWriteActionCallback( log => tabLoggerViewModel.AddLog(log));
            testUnitData.UnitLogger.AddClearCallback( () => tabLoggerViewModel.Clear());
            Tabs = new ObservableCollection<BaseTabItemViewModel>()
            {
               tabLoggerViewModel
            };
        }
    }
}
