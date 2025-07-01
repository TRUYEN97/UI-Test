using System.Windows;
using System.Windows.Controls;
using UiTest.Common;

namespace UiTest.View.Component
{
    /// <summary>
    /// Interaction logic for LabeledStatusBox.xaml
    /// </summary>
    public partial class LabeledStatusBox : UserControl
    {
        public LabeledStatusBox()
        {
            InitializeComponent();
            FontSize = 16;
            Padding = new Thickness(0, 2, 3, 0);
        }
        public string Label
        {
            get => (string)GetValue(LabelPropertie);
            set => SetValue(LabelPropertie, value);
        }
        public int LabelFontSize
        {
            get => (int)GetValue(LabelFontSizePropertie);
            set => SetValue(LabelFontSizePropertie, value);
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty LabelPropertie = DependencyUtil.RegisterDependencyProperty(nameof(Label), typeof(LabeledStatusBox), string.Empty);
        public static readonly DependencyProperty LabelFontSizePropertie = DependencyUtil.RegisterDependencyProperty(nameof(LabelFontSize), typeof(LabeledStatusBox), 12);
        public static readonly DependencyProperty TextProperty = DependencyUtil.RegisterDependencyProperty(nameof(Text), typeof(LabeledStatusBox),string.Empty);
    }
}
