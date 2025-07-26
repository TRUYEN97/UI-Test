using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.Functions.Config.Sfis
{
    public class SfisConfig : BasefunctionConfig
    {
        public SfisConfig() { }
        public string Param1 { get; set; } = "test Param1";
        public string Param2 { get; set; } = "test Param2";
        public int Param3 { get; set; } = 11;
    }
}
