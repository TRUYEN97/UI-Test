using System.Collections.Generic;

namespace UiTest.Config.Items
{
    public class ItemGroup
    {
        public ItemGroup()
        {
            Items = new List<ItemSetting>();
        }
        public bool IsFinalGroup { get; set; } = false;
        public string FailColor { get; set; } = "#FFFF0000";
        public string NextToPassGroup { get; set; } = string.Empty; 
        public string NextToFailGroup { get; set; } = string.Empty;
        public string NextToCancelGroup { get; set; } = string.Empty;
        public List<ItemSetting> Items { get; set; } = new List<ItemSetting>();
    }
}
