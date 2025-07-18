using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UiTest.Common;

namespace UiTest.View.Component
{
    /// <summary>
    /// Interaction logic for MyTextBox.xaml
    /// </summary>
    public partial class MyTextBox : UserControl
    {
        public MyTextBox()
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
        public string Text
        {
            get => (string)GetValue(TextPropertie);
            set => SetValue(TextPropertie, value);
        }
        public int LabelFontSize
        {
            get => (int)GetValue(LabelFontSizePropertie);
            set => SetValue(LabelFontSizePropertie, value);
        }

        public static readonly DependencyProperty LabelFontSizePropertie = DependencyUtil.RegisterDependencyProperty<int>(nameof(LabelFontSize), typeof(MyTextBox), 12);
        public static readonly DependencyProperty LabelPropertie = DependencyUtil.RegisterDependencyProperty<string>(nameof(Label), typeof(MyTextBox));
        public static readonly DependencyProperty TextPropertie = DependencyUtil.RegisterDependencyProperty<string>(nameof(Text), typeof(MyTextBox));
    }
}
