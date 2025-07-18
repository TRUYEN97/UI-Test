using System;
using System.Windows.Input;
using System.Windows;

namespace UiTest.Behaviors
{
    public class PreviewEnterFocusBehavior
    {
        public static readonly DependencyProperty FocusTargetProperty =
            DependencyProperty.RegisterAttached(
                "FocusTarget",
                typeof(string),
                typeof(PreviewEnterFocusBehavior),
                new PropertyMetadata(null, OnFocusTargetChanged));

        public static string GetFocusTarget(DependencyObject obj)
        {
            return (string)obj.GetValue(FocusTargetProperty);
        }

        public static void SetFocusTarget(DependencyObject obj, string value)
        {
            obj.SetValue(FocusTargetProperty, value);
        }

        private static void OnFocusTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                element.PreviewKeyDown -= Element_PreviewKeyDown;
                element.PreviewKeyDown += Element_PreviewKeyDown;
            }
        }

        private static void Element_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender is FrameworkElement source)
            {
                var targetName = GetFocusTarget(source);
                if (!string.IsNullOrEmpty(targetName))
                {
                    var window = Window.GetWindow(source);
                    if (window != null)
                    {
                        window.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            if (window.FindName(targetName) is UIElement target && target.Focusable && target.IsEnabled && target.IsVisible)
                            {
                                target.Focus();
                                Keyboard.Focus(target);
                            }
                        }), System.Windows.Threading.DispatcherPriority.Input);
                    }
                }
            }
        }
    }
}
