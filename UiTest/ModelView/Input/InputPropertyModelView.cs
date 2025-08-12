using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UiTest.Service.Relay;

namespace UiTest.ModelView.Input
{
    public class InputPropertyModelView : BaseModelView
    {
        private string _value;
        private string _key;

        public InputPropertyModelView(Action action) : this(string.Empty, string.Empty, action) { }
        public InputPropertyModelView(string key, string value, Action action)
        {
            Command = new RelayCommand(_ =>
            {
                action?.Invoke();
            },
            _ => !string.IsNullOrWhiteSpace(Key) && !string.IsNullOrWhiteSpace(Value));
            Key = key;
            Value = value;
        }
        public string Key { get => _key?.ToUpper()?.Trim() ; set { _key = value; OnPropertyChanged(); Command.RaiseCanExecuteChanged(); } }
        public string Value { get => _value?.Trim(); set { _value = value; OnPropertyChanged(); Command.RaiseCanExecuteChanged(); } }
        public RelayCommand Command { get; private set; }
    }
}
