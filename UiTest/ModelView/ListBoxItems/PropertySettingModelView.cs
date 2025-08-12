using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using UiTest.Model.Interface;
using UiTest.Service.Relay;

namespace UiTest.ModelView.ListBoxItems
{
    public class PropertySettingModelView : BaseModelView, IKeyValue
    {
        private string _name;
        private string _value;
        private string _iconPath;
        private readonly ICollection<object> itemEvents;
        public PropertySettingModelView(string name, string value, ICollection<object> itemEvents)
        {
            this.itemEvents = itemEvents;
            DeleteCommand = new RelayCommand(_ => { itemEvents?.Remove(this); DeleteEvent?.Invoke(this); });
            DoubleClickCommand = new RelayCommand(_ => DoubleClickEvent?.Invoke(this));
            ClickCommand = new RelayCommand(_ => ClickEvent?.Invoke(this));
            _name = name;
            _value = value;
        }
        public string IconPath { get => _iconPath; set { _iconPath = value; OnPropertyChanged(); } }
        public object Tag { get; set; }
        public event Action<PropertySettingModelView> DoubleClickEvent;
        public event Action<PropertySettingModelView> DeleteEvent;
        public event Action<PropertySettingModelView> ClickEvent;
        public bool IsNameReadOnly { get; set; }
        public bool IsValueReadOnly { get; set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand DoubleClickCommand { get; private set; }
        public RelayCommand ClickCommand { get; private set; }
        private bool IContainName(string value)
        {
            foreach (var item in itemEvents)
            {
                if (item is IKeyValue keyValue && keyValue.Name.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        public string Name
        {
            get => _name?.ToUpper()?.Trim();
            set
            {
                if (IContainName(value))
                {
                    MessageBox.Show($"Name [{value}] has exists!");
                }
                else
                {
                    _name = value;
                }
                OnPropertyChanged();
            }
        }
        public string Value
        {
            get => _value?.Trim();
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
    }
}
