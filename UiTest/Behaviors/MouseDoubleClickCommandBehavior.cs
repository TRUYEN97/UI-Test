using System.Windows;
using System.Windows.Input;

namespace UiTest.Behaviors
{
    internal class MouseDoubleClickCommandBehavior
    {
        public static readonly DependencyProperty CommadProperty =
            DependencyProperty.RegisterAttached(
                "command",
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
        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommadProperty, command);
        }
        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommadProperty);
        }

        public static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as UIElement;
            ICommand command = GetCommand(element);
            if (command?.CanExecute(null) == true)
            {
                command.Execute(null);
            }
        }
    }
}
