using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiTest.Config;
using UiTest.ModelView.ListBoxItems;
using UiTest.Service.Factory;
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
            Properties = new ObservableCollection<PropertyModelView>();
            Reload();

        }
        public string Product { get => _product; set { _product = value; } }
        public string Station { get => _station; set { _station = value; } }
        public string LogPath { get => _logPath; set { _logPath = value; } }
        public string Column { get => _column.ToString(); set { if (int.TryParse(value, out int num)) _column = num; } }
        public string Row { get => _row.ToString(); set { if (int.TryParse(value, out int num)) _row = num; } }
        public ObservableCollection<string> ViewNames { get; private set; }
        public string ViewSelected { get => viewSelected; set { viewSelected = value; OnPropertyChanged(); } }
        public ObservableCollection<PropertyModelView> Properties { get; private set; }
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
            Properties.Clear();
            programSetting.Properties = programSetting.Properties ?? new Dictionary<string, string>();
            foreach (var item in programSetting.Properties)
            {
                Properties.Add(new PropertyModelView(item.Key, item.Value, PropertyClick));
            }
        }

        private void PropertyClick(PropertyModelView propertyModelView)
        {
            if (string.IsNullOrWhiteSpace(propertyModelView?.Name))
            {
                return;
            }
            var inputView = new InputPropertyView(propertyModelView.Name, propertyModelView.Value);
            inputView.ShowDialog();
            propertyModelView.Name = inputView.Key.Text;
            propertyModelView.Value = inputView.Value.Text;
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
            foreach (var item in Properties)
            {
                programSetting.Properties.Add(item.Name, item.Value);
            }
        }
    }
}
