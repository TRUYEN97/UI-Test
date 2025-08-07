using System.Collections.Generic;
using UiTest.Service.ErrorCode;
using UiTest.Config.Items;
using UiTest.Functions.TestFunctions.Body.Sfis;
using UiTest.Functions.TestFunctions.Config.Sfis;

namespace UiTest.Config
{
    public class ProgramConfig
    {

        public ProgramConfig()
        {
            Modes.Add("Production", new ModeConfig());
        }
        public ProgramSetting ProgramSetting { get; set; } = new ProgramSetting();
        public ErrorCodeMapperConfig ErrorCode { get; set; } = new ErrorCodeMapperConfig();
        public ActionEvents ActionEvents { get; set; } = new ActionEvents();
        public Dictionary<string, ModeConfig> Modes { get; set; } = new Dictionary<string, ModeConfig>();
        public Dictionary<string, FunctionConfig> FunctionConfigs { get; set; } = new Dictionary<string, FunctionConfig>()
        {
            {"Sfis", new FunctionConfig(){ FunctionType = nameof(SendSfis), Name = "Sfis", Config = new SfisConfig() } }
        };
    }
}
