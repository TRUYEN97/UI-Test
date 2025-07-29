using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using UiTest.Common;
using UiTest.Config;
using UiTest.Model;
using UiTest.ModelView.ListBoxItems;
using UiTest.Service;
using UiTest.Service.Logger;
using UiTest.Service.Managements;
using UiTest.Service.Relay;

namespace UiTest.ModelView
{
    public class MainViewModel : BaseViewModel
    {
        private readonly SafeDispatcherProperty<Brush> _background;
        private readonly SafeDispatcherProperty<Brush> _sideBarBackground;
        private readonly WindowStateCommands _windowStateCommands;
        private readonly InputCommands _inputCommands;
        private readonly ModelManagement _modelManagement;
        private readonly ViewBuilder _viewBuilder;
        private string _index;
        private string _input;

        public MainViewModel(Core core)
        {
            _background = CreateSafeProperty<Brush>(nameof(Background),
                CreateLinearGradientColor(new List<(string, double)> { ("#22C1C3", 0), ("#009179", 1) }));
            _sideBarBackground = CreateSafeProperty<Brush>(nameof(SideBarBackground),
                CreateLinearGradientColor(new List<(string, double)> { ("#FF2A7BAB", 0), ("#F12A7BAB", 1) }));
            _windowStateCommands = new WindowStateCommands();
            _inputCommands = new InputCommands(core, this);
            _modelManagement = core.ModelManagement;
            _viewBuilder = core.ViewBuilder;
        }
        public ICommand DragMoveCommand => _windowStateCommands.DragMoveCommand;
        public ICommand ToggleSidebarCommand => _windowStateCommands.ToggleSidebarCommand;
        public ICommand CloseCommand => _windowStateCommands.CloseCommand;
        public ICommand MaximizeCommand => _windowStateCommands.MaximizeCommand;
        public ICommand MinimizeCommand => _windowStateCommands.MinimizeCommand;
        public ICommand InputKeyPessCommand => _inputCommands.InputKeyPessCommand;
        public ICommand IndexKeyPessCommand => _inputCommands.IndexKeyPessCommand;

        public ObservableCollection<TestMode> Modes => _modelManagement.Modes;
        public ObservableCollection<string> LogLines => ProgramLogger.Instance.MessageBox;
        public TestMode SelectedMode { get => _modelManagement.SelectedMode; set => _modelManagement.SelectedMode = value; }
        public ObservableCollection<PropertyModel> Properties => _modelManagement.Properties;
        public string Title => $"{AppInfo.ProductName} - V{AppInfo.ProductVersion}";
        public string PcName => PcInfo.PcName;
        public string Product => ConfigLoader.ProgramConfig.ProgramSetting.Product;
        public string Station => ConfigLoader.ProgramConfig.ProgramSetting.Station;
        public string Input { get => _input; set => SetProperty(ref _input, value, nameof(Input)); }
        public string Index { get => _index; set => SetProperty(ref _index, value, nameof(Index)); }


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
        public Visibility IndexVisibility => _inputCommands.IndexVisibility;
        public int Rows => _viewBuilder.Rows;
        public int Columns => _viewBuilder.Columns;
        public ObservableCollection<BaseSubModelView> Cells => _viewBuilder.Cells;
    }
}
