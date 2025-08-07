
using UiTest.Functions;

namespace UiTest.Functions.TestFunctions.Config
{
    public abstract class BasefunctionConfig
    {
        public string Spec { get; set; } = string.Empty;
        public string UpperLimit { get; set; } = string.Empty;
        public string LowerLimit { get; set; } = string.Empty;
    }
}
