using System;
using System.Collections.Generic;

namespace UiTest.Config
{
    public class ModeConfig
    {
        public ModeConfig() {
            GroupName = "Test";
            IsOnSFO = true;
            LoopTimes = 1;
        }
        public bool IsOnSFO {  get; set; }
        public string GroupName { get; set; }
        public int LoopTimes { get; set; }
        public string StandbyColor { get; set; }
        public string PassColor { get; internal set; }
        public string CancelColor { get; internal set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public List<StepFunction> StepFunctions { get; set; } = new List<StepFunction>();
    }
}
