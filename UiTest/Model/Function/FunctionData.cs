using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiTest.Service.Logger;

namespace UiTest.Model.Function
{
    public class FunctionData
    {
        public readonly string Name;
        public readonly string FunctionName;
        public readonly MyLogger logger;
        public FunctionData(string name, string functionName)
        {
            Name = name;
            FunctionName = functionName;
            logger = new MyLogger();
        }

        public string Value { get; internal set; }
        public string Result { get; internal set; }
        public string TestTime { get; internal set; }
        public string UpperLimit { get; internal set; }
        public string LowerLimit { get; internal set; }
        public string Spec { get; internal set; }
        public string ErrorCode { get; internal set; }
        public bool IsPass { get; internal set; }

        public override string ToString()
        {
            return $"{Name}/{FunctionName}";
        }
    }
}
