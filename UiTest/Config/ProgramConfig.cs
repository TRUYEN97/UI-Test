using System.Collections.Generic;
using UiTest.Functions.Body.Sfis;
using UiTest.Functions.Config.Sfis;

namespace UiTest.Config
{
    public class ProgramConfig
    {
        public ProgramConfig() 
        {
            Modes.Add("Production", new ModeConfig() { GroupName = "Test", IsOnSFO = true, LoopTimes = 1});
        }
        public ProgramInfo ProgramInfo { get; set; } = new ProgramInfo();
        public ProgramSetting ProgramSetting { get; set; } = new ProgramSetting();
        public Dictionary<string, ModeConfig> Modes { get; set; } = new Dictionary<string, ModeConfig>();
        public Dictionary<string, ItemGroup> ItemGroups { get; set; } = new Dictionary<string, ItemGroup>()
        {
            {"Test", new ItemGroup() { Items = new List<string>() { "Sfis"} } }
        };
        public Dictionary<string, ItemConfig> ItemConfigs { get; set; } = new Dictionary<string, ItemConfig>()
        {
            {"Sfis", new ItemConfig(){ FunctionType = nameof(SendSfis), Name = "Sfis", Config = new SfisConfig() } }
        };
    }
}
