using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using UiTest.Config;
using UiTest.Model.Interface;
using UiTest.ModelView.Component;
using UiTest.ModelView.ListBoxItems;
using UiTest.Service.Factory;
using UiTest.Service.Relay;
using UiTest.View.Input;

namespace UiTest.ModelView.TabItemViewModel.SettingTabs
{
    public class TabSettingModelView : BaseTabSettingModelView
    {
        private readonly ProgramSetting programSetting;
        private string _product;
        private string _station;
        private string _logPath;
        private int _column;
        private int _row;
        private string viewSelected;

        public TabSettingModelView(ProgramSetting programSetting) : base("View setting")
        {
            this.programSetting = programSetting;
            ViewNames = new ObservableCollection<string>();
            ListBoxModelView = new ListBoxModelView("Properties");
            Reload();
            AddProperty = new RelayCommand(_ =>
            {
                var newItem = new PropertySettingModelView(string.Empty, string.Empty, ListBoxModelView.Items);
                ListBoxModelView.Add(newItem);
                ListBoxModelView.SelectedItem = newItem;
            });
            DeleteProperty = new RelayCommand(_ => ListBoxModelView.Remove(ListBoxModelView.SelectedItem), _ => ListBoxModelView.SelectedItem is PropertySettingModelView);
            ListBoxModelView.SelectedItemChangedEvent += i => { AddProperty.RaiseCanExecuteChanged(); DeleteProperty.RaiseCanExecuteChanged(); };
        }
        public ListBoxModelView ListBoxModelView { get; set; }
        public RelayCommand AddProperty { get; private set; }
        public RelayCommand DeleteProperty { get; private set; }
        public string Product { get => _product; set { _product = value; } }
        public string Station { get => _station; set { _station = value; } }
        public string LogPath { get => _logPath; set { _logPath = value; } }
        public string Column { get => _column.ToString(); set { if (int.TryParse(value, out int num)) _column = num; } }
        public string Row { get => _row.ToString(); set { if (int.TryParse(value, out int num)) _row = num; } }
        public ObservableCollection<string> ViewNames { get; private set; }
        public string ViewSelected { get => viewSelected; set { viewSelected = value; OnPropertyChanged(); } }
        public override void Reload()
        {
            Product = programSetting?.Product;
            Station = programSetting?.Station;
            LogPath = programSetting?.Local_log;
            Column = programSetting?.Column.ToString();
            Row = programSetting?.Row.ToString();
            ReloadViewName();
            ReloadProperties();

        }

        private void ReloadProperties()
        {
            ListBoxModelView.Clear();
            programSetting.Properties = programSetting.Properties ?? new Dictionary<string, string>();
            ListBoxModelView.Add(new PropertyModelView("Name", "Value"));
            foreach (var item in programSetting.Properties)
            {
                ListBoxModelView.Add(new PropertySettingModelView(item.Key, item.Value, ListBoxModelView.Items));
            }
        }
        private void ReloadViewName()
        {
            ViewNames.Clear();
            ViewSelected = null;
            var factory = ViewModelFactory.Instance;
            foreach (var item in factory.ListName)
            {
                ViewNames.Add(item);
            }
            string viewName = programSetting?.View;
            if (factory.Exists(viewName))
            {
                ViewSelected = viewName;
            }
        }

        public override void Save()
        {
            programSetting.Product = Product ?? string.Empty;
            programSetting.Station = Station ?? string.Empty;
            programSetting.Local_log = LogPath ?? string.Empty;
            programSetting.Column = _column;
            programSetting.Row = _row;
            programSetting.View = viewSelected;
            programSetting.Properties = programSetting.Properties ?? new Dictionary<string, string>();
            programSetting.Properties.Clear();
            foreach (var item in ListBoxModelView.Items)
            {
                if (item is PropertySettingModelView settingModelView)
                {
                    programSetting.Properties.Add(settingModelView.Name, settingModelView.Value);
                }
            }
        }
    }
}
