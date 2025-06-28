using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UiTest.ModelView;

namespace UiTest.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool sidebarVisible;
        public MainWindow()
        {
            InitializeComponent();
            Background = Brushes.Transparent;
            var _viewModel = new MainViewModel();
            DataContext = _viewModel;
            sidebarVisible = true;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }
        private void ToggleSidebar_Click(object sender, RoutedEventArgs e)
        {
            ToggleSideba();
        }

        private void ToggleSideba()
        {
            var sb = (Storyboard)FindResource(sidebarVisible ? "CollapseSidebar" : "ExpandSidebar");
            sb.Begin();
            sidebarVisible = !sidebarVisible;
        }
    }
}
