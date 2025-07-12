using System;
using System.Collections.ObjectModel;
using UiTest.ModelView.TabItemViewModel;
using UiTest.Service.Cell;

namespace UiTest.ModelView.SubModelView
{
    public class TestUnitViewModel : BaseSubModelView
    {
        private readonly TabLoggerViewModel tabLoggerViewModel;

        public TestUnitViewModel() : base()
        {
            tabLoggerViewModel = new TabLoggerViewModel();
            Tabs = new ObservableCollection<BaseTabItemViewModel>()
            {
               tabLoggerViewModel
            };
            SelectedTab = tabLoggerViewModel;
        }
        public ObservableCollection<BaseTabItemViewModel> Tabs { get; }
        public BaseTabItemViewModel SelectedTab { get; set; }

        protected override bool UpdateView()
        {
            return true;
        }

        protected override void UpdateCellData(CellTest Cell)
        {
            UpdataCellData(Cell);
        }

        private void UpdataCellData(CellTest Cell)
        {
            var cellData = Cell?.CellData;
            if (cellData == null) return;
            cellData.UnitLogger.AddWriteActionCallback(log => tabLoggerViewModel.AddLog(log));
            cellData.UnitLogger.AddClearCallback(() => tabLoggerViewModel.Clear());
        }
    }
}
