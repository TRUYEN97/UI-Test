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
        public int PassCount { get; set; }
        public int FailCount { get; set; }
        public int FailContinue { get; set; }
        public void Set(Dictionary<string, string> properties)
        {
            Clear();
            properties.Add("Name", Name);
            if (properties != null)
            {
                foreach (var item in properties)
                {
                    this.properties[item.Key] = item.Value;
                }
            }
        }

        public void Clear()
        {
            properties.Clear();
        }

        public bool Add(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key)) return false;
            properties[key] = value?.Trim() ?? string.Empty;
            return true;
        }
        public string Get(string key, string defaultValue)
        {
            return properties[key] ?? defaultValue;
        }
        public bool TryGet(string key, out string value)
        {
            return properties.TryGetValue(key, out value);
        }
        public Dictionary<string, string> Properties => new Dictionary<string, string>(properties);
    }
}
