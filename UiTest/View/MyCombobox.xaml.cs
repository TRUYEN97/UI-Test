using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UiTest.Common;

namespace UiTest.View
{
    /// <summary>
    /// Interaction logic for MyCombobox.xaml
    /// </summary>
    public partial class MyCombobox : UserControl
    {
        public MyCombobox()
        {
            InitializeComponent();
            FontSize = 16;
        }
        public string Label
        {
            get => (string)GetValue(LabelPropertie);
            set => SetValue(LabelPropertie, value);
        }
        public IEnumerable ItemsSource
        {
            get => (IEnumerable) GetValue(ItemsSourcePropertie);
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

        public static readonly DependencyProperty LabelFontSizePropertie = Util.CreateDependencyProperty<int>(nameof(LabelFontSize), typeof(MyCombobox), 12);
        public static readonly DependencyProperty LabelPropertie = Util.CreateDependencyProperty<string>(nameof(Label), typeof(MyCombobox));
        public static readonly DependencyProperty ItemsSourcePropertie = Util.CreateDependencyProperty<IEnumerable>(nameof(ItemsSource), typeof(MyCombobox));
        public static readonly DependencyProperty SelectedItemPropertie = Util.CreateDependencyProperty<object>(nameof(SelectedItem), typeof(MyCombobox));
    }
}
