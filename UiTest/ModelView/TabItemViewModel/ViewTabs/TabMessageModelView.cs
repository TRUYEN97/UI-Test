using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.ModelView.TabItemViewModel.ViewTabs
{
    internal class TabMessageModelView: BaseViewTabModelView
    {
        private string _message;

        public TabMessageModelView()
        {
            Name = "View";
        }
        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
    }
}
