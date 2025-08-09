using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiTest.Config;

namespace UiTest.Functions.ActionEvents.Configs
{
    public class SettingConfig
    {
        public string ConfigPath {  get; set; } = ConfigLoader.CfPath;
        public string SavePath {  get; set; } = ConfigLoader.CfPath;
    }
}
