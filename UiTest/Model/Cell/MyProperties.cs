using System;
using System.Collections.Generic;

namespace UiTest.Model.Cell
{
    public class MyProperties
    {
        private readonly Dictionary<string, string> properties;
        public MyProperties(string name)
        {
            Name = name;
            properties = new Dictionary<string, string>();
        }
        public string Name { get; private set; }

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
    }
}
