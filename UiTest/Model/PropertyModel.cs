using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTest.Model
{
    public class PropertyModel
    {
        public PropertyModel(string name, string value)
        {   
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
    }
}
