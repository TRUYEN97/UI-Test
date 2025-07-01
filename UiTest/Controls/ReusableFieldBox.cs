using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UiTest.Common;

namespace UiTest.Controls
{
    public class ReusableFieldBox : ContentControl
    {
        static ReusableFieldBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ReusableFieldBox),
                new FrameworkPropertyMetadata(typeof(ReusableFieldBox)));
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        } 

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Thickness InnerPadding
        {
            get => (Thickness)GetValue(InnerPaddingProperty);
            set => SetValue(InnerPaddingProperty, value);
        }

        public Brush ShadowBackground
        {
            get => (Brush)GetValue(ShadowBackgroundProperty);
            set => SetValue(ShadowBackgroundProperty, value);
        }
        public static readonly DependencyProperty LabelProperty = DependencyUtil.RegisterDependencyProperty(nameof(Label), typeof(ReusableFieldBox), string.Empty);
        public static readonly DependencyProperty CornerRadiusProperty = DependencyUtil.RegisterDependencyProperty(nameof(CornerRadius), typeof(ReusableFieldBox), new CornerRadius(10));
        public static readonly DependencyProperty InnerPaddingProperty = DependencyUtil.RegisterDependencyProperty(nameof(InnerPadding), typeof(ReusableFieldBox), new Thickness(10, 0, 10, 0));
        public static readonly DependencyProperty ShadowBackgroundProperty = DependencyUtil.RegisterDependencyProperty(nameof(ShadowBackground), typeof(ReusableFieldBox),new SolidColorBrush(Color.FromArgb(100, 50, 50, 50)));
    }
}
