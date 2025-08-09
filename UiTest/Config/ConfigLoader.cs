﻿using System;
using System.IO;
using Newtonsoft.Json;
using UiTest.Common;
using UiTest.Service.Logger;

namespace UiTest.Config
{
    public class ConfigLoader
    {
        private static readonly Lazy<ConfigLoader> _instance = new Lazy<ConfigLoader>(() => new ConfigLoader());
        private static ConfigLoader Instance => _instance.Value;
        private ProgramConfig _programConfig;
        public static string CfPath { get; } = "./config.json";
        private ConfigLoader()
        {
            if (!Init(CfPath))
            {
                _programConfig = new ProgramConfig();
            }
            UpdateCf(CfPath);
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
                var programConfig = JsonConvert.DeserializeObject<ProgramConfig>(configText, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                _programConfig = programConfig;
                return true;
            }
            catch (Exception ex)
            {
                ProgramLogger.AddError(GetType().Name, ex.Message);
                return false;
            }
        }

        public static bool UpdateConfig()
        {
            return Instance.UpdateCf(CfPath);
        }
        
        public static bool Reload()
        {
            return Instance.Init(CfPath);
        }

        public static bool SaveConfig(string path)
        {
            return Instance.UpdateCf(path);
        }

        public bool UpdateCf(string path)
        {
            try
            {
                string cfJson = JsonConvert.SerializeObject(_programConfig, Formatting.Indented, new JsonSerializerSettings()
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
