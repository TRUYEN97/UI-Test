using System;
using System.Collections.ObjectModel;

namespace UiTest.ModelView.Component
{
    public class ListBoxModelView : BaseModelView
    {
        private object _selectedItem;

        public ListBoxModelView(string title)
        {
            Title = title;
            Items = new ObservableCollection<object>();
        }
        public object SelectedItem { get => _selectedItem; set { _selectedItem = value; OnPropertyChanged(); SelectedItemChangedEvent?.Invoke(value); } }
        public ObservableCollection<object> Items { get; private set; }
        public event Action<object> SelectedItemChangedEvent;
        public string Title { get; private set; }
        public void Add(object newItem)
        {
            if (newItem != null)
            {
                Items?.Add(newItem);
            }
        }

        public void Remove(object newItem)
        {
            Items?.Remove(newItem);
        }

        public void Clear()
        {
            Items?.Clear();
            SelectedItem = default;
        }
    }
}
