using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using UiTest.Common;
using UiTest.Config;
using UiTest.Service;
using UiTest.Service.Managements;
using UiTest.Service.Relay;

namespace UiTest.ModelView
{
    internal class MainViewModel: BaseViewModel
    {
        private readonly SafeDispatcherProperty<Brush> _background;
        private readonly SafeDispatcherProperty<Brush> _sideBarBackground;
        private readonly WindowStateCommands _windowStateCommands;
        private readonly ModelManagement _modelManagement;
        private readonly ViewBuilder _viewBuilder;
        private string _product;
        private string _station;
        public MainViewModel(Core core)
        {
            _background = CreateSafeProperty<Brush>(nameof(Background), 
                CreateLinearGradientColor(new List<(string, double)> {("#22C1C3", 0),("#009179", 1)}));
            _sideBarBackground = CreateSafeProperty<Brush>(nameof(SideBarBackground),
                CreateLinearGradientColor(new List<(string, double)> {("#FF2A7BAB", 0),("#F12A7BAB", 1)}));
            _windowStateCommands = new WindowStateCommands();
            _modelManagement = core.ModelManagement;
            _viewBuilder = core.ViewBuilder;
        }
        public ICommand DragMoveCommand => _windowStateCommands.DragMoveCommand;
        public ICommand ToggleSidebarCommand => _windowStateCommands.ToggleSidebarCommand;
        public ICommand CloseCommand => _windowStateCommands.CloseCommand;
        public ICommand MaximizeCommand => _windowStateCommands.MaximizeCommand;
        public ICommand MinimizeCommand => _windowStateCommands.MinimizeCommand;

        public ObservableCollection<TestMode> Modes => _modelManagement.Modes;
        public TestMode SelectedMode{ get => _modelManagement.SelectedMode; set => _modelManagement.SelectedMode = value; }
        public ObservableCollection<string> Properties { get; } = new ObservableCollection<string>() { "mode1", "mode2" };
        public string Title => $"{AppInfo.ProductName} - V{AppInfo.ProductVersion}";
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
        public int Rows => _viewBuilder.Rows;
        public int Columns => _viewBuilder.Columns;
        public ObservableCollection<BaseSubModelView> Cells => _viewBuilder.Cells;
    }
}
