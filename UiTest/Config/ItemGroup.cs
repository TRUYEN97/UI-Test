using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace UiTest.Config
{
    public class ItemGroup
    {
        public ItemGroup()
        {
            Items = new List<string>();
        }
        public bool IsFinalGroup { get; set; }
        public string TestColor { get; set; }
        public string FailColor { get; set; }
        public string NextToPassGroup { get; set; }
        public string NextToFailGroup { get; set; }
        public List<string> Items { get; set; }
    }
}
