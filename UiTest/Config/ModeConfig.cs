using System;
using System.Collections.Generic;

namespace UiTest.Config
{
    public class ModeConfig
    {
        public bool IsOnSFO {  get; set; }
        public List<StepFunction> StepFunctions { get; set; } = new List<StepFunction>();
        public string GroupItem { get; set; }
    }
}
