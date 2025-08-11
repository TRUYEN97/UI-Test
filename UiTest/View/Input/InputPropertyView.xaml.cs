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

namespace UiTest.View.Input
{
    /// <summary>
    /// Interaction logic for InputPropertyView.xaml
    /// </summary>
    public partial class InputPropertyView : Window
    {
        public InputPropertyView(string key, string view)
        {
            InitializeComponent();
            Key.Text = key;
            Value.Text = view;
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Key.Text))
            {
                MessageBox.Show("Key == null!");
                return;
            }
            DialogResult = true;
        }
    }
}
