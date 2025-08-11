using System;
using UiTest.Service.Relay;

namespace UiTest.ModelView.ListBoxItems
{
    public class PropertyModelView : BaseModelView
    {
        private string _name;
        private string _value;
        public PropertyModelView(string name, string value) : this(name, value, null) { }
        public PropertyModelView(string name, string value, Action<PropertyModelView> ItemEvent)
        {
            Name = name;
            Value = value;
            Command = new RelayCommand(_ => _itemEvent?.Invoke(this), _ => string.IsNullOrWhiteSpace(Name) && _itemEvent != null);
            this.ItemEvent += ItemEvent;
        }
        private event Action<PropertyModelView> _itemEvent;
        public event Action<PropertyModelView> ItemEvent
        {
            add { _itemEvent += value; Command.RaiseCanExecuteChanged(); }
            remove { _itemEvent -= value; Command.RaiseCanExecuteChanged(); }
        }
        public RelayCommand Command { get; private set; }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); Command.RaiseCanExecuteChanged(); } }
        public string Value { get => _value; set { _value = value; } }
    }
}
