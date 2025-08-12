using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using UiTest.ModelView.Input;

namespace UiTest.View.Input
{
    /// <summary>
    /// Interaction logic for InputPropertyView.xaml
    /// </summary>
    public partial class InputPropertyView : Window
    {

        public readonly InputPropertyModelView InputPropertyModelView;
        public InputPropertyView(string key, string value)
        {
            InitializeComponent();
            InputPropertyModelView = new InputPropertyModelView(key, value, () => { DialogResult = true; });
            DataContext = InputPropertyModelView;
            Key.Focus();
        }
    }
}
