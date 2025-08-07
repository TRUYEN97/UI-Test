using System;
using System.Collections.Generic;
using UiTest.Config.Events;
using UiTest.Config.Items;
using UiTest.Functions.ActionEvents.Configs;

namespace UiTest.Config
{
    public class ModeConfig
    {
        public ModeConfig()
        {
            BeginGroup = "Test";
            LoopTimes = 1;
        }
        public string BeginGroup { get; set; }
        public int LoopTimes { get; set; } = 1;
        public string StandbyColor { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, ItemGroup> ItemGroups { get; set; } = new Dictionary<string, ItemGroup>()
        {
            {"Test", new ItemGroup() { Items = new List<ItemSetting>() { new ItemSetting{Name = "Sfis", FunctionConfig = "Sfis", TimeOut = 10000} } } }
        };
        public List<ActionEventSetting> InputEvents { get; set; } = new List<ActionEventSetting>()
        { 
            new ActionEventSetting() { 
                FunctionType = "CheckInputLength",
                Config = new InputLengthConfig() 
                {
                    LowerLimit = 1,
                    UpperLimit = 3
                } 
            } 
        };
        public List<ActionEventSetting> ModeChangeEvents { get; set; } = new List<ActionEventSetting>
        {
            new ActionEventSetting{FunctionType = "LoginAction", Config = new LoginConfig()}
        };
    }
}
