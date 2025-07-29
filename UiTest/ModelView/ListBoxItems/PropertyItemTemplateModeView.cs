using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.ModelView.ListBoxItems
{
    public class PropertyItemTemplateModeView : BaseViewModel
    {
        private string _name;
        private string _value;

        public PropertyItemTemplateModeView(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get => _name; private set { _name = value; OnPropertyChanged(); } }
        public string Value { get => _value; private set { _value = value; OnPropertyChanged(); } }
    }
}
