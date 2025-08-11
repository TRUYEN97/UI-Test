using System;
using System.Collections.ObjectModel;
using UiTest.ModelView.TabItemViewModel;
using UiTest.ModelView.TabItemViewModel.ViewTabs;
using UiTest.Service.CellService;

namespace UiTest.ModelView.SubModelView
{
    public class TestUnitViewModel : BaseSubModelView
    {
        private readonly TabLogModelView tabLogViewModel;
        private readonly TabMessageModelView tabMessageViewModel;

        public TestUnitViewModel() : base()
        {
            tabLogViewModel = new TabLogModelView();
            tabMessageViewModel = new TabMessageModelView();
            Tabs = new ObservableCollection<BaseViewTabModelView>()
            {
               tabMessageViewModel,
               tabLogViewModel
            };
            if(Tabs.Count > 0)
            {
                SelectedTab = Tabs[0];
            }
        }
        public ObservableCollection<BaseViewTabModelView> Tabs { get; }
        public BaseViewTabModelView SelectedTab { get; set; }
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
