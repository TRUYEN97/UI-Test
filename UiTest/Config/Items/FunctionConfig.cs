using UiTest.Functions.TestFunctions.Config;

namespace UiTest.Config.Items
{
    public class FunctionConfig
    {
        public string FunctionType { get; set; }
        internal string Name { get; set; }
        internal ItemSetting ItemSetting { get; set; }
        public BasefunctionConfig Config { get; set; }
    }
}
