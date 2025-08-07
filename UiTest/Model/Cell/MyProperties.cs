using System;
using System.Collections.Generic;

namespace UiTest.Model.Cell
{
    public class MyProperties
    {
        public readonly int Index;
        public readonly string Name;
        private readonly Dictionary<string, string> properties;
        public MyProperties(string name, int index)
        {
            Name = name;
            properties = new Dictionary<string, string>();
            Index = index;
        }
        public int PassCount {  get; set; }
        public int FailCount { get; set; }
        public void SetProperties(Dictionary<string, string> properties)
        {
            this.properties.Clear();
            this.properties.Add("Name", Name);
            if (properties != null)
            {
                foreach (var item in properties)
                {
                    this.properties[item.Key] = item.Value;
                }
            }
        }
        public Dictionary<string, string> Properties => new Dictionary<string, string>(properties);
    }
}
