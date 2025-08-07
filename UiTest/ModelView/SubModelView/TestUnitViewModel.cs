using System;
using System.Collections.ObjectModel;
using UiTest.ModelView.TabItemViewModel;
using UiTest.Service.CellService;

namespace UiTest.ModelView.SubModelView
{
    public class TestUnitViewModel : BaseSubModelView
    {
        private readonly TabLogViewModel tabLogViewModel;
        private readonly TabMessageViewModel tabMessageViewModel;

        public TestUnitViewModel() : base()
        {
            tabLogViewModel = new TabLogViewModel();
            tabMessageViewModel = new TabMessageViewModel();
            Tabs = new ObservableCollection<BaseTabItemViewModel>()
            {
               tabMessageViewModel,
               tabLogViewModel
            };
            if(Tabs.Count > 0)
            {
                SelectedTab = Tabs[0];
            }
        }
        public ObservableCollection<BaseTabItemViewModel> Tabs { get; }
        public BaseTabItemViewModel SelectedTab { get; set; }
        protected override void UpdateMessage()
        {
            base.UpdateMessage();
            tabMessageViewModel.Message = Message;
        }
        protected override void UpdateCellData(Cell Cell)
        {
            var cellData = Cell?.CellData;
            if (cellData == null) return;
            tabLogViewModel.Log = cellData.CellLogger.LogText;
        }
    }
}
