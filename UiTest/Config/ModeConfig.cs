using System;
using System.Collections.Generic;

namespace UiTest.Config
{
    public class ModeConfig
    {
        public bool IsOnSFO {  get; set; }
        public List<StepFunction> StepFunctions { get; set; } = new List<StepFunction>();
        public string GroupName { get; set; }
        public string StandbyColor { get; set; }
        public int LoopTimes { get; set; }
    }
}
