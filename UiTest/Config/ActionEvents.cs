using System;
using System.Collections.Generic;
using UiTest.Config.Events;
using UiTest.Functions.ActionEvents.Configs;

namespace UiTest.Config
{
    public class ActionEvents
    {
        public List<ActionEventSetting> LauchEvents { get; set; } = new List<ActionEventSetting>
        {
            new ActionEventSetting{FunctionType = "LoginAction", Config = new LoginConfig()}
        };
        public List<ActionEventSetting> WindownClosingEvents { get; set; } = new List<ActionEventSetting>
        {
            new ActionEventSetting{FunctionType = "LoginAction", Config = new LoginConfig()}
        };
        public List<ActionEventSetting> ActionTools { get; set; } = new List<ActionEventSetting>
        {
            new ActionEventSetting{Name = "Setting", FunctionType = "SettingAction", Config = new SettingConfig()}
        };
    }
}
