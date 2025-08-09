using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UiTest.Config;
using UiTest.Functions.ActionEvents;
using UiTest.ModelView;
using UiTest.Service;

namespace UiTest.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel mainViewModel;
        public MainWindow()
        {
            InitializeComponent();
            Background = Brushes.Transparent;
            if (Core.Instance.Update())
            {
                mainViewModel = new MainViewModel(Core.Instance);
                DataContext = mainViewModel;
                Loaded += (s, e) =>
                {
                    TxtInput.Focus();
                };
            }
            else
            {
                Application.Current?.Shutdown();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cbb)
            {
                var oldMode = mainViewModel.SelectedMode;
                var newMode = cbb.SelectedItem;
                if (oldMode != newMode &&  mainViewModel.ModeSelectionChangedCommand?.CanExecute(newMode) == true)
                {
                    mainViewModel.ModeSelectionChangedCommand.Execute(newMode);
                }
            }
        }
    }
}
