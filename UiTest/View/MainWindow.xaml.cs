using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using UiTest.ModelView;

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
            var _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }
    }
}
