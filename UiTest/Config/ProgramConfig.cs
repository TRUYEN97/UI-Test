using System.Collections.Generic;

namespace UiTest.Config
{
    public class ProgramConfig
    {
        public ProgramConfig() 
        {
            Modes.Add("Production", new ModeConfig());
        }
        public ProgramInfo ProgramInfo { get; set; } = new ProgramInfo();
        public ProgramSetting ProgramSetting { get; set; } = new ProgramSetting();
        public Dictionary<string, ModeConfig> Modes { get; set; } = new Dictionary<string, ModeConfig>();
    }
}
