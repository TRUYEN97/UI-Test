using System;
using System.IO;
using Newtonsoft.Json;

namespace UiTest.Config
{
    public class ConfigLoader
    {
        private static readonly Lazy<ConfigLoader> _instance = new Lazy<ConfigLoader>(() => new ConfigLoader());
        private static ConfigLoader Instance => _instance.Value;
        private ProgramConfig _programConfig;
        public static string CfPath { get; } = "./config.json";
        public ConfigLoader() 
        {
            if (!Init(CfPath))
            {
                _programConfig = new ProgramConfig();
            }
            UpdateCf();
        }
        public static ProgramConfig ProgramConfig
        {
            get
            {
                return Instance._programConfig;
            }
        }

        private bool Init(string cfPath)
        {
            try
            {
                if (!File.Exists(cfPath))
                {
                    return false;
                }
                string configText = File.ReadAllText(cfPath);
                _programConfig = JsonConvert.DeserializeObject<ProgramConfig>(configText);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool UpdateConfig()
        {
            return Instance.UpdateCf();
        }

        public bool UpdateCf()
        {
            try
            {
                string cfJson = JsonConvert.SerializeObject(_programConfig, Formatting.Indented);
                File.WriteAllText(CfPath, cfJson);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
