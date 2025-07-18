using System;
using System.Collections.ObjectModel;
using UiTest.ModelView.TabItemViewModel;
using UiTest.Service.CellService;

namespace UiTest.ModelView.SubModelView
{
    public class TestUnitViewModel : BaseSubModelView
    {
        private readonly TabLoggerViewModel tabLoggerViewModel;
        private int _progressValue;
        private int _progressMaximum;
        private string _progressPercent;

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

        public int ProgressValue
        {
            get => _progressValue;
            set
            {
                if (_progressValue != value)
                _progressValue = value > ProgressMaximum ? ProgressMaximum : value;
                OnPropertyChanged();
                UpdateProcessPercent();
            }
        }
        public int ProgressMaximum
        {
            get => _progressMaximum;
            set
            {
                _progressMaximum = value;
                OnPropertyChanged();
                UpdateProcessPercent();
            }
        }
        public string ProgressPercent => _progressPercent;

        private void UpdateProcessPercent()
        {
            _progressPercent = $"{ProgressValue / ProgressMaximum * 100}%";
            OnPropertyChanged(nameof(ProgressPercent));
        }
        protected override void UpdateCellData(Cell Cell)
        {
            var cellData = Cell?.CellData;
            if (cellData == null) return;
        }
    }
}
