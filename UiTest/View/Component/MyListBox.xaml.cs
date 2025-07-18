using System.Collections;
using System.Windows;
using System.Windows.Controls;
using UiTest.Common;

namespace UiTest.View.Component
{
    /// <summary>
    /// Interaction logic for MyListBox.xaml
    /// </summary>
    public partial class MyListBox : UserControl
    {
        public MyListBox()
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
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourcePropertie);
            set => SetValue(ItemsSourcePropertie, value);
        }
        public object SelectedItem
        {
            get => GetValue(SelectedItemPropertie);
            set => SetValue(SelectedItemPropertie, value);
        }
        public int LabelFontSize
        {
            get => (int)GetValue(LabelFontSizePropertie);
            set => SetValue(LabelFontSizePropertie, value);
        }

        public static readonly DependencyProperty LabelFontSizePropertie = DependencyUtil.RegisterDependencyProperty<int>(nameof(LabelFontSize), typeof(MyListBox), 12);
        public static readonly DependencyProperty LabelPropertie = DependencyUtil.RegisterDependencyProperty<string>(nameof(Label), typeof(MyListBox));
        public static readonly DependencyProperty ItemsSourcePropertie = DependencyUtil.RegisterDependencyProperty<IEnumerable>(nameof(ItemsSource), typeof(MyListBox));
        public static readonly DependencyProperty SelectedItemPropertie = DependencyUtil.RegisterDependencyProperty<object>(nameof(SelectedItem), typeof(MyListBox));
    }
}
