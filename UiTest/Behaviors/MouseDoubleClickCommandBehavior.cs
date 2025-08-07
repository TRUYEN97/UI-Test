using System.Windows;
using System.Windows.Input;

namespace UiTest.Behaviors
{
    internal class MouseDoubleClickCommandBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(MouseDoubleClickCommandBehavior),
                new PropertyMetadata(null, (ob, e) =>
                {
                    if (ob is UIElement element)
                    {
                        element.MouseLeftButtonDown -= OnMouseDown;
                        element.MouseLeftButtonDown += OnMouseDown;
                    }
                }));

        public static readonly DependencyProperty CommandParameter =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(MouseDoubleClickCommandBehavior), new PropertyMetadata(null));
        public static void SetCommand(UIElement element, ICommand command)
        {
            element?.SetValue(CommandProperty, command);
        }
        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element?.GetValue(CommandProperty);
        }
        public static void SetCommandParameter(UIElement element, object commandParameter)
        {
            element?.SetValue(CommandParameter, commandParameter);
        }
        public static object GetCommandParameter(UIElement element)
        {
            return element?.GetValue(CommandParameter);
        }
        public static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is UIElement element && e.ClickCount >= 2)
            {
                ICommand command = GetCommand(element);
                object commandParameter = GetCommandParameter(element);
                if (command?.CanExecute(commandParameter) == true)
                {
                    command.Execute(commandParameter);
                }
            }
        }
    }
}
