using System;
using System.Collections.Generic;

namespace UiTest.Config
{
    public class ProgramSetting
    {
        private int column = 1;
        private int row = 1;
        public string Product { get; set; } = string.Empty;
        public string Station { get; set; } = string.Empty;
        public int Column { get => column; set => column = value < 1 ? 1 : value; }
        internal bool IsSingleView => Column == 1 && Row == 1;
        public int Row { get => row; set => row = value < 1 ? 1 : value; }
        public string View { get; set; } = "TestUnitViewModel";
        public string Local_log { get; set; } = "D:/UI Test logs";
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

    }
}
