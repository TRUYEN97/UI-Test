
namespace UiTest.Config.Items
{
    public class ItemSetting
    {
        public ItemSetting() { }
        public string Name { get; set; }
        public string FunctionConfig { get; set; }
        public int Retry { get; set; } = 0;
        public int TimeOut { get; set; } = int.MaxValue;
        public bool IsSkipFailure { get; set; } = false;
        public bool IsMultiTask { get; set; } = false;
        public bool IsDontWaitForMe { get; set; } = false;
    }
}
