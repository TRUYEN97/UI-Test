using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UiTest.Common;

namespace UiTest.View
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

        public static readonly DependencyProperty LabelPropertie = Util.CreateDependencyProperty(nameof(Label), typeof(LabeledStatusBox), string.Empty);
        public static readonly DependencyProperty LabelFontSizePropertie = Util.CreateDependencyProperty(nameof(LabelFontSize), typeof(LabeledStatusBox), 12);
        public static readonly DependencyProperty TextProperty = Util.CreateDependencyProperty(nameof(Text), typeof(LabeledStatusBox),string.Empty);
    }
}
