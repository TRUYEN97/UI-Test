using System.Collections.Generic;

namespace UiTest.Config.Items
{
    public class ItemGroup
    {
        public ItemGroup()
        {
            Items = new List<ItemSetting>();
        }
        public bool IsFinalGroup { get; set; }
        public string FailColor { get; set; }
        public string NextToPassGroup { get; set; }
        public string NextToFailGroup { get; set; }
        public List<ItemSetting> Items { get; set; }
    }
}
