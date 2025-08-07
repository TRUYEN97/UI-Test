using System.Windows;
using System.Windows.Input;

namespace UiTest.Behaviors
{
    public static class MouseLeftClickCommandBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(MouseLeftClickCommandBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static readonly DependencyProperty Commandparameter =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(MouseLeftClickCommandBehavior),
                new PropertyMetadata(null));

        public static void SetCommand(UIElement element, ICommand value)
            => element?.SetValue(CommandProperty, value);

        public static ICommand GetCommand(UIElement element)
            => (ICommand)element?.GetValue(CommandProperty);

        public static object GetCommandParameter(UIElement element) => element.GetValue(Commandparameter);
        public static void SetCommandParameter(UIElement element, object value) => element.SetValue(Commandparameter, value);

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement ui)
            {
                ui.MouseLeftButtonDown -= OnMouseLeftButtonDown;
                ui.MouseLeftButtonDown += OnMouseLeftButtonDown;
            }
        }

        private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is UIElement ui)
            {
                ICommand command = GetCommand(ui);
                object commandParameter = GetCommandParameter(ui);
                if (command?.CanExecute(commandParameter) == true)
                    command.Execute(commandParameter);
            }
        }
    }

}
