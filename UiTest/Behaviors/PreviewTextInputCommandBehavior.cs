using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace UiTest.Behaviors
{
    public static class PreviewTextInputCommandBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(PreviewTextInputCommandBehavior),
                new PropertyMetadata(null, (ob, e) =>
                {
                    if (ob is UIElement element)
                    {
                        element.PreviewTextInput -= OnTextInput;
                        element.PreviewTextInput += OnTextInput;
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

        public static void OnTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is UIElement element)
            {
                ICommand command = GetCommand(element);
                string text = e.Text;
                if (command?.CanExecute(text) == true)
                {
                    command.Execute(text);
                }
            }
        }
    }
}
