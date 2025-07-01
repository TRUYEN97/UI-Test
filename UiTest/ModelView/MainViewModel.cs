using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UiStore.Common;
using UiTest.Common;
using UiTest.Mode;
using UiTest.Relay;
using UiTest.Service;

namespace UiTest.ModelView
{
    internal class MainViewModel: BaseViewModel
    {
        private readonly SafeDispatcherProperty<Brush> _background;
        private readonly SafeDispatcherProperty<Brush> _sideBarBackground;
        private readonly SafeDispatcherProperty<TestUnitViewModel> _currentViewModel;
        private readonly WindowStateCommands _windowStateCommands;
        private readonly ModelManagement _modelManagement;
        private string _product;
        private string _station;
        public MainViewModel()
        {
            _background = CreateSafeProperty<Brush>(nameof(Background), 
                CreateLinearGradientColor(new List<(string, double)> {("#22C1C3", 0),("#009179", 1)}));
            _sideBarBackground = CreateSafeProperty<Brush>(nameof(SideBarBackground),
                CreateLinearGradientColor(new List<(string, double)> {("#FF2A7BAB", 0),("#F12A7BAB", 1)}));
            _currentViewModel = CreateSafeProperty(nameof(CurrentViewModel), new TestUnitViewModel(new TestUnitData("1")));
            _windowStateCommands = new WindowStateCommands();
            _modelManagement = new ModelManagement();
        }
        public ICommand DragMoveCommand => _windowStateCommands.DragMoveCommand;
        public ICommand ToggleSidebarCommand => _windowStateCommands.ToggleSidebarCommand;
        public ICommand CloseCommand => _windowStateCommands.CloseCommand;
        public ICommand MaximizeCommand => _windowStateCommands.MaximizeCommand;
        public ICommand MinimizeCommand => _windowStateCommands.MinimizeCommand;

        public ObservableCollection<BaseMode> Modes => _modelManagement.Modes;
        public BaseMode SelectedMode{ get => _modelManagement.SelectedMode; set => _modelManagement.SelectedMode = value; }
        public ObservableCollection<string> Properties { get; } = new ObservableCollection<string>() { "mode1", "mode2" };
        public string Title => $"{ProgramInfo.ProductName} - V{ProgramInfo.ProductVersion}";
        public string PcName => PcInfo.PcName;
        public string Product { get => _product; set => SetProperty(ref _product, value, nameof(Product)); }
        public string Station { get => _station; set => SetProperty(ref _station, value, nameof(Station)); }

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
        public TestUnitViewModel CurrentViewModel {
            get => _currentViewModel.Value; 
            set => _currentViewModel.Value = value;
        }
    }
}
