using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace UiTest.Behaviors
{
    public class PreviewEnterCommandBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(PreviewEnterCommandBehavior),
                new PropertyMetadata(null, (s, e) =>
                {
                    if (s is UIElement element)
                    {
                        element.PreviewKeyDown -= OnKeyPress;
                        element.PreviewKeyDown += OnKeyPress;
                    }
                }));

        public static void SetCommand(UIElement element, ICommand command)
        {
            element.SetValue(CommandProperty, command);
        }
        public static ICommand GetCommand(UIElement element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        public static void OnKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender is UIElement element)
            {
                ICommand command = GetCommand(element);
                if (command?.CanExecute((element, e)) == true)
                {
                    command.Execute((element, e));
                }
            }
        }
    }
}
