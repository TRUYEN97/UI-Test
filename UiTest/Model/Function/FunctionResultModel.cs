using UiTest.Model.Interface;

namespace UiTest.Model.Function
{
    public class FunctionResultModel: IFunctionResult
    {
        public string StartTime { get;  set; }
        public string StopTime { get;  set; }
        public string Value { get;  set; }
        public string Result { get;  set; }
        public double CycleTime { get;  set; }
        public string UpperLimit { get;  set; }
        public string LowerLimit { get;  set; }
        public string Spec { get;  set; }
        public string ErrorCode { get;  set; }
    }
}
