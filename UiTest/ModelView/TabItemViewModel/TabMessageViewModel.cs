using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.ModelView.TabItemViewModel
{
    internal class TabMessageViewModel: BaseTabItemViewModel
    {
        private string _message;

        public TabMessageViewModel()
        {
            Name = "View";
        }
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
    }
}
