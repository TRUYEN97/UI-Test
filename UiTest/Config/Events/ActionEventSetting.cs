using System;
using UiTest.Functions.TestFunctions.Config;

namespace UiTest.Config.Events
{
    public class ActionEventSetting
    {
        private string name;

        public string Name { get => name ?? FunctionType ?? string.Empty; set => name = value; }
        public string FunctionType { get; set; }
        public object Config { get; set; }
    }
}
