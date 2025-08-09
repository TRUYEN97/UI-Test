using System;
using System.Windows.Input;
using UiTest.Config;
using UiTest.Service.Relay;

namespace UiTest.ModelView
{
    public class SettingModelView:BaseModelView
    {
        private readonly ProgramConfig config;
        private string configPath;
        private string savePath;

        public SettingModelView(string configPath, string savePath)
        {
            config = ConfigLoader.ProgramConfig;
            ConfigPath = configPath;
            SavePath = savePath;
            SaveCommand = new RelayCommand( _ =>
            {
                ConfigLoader.SaveConfig(SavePath);
            });
        }
        public string ConfigPath { get => configPath; private set => configPath = value; }
        public string SavePath { get => savePath; private set => savePath = value; }
        public ICommand SaveCommand { get; private set; }
    }
}
