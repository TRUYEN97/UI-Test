using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using UiStore.Common;
using UiTest.Common;

namespace UiTest.ModelView
{
    internal class MainViewModel: BaseViewModel
    {
        private readonly SafeDispatcherProperty<Brush> _background;
        private readonly SafeDispatcherProperty<Brush> _sideBarBackground;
        private readonly SafeDispatcherProperty<TestSingleUnitViewModel> _currentViewModel;
        private string _product;
        private string _station;
        private string _selectedStatus;
        public MainViewModel()
        {
            _background = CreateSafeProperty<Brush>(nameof(Background), 
                CreateLinearGradientColor(new List<(string, double)> {("#FF0D0D3D", 0),("#FF000091", 1)}));
            _sideBarBackground = CreateSafeProperty<Brush>(nameof(SideBarBackground),
                CreateLinearGradientColor(new List<(string, double)> {("#FF2A7BAB", 0),("#F12A7BAB", 1)}));
            _currentViewModel = CreateSafeProperty(nameof(CurrentViewModel), new TestSingleUnitViewModel());
        }

        public ObservableCollection<object> StatusList { get; } = new ObservableCollection<object>() { "mode1", "mode2"};
        public ObservableCollection<string> Properties { get; } = new ObservableCollection<string>();
        public string SelectedStatus{ get => _selectedStatus; set { _selectedStatus = value; OnPropertyChanged(); }}
        public string Title => $"{ProgramInfo.ProductName} - V{ProgramInfo.ProductVersion}";
        public string PcName => PcInfo.PcName;
        public string Product { get => _product; set { _product = value; OnPropertyChanged(); } }
        public string Station { get => _station; set { _station = value; OnPropertyChanged(); } }

        public Brush SideBarBackground
        {
            get => _sideBarBackground.Value;
            set => _sideBarBackground.Value = value;
        }

        public Brush Background
        {
            get => _background.Value;
            set => _background.Value = value;
        }
        public TestSingleUnitViewModel CurrentViewModel {
            get => _currentViewModel.Value; 
            set => _currentViewModel.Value = value;
        }
    }
}
