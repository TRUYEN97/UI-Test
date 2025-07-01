using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using UiTest.Service;

namespace UiTest.Relay
{
    internal class WindowStateCommands    {
        public WindowStateCommands()
        {
            DragMoveCommand = new RelayCommand(DragMove);
            ToggleSidebarCommand = new RelayCommand(ToggleSidebar);
            CloseCommand = new RelayCommand(ExecuteClose);
            MaximizeCommand = new RelayCommand(ExecuteMaximize);
            MinimizeCommand = new RelayCommand(ExecuteMinimize);
        }
        public bool SidebarVisible { get; set; } = true;
        public RelayCommand DragMoveCommand { get; private set; }
        public RelayCommand ToggleSidebarCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }
        public RelayCommand MaximizeCommand { get; private set; }
        public RelayCommand MinimizeCommand { get; private set; }


        private void ToggleSidebar(object obj)
        {
            if (obj is FrameworkElement element)
            {
                var window = Window.GetWindow(element);
                var sb = (Storyboard)window.FindResource(SidebarVisible ? "CollapseSidebar" : "ExpandSidebar");
                sb.Begin();
                SidebarVisible = !SidebarVisible;
            }
        }
        private void DragMove(object obj)
        {
            if (obj is Window window)
                window.DragMove();
        }

        private void ExecuteClose(object obj)
        {
            if (obj is Window window)
                window.Close();
        }

        private void ExecuteMaximize(object obj)
        {
            if (obj is Window window)
            {
                window.WindowState = window.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
        }

        private void ExecuteMinimize(object obj)
        {
            if (obj is Window window)
                window.WindowState = WindowState.Minimized;
        }
    }
}
