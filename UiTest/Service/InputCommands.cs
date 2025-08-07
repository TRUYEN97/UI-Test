using System.Windows;
using UiTest.ModelView;
using UiTest.Service.Relay;

namespace UiTest.Service
{
    public class InputCommands
    {
        private readonly Core core;

        public InputCommands(Core core, MainViewModel mainViewModel)
        {
            this.core = core;;
            InputKeyPessCommand = new RelayCommand((ob) =>
            {
                if (IsSingleView)
                {
                    core.Start(mainViewModel.Input);
                    mainViewModel.Input = string.Empty;
                }
            });
            IndexKeyPessCommand = new RelayCommand((ob) =>
            {
                if (!IsSingleView)
                {
                    core.Start(mainViewModel.Input, mainViewModel.Index);
                }
                mainViewModel.Input = string.Empty;
                mainViewModel.Index = string.Empty;
            });
        }
        public RelayCommand InputKeyPessCommand { get; private set; }
        public RelayCommand IndexKeyPessCommand { get; private set; }
        public Visibility IndexVisibility => IsSingleView ? Visibility.Collapsed : Visibility.Visible;
        private bool IsSingleView => core.ProgramConfig.ProgramSetting.IsSingleView;
    }
}
