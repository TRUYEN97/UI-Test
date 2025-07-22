using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using UiTest.ModelView;
using UiTest.Service;

namespace UiTest.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Background = Brushes.Transparent;
            Core.Instance.Update();
            var _viewModel = new MainViewModel(Core.Instance);
            DataContext = _viewModel;
            Loaded += (s, e) =>
            {
                TxtInput.Focus();
            };
        }
    }
}
