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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UiTest.ModelView.TabItemViewModel;

namespace UiTest.View.TabItemView
{
    /// <summary>
    /// Interaction logic for TabLoggerView.xaml
    /// </summary>
    public partial class TabLoggerView : UserControl
    {
        public TabLoggerView()
        {
            InitializeComponent();
            DataContext = new TabLoggerViewModel();
        }
    }
}
