using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Newtonsoft.Json;
using UiTest.Common;
using UiTest.Config;
using UiTest.ModelView.TabItemViewModel;
using UiTest.ModelView.TabItemViewModel.SettingTabs;
using UiTest.Service.Logger;
using UiTest.Service.Relay;

namespace UiTest.ModelView
{
    public class SettingModelView : BaseModelView
    {
        private ProgramConfig config;
        private string configPath;
        private string savePath;
        private BaseTabSettingModelView _selectedTab;
        private int _selectedIndex;

        public SettingModelView(string configPath, string savePath)
        {
            Tabs = new ObservableCollection<BaseTabSettingModelView>();
            ConfigPath = configPath;
            SavePath = savePath;
            SaveCommand = new RelayCommand(_ => SaveConfig(SavePath));
            ReLoadCommand = new RelayCommand(_ => ReLoad());
            ReLoad();
        }
        public ObservableCollection<BaseTabSettingModelView> Tabs { get; }
        public BaseTabSettingModelView SelectedTab { get => _selectedTab; set { _selectedTab = value; OnPropertyChanged(); } }
        public int SelectedIndex { get => _selectedIndex; set { _selectedIndex = value; OnPropertyChanged(); } }
        public string ConfigPath { get => configPath; private set => configPath = value; }
        public string SavePath { get => savePath; private set => savePath = value; }
        public ICommand SaveCommand { get; private set; }
        public ICommand ReLoadCommand { get; private set; }

        private void ReLoad()
        {
            int tabIndex = SelectedIndex;
            config = Load(configPath);
            Tabs.Clear();
            Tabs.Add(new TabErrorCodeSettingModelView(config.ErrorCode));
            Tabs.Add(new TabSettingModelView(config.ProgramSetting));
            Tabs.Add(new ActionEventSettingModelView(config.ActionEvents));
            SelectedIndex = Tabs.Count > tabIndex ? tabIndex : 0;
        }

        private ProgramConfig Load(string cfPath)
        {
            try
            {
                if (!File.Exists(cfPath))
                {
                    return new ProgramConfig();
                }
                string configText = File.ReadAllText(cfPath);
                var programConfig = JsonConvert.DeserializeObject<ProgramConfig>(configText, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                return programConfig;
            }
            catch (Exception ex)
            {
                ProgramLogger.AddError(GetType().Name, ex.Message);
                return new ProgramConfig();
            }
        }
        private bool SaveConfig(string path)
        {
            try
            {
                foreach (var item in Tabs)
                {
                    item.Save();
                }
                string cfJson = JsonConvert.SerializeObject(config, Formatting.Indented, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                FileUtil.WriteAllText(path, cfJson);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
