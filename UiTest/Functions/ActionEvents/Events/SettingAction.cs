using UiTest.Common;
using UiTest.Functions.ActionEvents.Configs;
using UiTest.View;

namespace UiTest.Functions.ActionEvents.Events
{
    public class SettingAction : BaseActionEvent<SettingConfig>
    {
        public SettingAction(SettingConfig config) : base(config)
        {
        }

        protected override TestResult Test()
        {
            SettingView settingView = new SettingView(Config.ConfigPath, Config.SavePath);
            return settingView.ShowDialog() == true ? TestResult.PASSED: TestResult.FAILED;
        }
    }
}
