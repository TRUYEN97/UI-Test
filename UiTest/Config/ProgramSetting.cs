using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.Config
{
    public class ProgramSetting
    {
        private int column = 1;
        private int row = 1;

        public int Column { get => column; set => column = value < 1 ? 1 : value; }
        internal bool IsSingleView => Column == 1 && Row == 1;
        public int Row { get => row; set => row = value < 1 ? 1 : value; }
        public string View { get; set; } = "TestUnitViewModel";
        public string Local_log { get; set; } = "D:/UI Test logs";
        public bool ShowMissingErrorCode { get; set; } = true;

    }
}
