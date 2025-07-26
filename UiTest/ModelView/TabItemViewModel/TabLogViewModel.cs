using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UiTest.ModelView.TabItemViewModel
{
    internal class TabLogViewModel : BaseTabItemViewModel
    {
        private string _log;

        public TabLogViewModel()
        {
            Name = "Log";
        }
        public string Log { get => _log; set { _log = value; OnPropertyChanged(); } }
    }
}
